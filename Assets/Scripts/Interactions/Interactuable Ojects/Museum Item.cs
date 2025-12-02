using UnityEngine;

public class MuseumItem : MonoBehaviour, IInteractable
{
    public string InteractMessage => "Presione E para intectuar";

    [SerializeField] string description;
    [SerializeField] string nombre;
    public void Interact(){
        OverlayManager.Instance.showOverlay(nombre,description);
    }
}
