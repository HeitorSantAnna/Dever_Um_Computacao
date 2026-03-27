using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class Criar : MonoBehaviour
{
    private string name = "tela.ppm";

    public static int scaletexx = Painting.scaletexx, scaletexy = Painting.scaletexy;

    Texture2D tex;

    //[SerializeField] Color[,] colorim;

    [SerializeField] List<Color> cor = new List<Color>();

    [SerializeField] List<GameObject> gotroca = new List<GameObject>();

    [SerializeField] List<int> valor1 = new List<int>();

    [SerializeField] List<int> valor2 = new List<int>();

    [SerializeField] GameObject tela;

    private Renderer renderer;

    private void Start()
    {
        tex = Painting.tex;

        scaletexy = Painting.scaletexy;

        scaletexx = Painting.scaletexx;

        //colorim = new Color[scaletexx, scaletexy];

        renderer = tela.GetComponent<Renderer>();

        for (int i = 0; i < Painting.colors.Count; i++)
        {
            cor.Add(Painting.colors[i]);
        }

        for(int i = 0; i < Troca.valor1.Count; i++)
        {
            valor1.Add(Troca.valor1[i]);
        }

        for(int i = 0; i < Troca.valor2.Count; i++)
        {
            valor2.Add(Troca.valor2[i]);
        }

        for(int i = 0; i < Troca.envio.Count; i++)
        {
            gotroca.Add(Troca.envio[i]);
        }
    }

    public void ExportPPM()
    {
        string caminho = Application.dataPath + "/" + name;

        int width = tex.width;
        int height = tex.height;

        string header = $"P3\n{width} {height}\n255\n";

        using (StreamWriter writer = new StreamWriter(caminho))
        {
            writer.Write(header);

            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = tex.GetPixel(x, y);

                    int r = Mathf.RoundToInt(c.r * 255);
                    int g = Mathf.RoundToInt(c.g * 255);
                    int b = Mathf.RoundToInt(c.b * 255);

                    //colorim[width, height] = c;

                    writer.Write($"{r} {g} {b} ");
                }

                writer.Write("\n");
            }
        }

        Debug.Log($"Arquivo salvo em: {caminho}");
    }

    public void ImportarPPM()
    {
        string caminho = Application.dataPath + "/" + name;

        int width = tex.width;
        int height = tex.height;

        using (StreamReader reader = new StreamReader(caminho))
        {
            //Aqui vou precisar colocar o script para ler a imagem

            //Até aqui
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = tex.GetPixel(x, y);
                       
                    if(cor.Contains(c))
                    {
                        int ind = cor.IndexOf(c);

                        int inde = valor2.IndexOf(ind);

                        int index = valor1[inde];

                        float u = x / (float)width;
                        float v = 1f - (y / (float)height);

                        Vector3 size = tela.GetComponent<Renderer>().bounds.size;

                        Vector3 local = new Vector3((u - 0.5f) * size.x, 0, (v - 0.5f) * size.z);

                        Vector3 worldPos = tela.transform.TransformPoint(local);

                        Instantiate(gotroca[index], worldPos, Quaternion.identity);
                    }
                }
            }
        }

        Debug.Log("O caminho reverso deu certo");
    }
}
