using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BotInteraction : MonoBehaviour, IInteractable
{
    public string InteractMessage => "Presione E para díalogar";

    string modelApiUrl = "http://127.0.0.1:8000/";
    string audioToStringEndpoint = "audioToText";
    bool isInteracting = false;

    ChatManager chatManager;
    private void Start()
    {
        chatManager = GetComponent<ChatManager>();
    }
    public void Interact()
    {
        SceneManager.LoadScene("chat");
        //if (isInteracting) return;
        //isInteracting = true;
        //chatManager.SendMessagePlayer("hola");
        //StartCoroutine(modelCall());
    }

    IEnumerator modelCall()
    {
        UnityWebRequest peticion = UnityWebRequest.Get($"{modelApiUrl}{audioToStringEndpoint}");

        yield return peticion.SendWebRequest();
        isInteracting = false;
        if (peticion.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(peticion.error);
        }
        else
        {
            // Show results as text
            Debug.Log(peticion.downloadHandler.text);
            chatManager.ReceiveMessageBot(peticion.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = peticion.downloadHandler.data;
        }
    }

}
