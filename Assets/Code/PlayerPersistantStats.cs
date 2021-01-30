using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistantStats : MonoBehaviour
{
    public static PlayerPersistantStats Instance = null;

    public int AdditionalDashNumber = 0;
    public int AdditionalSpecialNumber = 0;
    public int AdditionalAttackDamage = 0;

    private int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

    public void RestartAdditionalStats()
    {
        currentLevel = 0;

        AdditionalAttackDamage = 0;
        AdditionalDashNumber = 0;
        AdditionalSpecialNumber = 0;
    }
}
