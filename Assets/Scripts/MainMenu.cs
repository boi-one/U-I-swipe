using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        MusicManager.Instance.PlayMusic("Main-Menu");
    }
    public void Speel()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}