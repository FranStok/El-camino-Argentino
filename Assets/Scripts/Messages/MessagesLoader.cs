using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class MessagesLoader : MonoBehaviour
{
    [SerializeField] private GameObject messagePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChatManager chatManagerInstance = ChatManager.Instance;

        LoadMessages(chatManagerInstance.messages);
    }

    public void LoadMessages(List<Message> mensajes)
    {
        foreach (Message mensaje in mensajes)
        {
            CreateMessageUI(mensaje);
        }
    }
    public void LoadMessages(Message mensaje)
    {
        CreateMessageUI(mensaje);
    }

    private void CreateMessageUI(Message mensaje)
    {
        GameObject newMessageUI = Instantiate(messagePrefab, transform);

        newMessageUI.transform.SetSiblingIndex(transform.childCount - 1);

        newMessageUI.GetComponent<HorizontalLayoutGroup>().childAlignment =
        (mensaje.sender == Sender.Player)
            ? TextAnchor.UpperRight
            : TextAnchor.UpperLeft;

        newMessageUI.GetComponent<SetMessegeToUIText>().SetText(mensaje.text);

    }
}


