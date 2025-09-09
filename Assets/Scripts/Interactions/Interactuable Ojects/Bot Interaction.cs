using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BotInteraction : MonoBehaviour, IInteractable
{
    public string InteractMessage => "Presione E para díalogar";


    public void Interact()
    {
        SceneManager.LoadScene("chat");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }



}
