using System;
using System.Collections;
using System.Collections.Generic;
using Messages.Response;
using Messages.Utils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    
    private const string AudioToStringEndpoint = "audioToText";
    private const string ModelPromptEndpoint = "modelPrompt";
    private ChatManager _chatManager;
    
    private AudioClip _recordedClip;
    private string _micName;
    private InputAction _openMicAction;
    private Button _button;


    private bool _isFetching = false;
    private bool _isRecording = false;

    private bool _recordingFromButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Awake()
    {
         _button=GetComponent<Button>(); 
        
        _chatManager=GameObject.FindWithTag("ChatManager").GetComponent<ChatManager>();
    }

    private void Start()
    {

        _openMicAction = InputSystem.actions.FindAction("Talk");

        if (Microphone.devices.Length > 0)
        {
            _micName = Microphone.devices[0];
            Debug.Log("Usando micr�fono: " + _micName);
        }
        else
        {
            Debug.LogError("No se detectaron micr�fonos!");
        }
    }
    
    private void Update()
    {
        if (_isRecording && !_recordingFromButton && _openMicAction.ReadValue<float>() <= 0)
        {
            StopRecording();
            return;
        }
        if (!_isFetching && !_isRecording && _openMicAction.ReadValue<float>() > 0)
        {
            StartRecording();
            _recordingFromButton = false;
        }
    }


    public void StartRecordingFromButton()
    {
        if(_isRecording || _isFetching) return;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(StopRecordingFromButton);
        GetComponent<TextUIController>().SetText("Detener");
        _recordingFromButton = true;
        StartRecording();
    }

    public void StopRecordingFromButton()
    {
        if (!_isRecording) return;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(StartRecordingFromButton);
        GetComponent<TextUIController>().SetText("Hablar");
        _recordingFromButton = false;
        StopRecording();
    }


    private void StartRecording()
    {
        _isRecording = true;
        // Pongo 600 segundos (10 minutos) como l�mite m�ximo
        _recordedClip = Microphone.Start(_micName, false, 60, 44100);
    }

    private void StopRecording()
    {
        _isRecording = false;
        ClipAudio();
        TranscribeAudio();

        
    }

    private void ClipAudio()
    {
        //Capture the current clip data
        var position = Microphone.GetPosition(_micName);
        Microphone.End(_micName);
        
        float[] samples = new float[_recordedClip.samples * _recordedClip.channels];
        _recordedClip.GetData(samples, 0);

        //Create shortened array for the data that was used for recording
        var clippedSamples = new float[position * _recordedClip.channels];

        //Copy the used samples to a new array
        for (int i = 0; i < clippedSamples.Length; i++)
        {
            clippedSamples[i] = samples[i];
        }


        AudioClip clippedClip = AudioClip.Create(
            "Clipped",
            position,
            _recordedClip.channels,
            _recordedClip.frequency,
            false
        );
        clippedClip.SetData(clippedSamples, 0);
        _recordedClip = clippedClip;
    }

    private void TranscribeAudio()
    {
        if (_isFetching) return;

        byte[] audioBytes = WavUtil.AudioClipToWAV(_recordedClip);
        
        _isFetching = true;

        StartCoroutine(ApiClient.Post(AudioToStringEndpoint, (respuestaTranscriptaJson) =>
        {
            TextResponse transcribe = JsonUtility.FromJson<TextResponse>(respuestaTranscriptaJson);
            _chatManager.SendMessagePlayer(transcribe.response);
            Debug.Log(transcribe);
            GetComponent<ScrollController>().ResetScroll();
            StartCoroutine(ApiClient.Get(ModelPromptEndpoint, (modelResponseJson) =>
                    {
                        TextResponse modelResponse = JsonUtility.FromJson<TextResponse>(modelResponseJson);

                        _chatManager.ReceiveMessageBot(modelResponse.response);

                        StartCoroutine(GetComponent<ScrollController>().ResetScroll());
                        _isFetching = false;
                    }, parameters: new Dictionary<string, string>
                    {
                        { "prompt", transcribe.response }
                    }
                )
            );
        },body:audioBytes));
    }
    
}
