using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Config : MonoBehaviour
{
    [SerializeField] int scalex, scaley;

    [SerializeField] TMP_InputField txtscalex, txtscaley;

    [SerializeField] TextMeshProUGUI aviso;

    void Start()
    {
        
    }

    void Update()
    {
        scalex = int.Parse(txtscalex.text);

        scaley = int.Parse(txtscaley.text);
    }

    public void Play(string scene)
    {
        if (scalex == 0 || scaley == 0)
        {
            aviso.text = "Não pode começar antes de dizer a escla da tela de pintura";
        }
        else
        {
            Painting.scaletexy = scaley;
            Painting.scaletexx = scalex;
            SceneManager.LoadScene(scene);
        }
    }

    public void Configuration(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
