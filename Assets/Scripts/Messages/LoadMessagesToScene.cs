using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class LoadMessagesToScene : MonoBehaviour
{
    [SerializeField] private GameObject messagePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChatManager chatManagerInstance = ChatManager.Instance;

        foreach (Message mensaje in chatManagerInstance.messages)
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
}


