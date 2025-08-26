using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BotInteraction : MonoBehaviour, IInteractable
{
    public string InteractMessage => "Presione E para díalogar";

    string modelApiUrl = "http://127.0.0.1:8000/";
    string audioToStringEndpoint = "audioToText";
    bool isInteracting = false;
    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;
        Debug.Log("PRESIONADO");
        StartCoroutine(modelCall());
    }

    IEnumerator modelCall()
    {
        UnityWebRequest peticion = UnityWebRequest.Get($"{modelApiUrl}{audioToStringEndpoint}");

        yield return peticion.SendWebRequest();
        isInteracting=false;
        if (peticion.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(peticion.error);
        }
        else
        {
            // Show results as text
            Debug.Log(peticion.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = peticion.downloadHandler.data;
        }
    }

}
