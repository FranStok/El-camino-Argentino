using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public List<Message> messages = new List<Message>();
    public static ChatManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject); // Ensures only one instance exists
        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject); // Persists across scene loads
        }
    }

    private void Start()
    {
        messages.AddRange(new[]
        {
            new Message(Sender.Player, "hola"),
            new Message(Sender.Bot, "�c�mo est�s?"),
            new Message(Sender.Player, "Bien"),
            new Message(Sender.Bot, "BUENISIMO"),
            new Message(Sender.Player, "�c�mo est�s?"),
        });

    }
    public void SendMessagePlayer(string message)
    {
        messages.Add(new Message(Sender.Player, message));
    }

    public void ReceiveMessageBot(string message)
    {
        messages.Add(new Message(Sender.Bot, message));
    }
}

[System.Serializable]
public class Message
{
    public Sender sender;  // "Bot" o "Player"
    public string text;

    public Message(Sender sender, string text)
    {
        this.sender = sender;
        this.text = text;
    }
}

public enum Sender
{
    Player, Bot
}
