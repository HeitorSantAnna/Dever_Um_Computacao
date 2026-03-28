using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Criar : MonoBehaviour
{
    private string name = "tela.ppm";

    public int scaletexx, scaletexy;

    [SerializeField] Texture2D tex;

    [SerializeField] List<Color> cor = new List<Color>();

    [SerializeField] List<GameObject> gotroca = new List<GameObject>();

    [SerializeField] List<int> valor1 = new List<int>();

    [SerializeField] List<Color> valor2 = new List<Color>();

    [SerializeField] GameObject tela;

    [SerializeField] Renderer alvo;

    private void Start()
    {
        tex = Painting.tex;

        scaletexy = Painting.scaletexy;

        scaletexx = Painting.scaletexx;

        /*for (int i = 0; i < Painting.colors.Count; i++)
        {
            cor.Add(Painting.colors[i]);
        }

        for (int i = 0; i < Troca.valor1.Count; i++)
        {
            valor1.Add(Troca.valor1[i]);
        }

        for (int i = 0; i < Troca.valor2.Count; i++)
        {
            valor2.Add(Troca.valor2[i]);

            Debug.Log("Colocado 2");
        }

        for (int i = 0; i < Troca.valor1.Count; i++)
        {
            gotroca.Add(Troca.valor1[i]);
        }*/
        cor.AddRange(Painting.colors);
        valor1.AddRange(Troca.valor1);
        valor2.AddRange(Troca.valor2);
        gotroca.AddRange(Troca.valor1);

        for(int x = 0; x < cor.Count; x++)
        {
            Debug.Log($"{cor[x]}");
        }

        for (int x = 0; x < valor1.Count; x++)
        {
            Debug.Log($"{valor1[x]}");
        }

        for (int x = 0; x < valor2.Count; x++)
        {
            Debug.Log($"{valor2[x]}");
        }

        for (int x = 0; x < gotroca.Count; x++)
        {
            Debug.Log($"{gotroca[x]}");
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
        /*string caminho = Application.dataPath + "/" + name;

        if (!File.Exists(caminho))
        {
            Debug.LogError("Arquivo não encontrado: " + caminho);
            return;
        }

        using (StreamReader reader = new StreamReader(caminho))
        {
            string tipo = reader.ReadLine();

            if (tipo != "P3")
            {
                Debug.LogError("Formato não suportado! Use P3.");
                return;
            }

            string linha = reader.ReadLine();

            while (linha.StartsWith("#"))
            {
                linha = reader.ReadLine();
            }

            string[] dim = linha.Split(' ');
            int largura = int.Parse(dim[0]);
            int altura = int.Parse(dim[1]);

            int max = int.Parse(reader.ReadLine());

            List<int> valores = new List<int>();

            while (!reader.EndOfStream)
            {
                string linhaPixels = reader.ReadLine();
                string[] partes = linhaPixels.Split(' ');

                foreach (string p in partes)
                {
                    if (!string.IsNullOrWhiteSpace(p))
                    {
                        valores.Add(int.Parse(p));
                    }
                }
            }

            List<Color> pixels = new List<Color>();

            for (int i = 0; i < valores.Count; i += 3)
            {
                int r = valores[i];
                int g = valores[i + 1];
                int b = valores[i + 2];

                pixels.Add(new Color(r / 255f, g / 255f, b / 255f));
            }

            if (pixels.Count < largura * altura)
            {
                Debug.LogError("Imagem PPM incompleta!");
                return;
            }

            Texture2D tex = new Texture2D(largura, altura);

            tex.filterMode = FilterMode.Point;

            int index = 0;

            for (int y = altura - 1; y >= 0; y--)
            {
                for (int x = 0; x < largura; x++)
                {
                    tex.SetPixel(x, y, pixels[index]);
                    index++;
                }
            }

            tex.Apply();

            if (alvo != null)
            {
                alvo.material.mainTexture = tex;
            }

            Debug.Log("PPM carregado corretamente!");
        }

        //Aqui vai substituir as cores por objetos
        int xt = tex.width, yt = tex.height;

        for(int u = yt - 1; u >= 0; u--)
        {
            for(int v = 0; v < xt; v++)
            {
                Color c = tex.GetPixel(u, v);

                if (cor.Contains(c))
                {
                    //Pegar a cor, depois pegar o indice da cor
                    int index = valor2.IndexOf(c);

                    Vector3 pos = new Vector3(u, 0, v);

                    Instantiate(gotroca[index], pos, Quaternion.identity);
                }
            }
        }

        tela.SetActive(false);

        int xt = tex.width;
        int yt = tex.height;

        bool CorIgual(Color a, Color b)
        {
            return Mathf.Abs(a.r - b.r) < 0.01f &&
                   Mathf.Abs(a.g - b.g) < 0.01f &&
                   Mathf.Abs(a.b - b.b) < 0.01f;
        }

        for (int u = yt - 1; u >= 0; u--)
        {
            for (int v = 0; v < xt; v++)
            {
                Color c = tex.GetPixel(v, u);

                for (int i = 0; i < cor.Count; i++)
                {
                    if (CorIgual(cor[i], c))
                    {
                        int inde = valor2.IndexOf(cor[i]);
                        if (inde == -1) continue;

                        int index = valor1[inde];

                        float uNorm = v / (float)xt;
                        float vNorm = u / (float)yt;

                        Vector3 size = tela.GetComponent<Renderer>().bounds.size;

                        Vector3 local = new Vector3((uNorm - 0.5f) * size.x, 0, (vNorm - 0.5f) * size.z);

                        Vector3 worldPos = tela.transform.TransformPoint(local);

                        Instantiate(gotroca[index], worldPos, Quaternion.identity);
                    }
                }
            }
        }*/

        cor.AddRange(Painting.colors);
        valor1.AddRange(Troca.valor1);
        valor2.AddRange(Troca.valor2);
        gotroca.AddRange(Troca.valor1);

        string caminho = Application.dataPath + "/" + name;

        if (!File.Exists(caminho))
        {
            Debug.LogError("Arquivo não encontrado: " + caminho);
            return;
        }

        using (StreamReader reader = new StreamReader(caminho))
        {
            string tipo = reader.ReadLine();

            if (tipo != "P3")
            {
                Debug.LogError("Formato não suportado! Use P3.");
                return;
            }

            string linha = reader.ReadLine();

            while (linha.StartsWith("#"))
                linha = reader.ReadLine();

            string[] dim = linha.Split(' ');
            int largura = int.Parse(dim[0]);
            int altura = int.Parse(dim[1]);

            reader.ReadLine(); // max value

            List<int> valores = new List<int>();

            while (!reader.EndOfStream)
            {
                string linhaPixels = reader.ReadLine();
                string[] partes = linhaPixels.Split(' ');

                foreach (string p in partes)
                {
                    if (!string.IsNullOrWhiteSpace(p))
                        valores.Add(int.Parse(p));
                }
            }

            List<Color> pixels = new List<Color>();

            for (int i = 0; i < valores.Count; i += 3)
            {
                pixels.Add(new Color(
                    valores[i] / 255f,
                    valores[i + 1] / 255f,
                    valores[i + 2] / 255f
                ));
            }

            if (pixels.Count < largura * altura)
            {
                Debug.LogError("Imagem PPM incompleta!");
                return;
            }

            // ? CORREÇÃO: usar variável da classe
            tex = new Texture2D(largura, altura);
            tex.filterMode = FilterMode.Point;

            int index = 0;

            for (int y = altura - 1; y >= 0; y--)
            {
                for (int x = 0; x < largura; x++)
                {
                    tex.SetPixel(x, y, pixels[index]);
                    index++;
                }
            }

            tex.Apply();

            if (alvo != null)
                alvo.material.mainTexture = tex;
        }

        // ===== GERAR OBJETOS =====
        int xt = tex.width;
        int yt = tex.height;

        for (int u = yt - 1; u >= 0; u--)
        {
            for (int v = 0; v < xt; v++)
            {
                Color c = tex.GetPixel(v, u);

                for (int i = 0; i < cor.Count; i++)
                {
                    if (CorIgual(cor[i], c))
                    {
                        int inde = valor2.IndexOf(cor[i]);
                        if (inde == -1) continue;

                        if (inde >= valor1.Count || inde >= gotroca.Count) continue;

                        int prefabIndex = valor1[inde];

                        float uNorm = v / (float)xt;
                        float vNorm = u / (float)yt;

                        Vector3 size = tela.GetComponent<Renderer>().bounds.size;

                        Vector3 local = new Vector3(
                            (uNorm - 0.5f) * size.x,
                            0,
                            (vNorm - 0.5f) * size.z
                        );

                        Vector3 worldPos = tela.transform.TransformPoint(local);

                        Instantiate(gotroca[prefabIndex], worldPos, Quaternion.identity);
                    }
                }
            }
        }

        tela.SetActive(false);
        bool CorIgual(Color a, Color b)
        {
            return Mathf.Abs(a.r - b.r) < 0.01f &&
                   Mathf.Abs(a.g - b.g) < 0.01f &&
                   Mathf.Abs(a.b - b.b) < 0.01f;
        }
    }
}
