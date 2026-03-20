using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Painting : MonoBehaviour
{
    public static Texture2D tex;

    private Renderer renderer;

    [SerializeField] Vector2 pos;

    public static int scaletexx, scaletexy;

    public static Color colorPaint;

    void Start()
    {
        tex = new Texture2D(scaletexx, scaletexy);

        tex.filterMode = FilterMode.Point;

        renderer = GetComponent<Renderer>();

        renderer.material.mainTexture = tex;

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                tex.SetPixel(i, j, Color.gray);
            }
        }

        tex.Apply(false);
    }

    void Update()
    {
    }

    public void PantAndVanish(InputAction.CallbackContext value)
    {
        pos = value.ReadValue<Vector2>();

        if(Mouse.current.leftButton.isPressed)
        {
            Paint(pos);
        }
        else if(Mouse.current.rightButton.isPressed)
        {
            Vanish(pos);
        }
    }

    void Paint(Vector2 pos)
    {
        RaycastHit hit;

        Ray ray;

        ray = Camera.main.ScreenPointToRay(pos);

        if(Physics.Raycast(ray, out hit))
        {
            float x = hit.textureCoord.x * scaletexx;
            float y = hit.textureCoord.y * scaletexy;

            tex.SetPixel((int)x, (int)y, colorPaint);

            tex.Apply(false);
        }
    }

    public void Vanish(Vector2 pos)
    {
        RaycastHit hit;

        Ray ray;

        ray = Camera.main.ScreenPointToRay(pos);

        if(Physics.Raycast(ray, out hit))
        {
            float x = hit.textureCoord.x * scaletexx;
            float y = hit.textureCoord.y * scaletexy;

            tex.SetPixel((int)x, (int)y, Color.clear);

            tex.Apply(false);
        }
    }
}
