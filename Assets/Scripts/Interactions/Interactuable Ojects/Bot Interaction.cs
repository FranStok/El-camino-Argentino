using System.Collections;
using UnityEngine;
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
