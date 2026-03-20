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

        changeColor.SetActive(true);
    }

    public void OnPaintTexture()
    {
        principal.Enable();

        changeColor.SetActive(false);
    }

    public void Refer()
    {
        principal.Disable();

        tela.transform.position = new Vector3(2, 1, 0);

        refer.SetActive(true);
    }
}
