using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public List<Message> messages = new List<Message>();

    public void SendMessagePlayer(string message){
        messages.Add(new Message(Sender.player,message));
    }

    public void ReceiveMessageBot(string message)
    {
        messages.Add(new Message(Sender.bot, message));
    }
}

[System.Serializable]
public class Message
{
    public Sender sender;  // "NPC" o "Player"
    public string text;

    public Message(Sender sender, string text)
    {
        this.sender = sender;
        this.text = text;
    }
}

public enum Sender
{
    player, bot
}
