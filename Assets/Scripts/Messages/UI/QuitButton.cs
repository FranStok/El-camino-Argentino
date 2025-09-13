using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class QuitButton : MonoBehaviour
    {
        public void onQuitPressed()
        {
            SceneManager.LoadScene("Museo");
        }
        
    }
}

