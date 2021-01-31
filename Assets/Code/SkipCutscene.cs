using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] int indexOfLevelToLoad = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Attack") || Input.GetButtonDown("Special"))
        {
            SceneManager.LoadScene(indexOfLevelToLoad);
        }
    }
}
