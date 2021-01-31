﻿using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] Slider healthBarSlider = null;

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
        playerHP.text = _playerHP.ToString() + "  " + _maxplayerHP;
        healthBarSlider.maxValue = _maxplayerHP;
        healthBarSlider.value = _playerHP;
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
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            if (victoryScreenGameObject != null)
            {
                victoryScreenGameObject.SetActive(true);
            }
        }
        else
        {
            PlayerPersistantStats.Instance.RestartAdditionalStats();
            SceneManager.LoadScene(5);
        }
    }

    public void AdditionalDash()
    {
        PlayerPersistantStats.Instance.AdditionalDashNumber++;
        PlayerPersistantStats.Instance.PlayerPreviousHealth = PlayerController.Instance.PlayerHealth.CurrentHealth;
        PlayerPersistantStats.Instance.LoadNextLevel();
        
    }

    public void AdditionalSpecial()
    {
        PlayerPersistantStats.Instance.AdditionalSpecialNumber++;
        PlayerPersistantStats.Instance.PlayerPreviousHealth = PlayerController.Instance.PlayerHealth.CurrentHealth;
        PlayerPersistantStats.Instance.LoadNextLevel();
    }

    public void AdditionalDamage()
    {
        PlayerPersistantStats.Instance.AdditionalAttackDamage++;
        PlayerPersistantStats.Instance.PlayerPreviousHealth = PlayerController.Instance.PlayerHealth.CurrentHealth;
        PlayerPersistantStats.Instance.LoadNextLevel();
    }
}
