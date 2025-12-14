using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class QuitButton : MonoBehaviour
    {
        [SerializeField] private MessageSender  _messageSender;
        public void onQuitPressed()
        {
            if(!_messageSender.isProcessing) SceneManager.LoadScene("Museo");
        }
        
    }
}

