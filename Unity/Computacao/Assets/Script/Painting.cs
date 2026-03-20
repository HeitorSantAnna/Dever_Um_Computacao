using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Painting : MonoBehaviour
{
    public static Texture2D tex;

    private Renderer renderer;

    [SerializeField] Vector2 pos;

    public static int scaletexx, scaletexy;

    public static Color colorPaint; 

    [SerializeField] string name = "tela.ppm";

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

    void Vanish(Vector2 pos)
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

    public void ExportPPM()
    {
        string caminho = Application.dataPath + "/" + name;

        int width = tex.width;
        int height = tex.height;

        Color[] colors = tex.GetPixels();

        string header = $"P3\n{width} {height}\n255\n";

        using (StreamWriter writer = new StreamWriter(caminho))
        {
            writer.Write(header);
            
            for(int y = height - 1; y >= 0; y--)
            {
                for(int x = 0; x < width; x++)
                {
                    Color c = tex.GetPixel(x, y);

                    int r = Mathf.RoundToInt(c.r * 255);
                    int g = Mathf.RoundToInt(c.g * 255);
                    int b = Mathf.RoundToInt(c.b * 255);

                    writer.WriteLine($"{r} {g} {b}");
                }
            }
        }

        Debug.Log($"Arquivo salvo em: {caminho}");
    }
}
