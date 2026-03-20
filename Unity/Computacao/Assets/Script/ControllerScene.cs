using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScene : MonoBehaviour
{
    public void Voltar(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
