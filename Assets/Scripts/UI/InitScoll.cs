using UnityEngine;
using UnityEngine.UI;

public class InitScoll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<ScrollRect>().normalizedPosition=new Vector2(0,0);
    }


}
