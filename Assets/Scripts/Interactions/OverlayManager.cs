using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverlayManager : MonoBehaviour
{
    public static OverlayManager Instance { get; private set; }

    private string overlayTextDescription;
    private string overlayTextTitle;
    
    public bool isOpen = false;

    [SerializeField] private TextMeshProUGUI textoDescripcionUI;
    [SerializeField] private TextMeshProUGUI textoTituloUI;
    InputAction exitAction;
    
    private void Awake()
        
    {
        gameObject.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        exitAction = InputSystem.actions.FindAction("Exit Description");
    }

    private void Update()
    {
        if(exitAction.WasPressedThisFrame() && isOpen) hideOverlay();
    }

    public void showOverlay(string titulo,string descripcion)
    {
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        textoTituloUI.text = titulo;
        textoDescripcionUI.text = descripcion;
        isOpen = true;
        gameObject.SetActive(true);
    }

    public void hideOverlay()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
        isOpen = false;
    }
}
