using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    public static Texture2D tex;

    private Renderer renderer;

    [SerializeField] bool line = false, po = false;

    [SerializeField] Vector2 posline = new Vector2();

    [SerializeField] Vector2 pos;

    public static int scaletexx, scaletexy;

    public static Color colorPaint;

    public static List<Color> colors = new List<Color>();

    [SerializeField] GameObject game;

    [SerializeField] Image image;

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
                tex.SetPixel(i, j, Color.clear);
            }
        }

        tex.Apply(false);
    }

    public void PantAndVanish(InputAction.CallbackContext value)
    {
        pos = value.ReadValue<Vector2>();

        if (Keyboard.current.leftShiftKey.isPressed && Mouse.current.leftButton.isPressed)
        {
            PaintLine(pos);
            line = true;
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            if (!line)
            {
                Paint(pos);
            }
            else if(line)
            {
                po = true;
                PaintLine(pos);
            }
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

    void PaintLine(Vector2 pos)
    {
        if (po == false)
        {
            posline = pos;

            RaycastHit hit;

            Ray ray;

            ray = Camera.main.ScreenPointToRay(posline);

            if(Physics.Raycast(ray, out hit))
            {
                posline.x = hit.textureCoord.x * scaletexx;
                posline.y = hit.textureCoord.y * scaletexy;

                tex.SetPixel((int)posline.x, (int)posline.y, colorPaint);

                tex.Apply(false);
            }
        }
        else if(po == true)
        {
            RaycastHit hit;

            Ray ray;

            ray = Camera.main.ScreenPointToRay(pos);

            if(Physics.Raycast(ray, out hit))
            {
                Vector2 Texhit = hit.textureCoord;
                Texhit.x *= scaletexx;
                Texhit.y *= scaletexy;

                float distance = Vector2.Distance(posline, Texhit);

                for (int i = 0; i < distance; i++)
                {
                    float t = i / distance;

                    float x = Mathf.Lerp(posline.x, Texhit.x, t);
                    float y = Mathf.Lerp(posline.y, Texhit.y, t);

                    tex.SetPixel((int)x, (int)y, colorPaint);
                }

                tex.Apply(false);
                line = false;
                po = false;
            }
        }

        Debug.Log(po);
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

    public void InfoColors()
    {
        colors.Clear();

        int width = tex.width;
        int height = tex.height;

        for(int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = tex.GetPixel(x, y);

                if(!colors.Contains(c))
                {
                    colors.Add(c);
                }
            }
        }
    }
}
