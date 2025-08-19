using UnityEngine;
using UnityEngine.InputSystem;

public class MicDetector : MonoBehaviour
{
    private AudioClip recordedClip;
    private string micName;
    private bool isRecording = false;
    InputAction openMicAction;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        openMicAction = InputSystem.actions.FindAction("Talk");

        if (Microphone.devices.Length > 0)
        {
            micName = Microphone.devices[0];
            Debug.Log("Usando micrófono: " + micName);
        }
        else
        {
            Debug.LogError("No se detectaron micrófonos!");
        }
    }

    void Update()
    {
        if (openMicAction.ReadValue<float>() <= 0 && isRecording)
        {
            StopRecording();
            return;
        }
        if (!isRecording && openMicAction.ReadValue<float>() > 0)
        {
            StartRecording();
            return;
        }
    }


    void StartRecording()
    {
        isRecording = true;
        // Pongo 600 segundos (10 minutos) como límite máximo
        recordedClip = Microphone.Start(micName, false, 600, 44100);
        Debug.Log("Grabando...");
    }

    void StopRecording()
    {
        isRecording = false;
        int position = Microphone.GetPosition(micName); // posición real hasta donde grabó
        Microphone.End(micName);
        Debug.Log("Grabación detenida en: " + position + " samples");

        audioSource.clip = recordedClip;
        audioSource.Play();

        // recorto el clip para que quede solo la parte grabada
        //AudioClip trimmedClip = AudioClip.Create("Recorte", position, recordedClip.channels, recordedClip.frequency, false);
        //float[] data = new float[position * recordedClip.channels];
        //recordedClip.GetData(data, 0);
        //trimmedClip.SetData(data, 0);

    }
}
