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

    public void Play(string scene)
    {
        if (scalex == 0 || scaley == 0)
        {
            aviso.text = "Não pode começar antes de dizer a escla da tela de pintura";
        }
        else
        {
            Painting.scaletexy = int.Parse(txtscaley.text);
            Painting.scaletexx = int.Parse(txtscalex.text);
            SceneManager.LoadScene(scene);
        }
    }

    public void Configuration(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Imp(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
