using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatManager : MonoBehaviour
{
    [SerializeField] [CanBeNull] private GameObject messagesGameObject;
    [CanBeNull] private MessagesLoader _messagesLoader;

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

        Instance._messagesLoader = messagesGameObject?.GetComponent<MessagesLoader>();

    }
    public void SendMessagePlayer(string message)
    {
        Message newMessage = new Message(Sender.Player, message);
        messages.Add(newMessage);
        if (SceneManager.GetActiveScene().name == "Chat" && _messagesLoader != null) _messagesLoader.LoadMessages(new List<Message> { newMessage });

    }

    public void ReceiveMessageBot(string message)
    {
        Message newMessage = new Message(Sender.Bot, message);
        messages.Add(newMessage);
        if (SceneManager.GetActiveScene().name == "Chat" && _messagesLoader != null) _messagesLoader.LoadMessages(new List<Message> { newMessage });
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
