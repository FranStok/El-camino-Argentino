using UnityEngine;
using UnityEngine.SceneManagement;
public class onQuitPressed : MonoBehaviour
{
    public void OnQuitPressed(){
        SceneManager.LoadScene("Museo");
    }
}
