using System;
using System.Collections;
using System.Collections.Generic;
using Messages.Response;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    private const string AudioToStringEndpoint = "audioToText";
    private ChatManager _chatManager;
    private AudioSource _audioSource;
    
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
        _audioSource = GetComponent<AudioSource>();

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

    public void SendChatMessage()
    {
        if (_isFetching) return;
        _isFetching = true;

        StartCoroutine(ApiClient.Get(AudioToStringEndpoint, (respuesta) =>
        {
            _chatManager.SendMessagePlayer(JsonUtility.FromJson<AudioToTextResponse>(respuesta).response);
            _isFetching = false;
        }));
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
        _recordedClip = Microphone.Start(_micName, false, 600, 44100);
    }

    private void StopRecording()
    {
        _isRecording = false;
        int position = Microphone.GetPosition(_micName); // posici�n real hasta donde grab�
        Microphone.End(_micName);
        
        SendChatMessage();
        _audioSource.clip = _recordedClip;
        _audioSource.Play();


    }
}
