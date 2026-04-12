using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Criar : MonoBehaviour
{
    private string name = "Arte.ppm";

    public int scaletexx, scaletexy;

    [SerializeField] Texture2D tex;

    public static List<GameObject> OBJtroca = new List<GameObject>();

    [SerializeField] List<int> valor1 = new List<int>();

    public static List<Color> Colortroca = new List<Color>();

    [SerializeField] GameObject tela;

    [SerializeField] Renderer alvo;

    private void Start()
    {
        tex = Painting.tex;

        scaletexy = Painting.scaletexy;

        scaletexx = Painting.scaletexx;
    }

    public void ExportPPM()
    {
                string caminho = Application.dataPath + "/" + "Image/" + name;

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

                            writer.Write($"{r} {g} {b} ");
                        }

                        writer.Write("\n");
                    }
                }

                Debug.Log($"Arquivo salvo em: {caminho}");
    }

    public void ImportarPPM()
    {
        string caminho = Application.dataPath + "/Image/" + name;

        if(!File.Exists(caminho))
        {
            Debug.Log("Não foi possivel encontrar o arquivo");
            return;
        }

        using (StreamReader reader = new StreamReader(caminho))
        {
            string tipo = reader.ReadLine();

            if(tipo != "P3")
            {
                Debug.Log("Esse arquivo não é P3");
                return;
            }

            string line = reader.ReadLine();

            while(!string.IsNullOrEmpty(line) && line.StartsWith("#"))
            {
                line = reader.ReadLine();
            }

            string[] dim = line.Split(" ");
            int largura = int.Parse(dim[0]);
            int altura = int.Parse(dim[1]);

            int maxVal = int.Parse(reader.ReadLine());

            Texture2D texture = new Texture2D(largura, altura);

            texture.filterMode = FilterMode.Point;

            alvo = tela.GetComponent<Renderer>();

            alvo.material.mainTexture = texture;

            List<int> valores = new List<int>();

            while(!reader.EndOfStream)
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

            for(int i = 0; i <= valores.Count - 3; i+= 3)
            {
                int r = valores[i];
                int g = valores[1 + i];
                int b = valores[i + 2];

                pixels.Add(new Color(r / 255f, g / 255f, b / 255f));
            }

            int width = texture.width;
            int height = texture.height;

            int index = 0;

            for(int y = height - 1; y >= 0; y--)
            {
                for(int x = 0; x < width; x++)
                {
                    texture.SetPixel(x, y, pixels[index]);
                    index++;
                }
            }

            texture.Apply(false);

            //Apartir daqui serve para instanciar os objetos

            for(int i = 0; i < OBJtroca.Count; i++)
            {
                OBJtroca[i].SetActive(true);
            }

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color c = texture.GetPixel(x, y);

                    float tamanhoplano = 10;

                    float u = (float)x / width;
                    float v = (float)y / height;

                    float posX = -(u - 0.5f) * tamanhoplano;
                    float posY = -(v - 0.5f) * tamanhoplano;

                    Vector3 localpos = new Vector3(posX, 0, posY);
                    Vector3 localWorld = tela.transform.TransformPoint(localpos);

                    if(Colortroca.Contains(c))
                    {
                        int indexs = Colortroca.IndexOf(c);

                        GameObject obj = Instantiate(OBJtroca[indexs], localWorld, Quaternion.identity);

                        float escalax = tamanhoplano / texture.width;
                        float escalaz = tamanhoplano / texture.height;

                        obj.transform.localScale = new Vector3(escalax, escalax, escalaz * -1);
                    }
                }
            }

            tela.SetActive(false);
        }
    }
}
