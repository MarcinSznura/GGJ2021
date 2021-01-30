using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHP = null;
    [SerializeField] GameObject gameOverGameObject = null;
    [SerializeField] GameObject victoryScreenGameObject = null;

    private void Start()
    {
        if (PlayerPersistantStats.Instance == null)
        {
            Debug.LogError("GameManager Instance Is Missing!!!");
            enabled = false;
        }

        if (victoryScreenGameObject != null)
        {
            victoryScreenGameObject.SetActive(false);
        }

        if (gameOverGameObject != null)
        {
            gameOverGameObject.SetActive(false);
        }

        PlayerController.Instance.PlayerHealth.OnPlayerDeath += showGameOverScreen;
    }

    private void OnDestroy()
    {
        PlayerController.Instance.PlayerHealth.OnPlayerDeath -= showGameOverScreen;
    }

    public void UpdateUI(int _playerHP, int _maxplayerHP)
    {
        playerHP.text = "HP: " + _playerHP.ToString() + "/" + _maxplayerHP;
    }

    private void showGameOverScreen()
    {
        PlayerController.Instance.PlayerHealth.OnPlayerDeath -= showGameOverScreen;

        if (gameOverGameObject != null)
        {
            gameOverGameObject.SetActive(true);
        }
    }

    public void ShowVictoryScreen()
    {
        if (victoryScreenGameObject != null)
        {
            victoryScreenGameObject.SetActive(true);
        }
    }

    public void AdditionalDash()
    {
        PlayerPersistantStats.Instance.AdditionalDashNumber++;
        PlayerPersistantStats.Instance.LoadNextLevel();
    }

    public void AdditionalSpecial()
    {
        PlayerPersistantStats.Instance.AdditionalSpecialNumber++;
        PlayerPersistantStats.Instance.LoadNextLevel();
    }

    public void AdditionalDamage()
    {
        PlayerPersistantStats.Instance.AdditionalAttackDamage++;
        PlayerPersistantStats.Instance.LoadNextLevel();
    }
}
