using UnityEngine;
using UnityEngine.UI;

public class InitScoll : MonoBehaviour
{
    void Start()
    {
        GetComponent<ScrollRect>().normalizedPosition=new Vector2(0,0);
    }


}
