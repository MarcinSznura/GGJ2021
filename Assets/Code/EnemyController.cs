using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private PlayerController player = null;
    private NavMeshAgent navMeshAgent = null;
    private EnemyHealth health = null;

    private bool canAttack = true;

    private void Awake()
    {
        if (TryGetComponent(out navMeshAgent) == false)
        {
            Debug.Log("NavMeshAgent not found on enemy " + gameObject.name);
        }

        if (TryGetComponent(out health) == false)
        {
            Debug.Log("Enemy is missing health! " + gameObject.name);
        }
    }

    void Start()
    {
        player = PlayerController.Instance;

        if (player == null)
        {
            Debug.LogError("Player instance not found. disabling this enemy " + gameObject);
            enabled = false;
        }

        navMeshAgent.Warp(gameObject.transform.position);

        StartCoroutine(handleEnemy());
    }


    private IEnumerator handleEnemy()
    {
        while (health.CurrentHealth > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 3f)
            {
                MoveTo(player.transform.position);
            }
            else
            {
                Stop();
                
                if (canAttack)
                {
                    attack();
                }
            }

            yield return null;
        }


    }

    private void attack() //TODO make EnemyAttack from this
    {
        player.GetComponent<PlayerHealth>().TakeDamage(1);
        canAttack = false;
    }

    public void MoveTo(Vector3 _destination)
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = _destination;
            navMeshAgent.speed = 6f;
        }
    }

    public void Stop()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.isStopped = true; 
        }
    }
}
