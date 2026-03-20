using UnityEngine;

public class Analise : MonoBehaviour
{
    [SerializeField] Texture2D texture;

    [SerializeField] Renderer renderer;

    [SerializeField] GameObject com;

    [SerializeField] Transform context;

    private Color cor1, cor2;

    //private bool jaCriado = false;

    void Start()
    {
        texture = Painting.tex;

        texture.filterMode = FilterMode.Point;

        renderer = GameObject.Find("Tela").GetComponent<Renderer>();

        //Criar();

        Colocar();
    }

    void Colocar()
    {
        for(int i = 0; i < texture.width - 1; i++)
        {
            for(int j = 0; j < texture.height - 1; j++)
            {
                cor1 = texture.GetPixel(i, j);
                cor2 = texture.GetPixel(i + 1, j + 1);

                if (cor1 != cor2)
                {
                    GameObject corimg = Instantiate(com);

                    Renderer rend = corimg.GetComponent<Renderer>();

                    rend.material = new Material(rend.material);

                    rend.material.color = cor1;

                    corimg.transform.SetParent(context);
                }
            }
        }
    }

    /*void Criar()
    {
        if (!jaCriado)
        {
            com = GameObject.CreatePrimitive(PrimitiveType.Plane);

            com.transform.position = new Vector3(2, 1, 0);

            com.name = "Amostra";

            renderer.material.mainTexture = texture;
        }
        else if(jaCriado)
        {
            renderer.material.mainTexture = texture;
        }
    }*/

    void Update()
    {
        
    }
}
