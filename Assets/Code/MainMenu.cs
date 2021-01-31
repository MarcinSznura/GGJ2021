using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(int _index)
    {
        SceneManager.LoadScene(_index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
