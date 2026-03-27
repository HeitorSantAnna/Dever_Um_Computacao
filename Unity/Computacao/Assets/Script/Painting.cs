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

    private bool line = false;

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
                tex.SetPixel(i, j, Color.gray);
            }
        }

        tex.Apply(false);
    }

    public void PantAndVanish(InputAction.CallbackContext value)
    {
        pos = value.ReadValue<Vector2>();

        if(Mouse.current.leftButton.isPressed)
        {
            if (!line)
            {
                Paint(pos);
            }
            else if(line)
            {
                Line(pos);
            }
        }
        else if(Mouse.current.rightButton.isPressed)
        {
            Vanish(pos);
        }
        else if(Keyboard.current.leftShiftKey.isPressed && Mouse.current.rightButton.isPressed)
        {
            PaintLine(pos);
            line = true;
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
        RaycastHit hit;

        //s[0] = pos;

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

    void Line(Vector2 pos)
    {
        //s[1] = pos;

        RaycastHit hit;

        Ray ray;

        ray = Camera.main.ScreenPointToRay(pos);

        /*if (Physics.Linecast(s[0], s[1], out hit))
        {
            float x = hit.textureCoord.x * scaletexx;
            float y = hit.textureCoord.y * scaletexy;

            tex.SetPixel((int)x, (int)y, colorPaint);

            tex.Apply(false);
        }*/
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
        //Aqui é para pegar as cores
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
        //Até aqui
    }
}
