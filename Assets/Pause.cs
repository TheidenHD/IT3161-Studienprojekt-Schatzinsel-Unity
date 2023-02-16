using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public void HauptM()
    {
        SceneManager.LoadScene(0);
    }

    public void Weiter()
    {
        Time.timeScale = 1.0f;
    }

    public void Bennden()
    {
        Application.Quit();
    }
}
