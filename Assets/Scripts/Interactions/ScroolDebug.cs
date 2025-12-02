using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollDebug : MonoBehaviour
{
    public void OnScrollChanged(Vector2 value)
    {
        Debug.Log("Scroll value: " + value);
    }
}