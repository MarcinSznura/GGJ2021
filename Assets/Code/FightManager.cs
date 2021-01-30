using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] FightData[] fightData = new FightData[0];
    [Space(5f)]
    [SerializeField] float timeToEndLevel = 0f;

    private int waveNumber = 0;
    private int aliveEnemiesNumber = 0;

    private GameplayUI UI = null;

    private void Awake()
    {
        UI = FindObjectOfType<GameplayUI>();
    }

    void Start()
    {
        spawnEnemies();
    }

    private void spawnEnemies()
    {
        if (waveNumber < fightData.Length)
        {
            List<EnemyToSpawn> _enemiesToSpawn = fightData[waveNumber].enemiesToSpawn;

            for (int i = 0; i < _enemiesToSpawn.Count; i++)
            {
                GameObject _spawnedEnemy = Instantiate(_enemiesToSpawn[i].enemyToSpawn, _enemiesToSpawn[i].positionsToSpawn);
                aliveEnemiesNumber++;

                EnemyHealth _eh = _spawnedEnemy.GetComponent<EnemyHealth>();
                _eh.OnEnemyDeath += decreaseAliveEnemyNumber;
            }

            waveNumber++;
        }
        else
        {
            StartCoroutine(showVictoryScreen());
        }
    }

    private IEnumerator showVictoryScreen()
    {
        yield return new WaitForSeconds(timeToEndLevel);

        UI.ShowVictoryScreen();
        PlayerController.Instance.gameObject.SetActive(false);
    }

    private void decreaseAliveEnemyNumber(EnemyHealth _enemyHealth)
    {
        _enemyHealth.OnEnemyDeath -= decreaseAliveEnemyNumber;
        aliveEnemiesNumber--;

        if (aliveEnemiesNumber <= 0)
        {
            spawnEnemies();
        }
    }
}

#region Classes to spawn enemy

[System.Serializable]
public class FightData
{
    public List<EnemyToSpawn> enemiesToSpawn = new List<EnemyToSpawn>();
    public float TimeToWaitTillSpawn = 0f;
}

[System.Serializable]
public struct EnemyToSpawn
{
    public GameObject enemyToSpawn;
    public Transform positionsToSpawn;
}

#endregion
