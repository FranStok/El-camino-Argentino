using TMPro;
using UnityEngine;

public class TextUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoUI;

    public void SetText(string texto)
    {
        this.textoUI.text = texto;
    }
}
