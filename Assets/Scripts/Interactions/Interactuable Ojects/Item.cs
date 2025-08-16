using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string InteractMessage => "Presione E para intectuar";

    public void Interact(){
        Debug.Log("PRESIONADO");
    }
}
