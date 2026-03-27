using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeModes : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    [SerializeField] InputActionMap principal;

    [SerializeField] GameObject changeColor, refer, tela;

    void Start()
    {
        principal = inputActions.FindActionMap("Create");

        principal.Enable();
    }

    void Update()
    {
        
    }

    public void OnChangeColor()
    {
        principal.Disable();
    }

    public void OnPaintTexture()
    {
        principal.Enable();
    }

    public void Refer()
    {
        principal.Disable();
    }

    public void Return()
    {
        principal.Enable();
    }
}
