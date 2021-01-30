using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] ParticleSystem damageAreaParticle = null;

    [SerializeField] float timeToWaitAfterAttack = 0f;
    [SerializeField] float timeToChargeAttack = 0f;
    [SerializeField] float distanceToAttack = 3f;

    [SerializeField] GameObject damageArea = null;
    [SerializeField] float timeToHoldDamageArea = 0f;

    private PlayerController player = null;
    private NavMeshAgent navMeshAgent = null;
    private EnemyHealth health = null;

    private bool canAttack = true;
    private bool isAttacking = false;
    private bool isWaiting = false;

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

        damageArea.SetActive(false);
    }


    private IEnumerator handleEnemy()
    {
        while (health.CurrentHealth > 0)
        {
            while (Vector3.Distance(transform.position, player.transform.position) > distanceToAttack)
            {
                MoveTo(player.transform.position);

                yield return null;
            }

            Stop();

            yield return new WaitForSeconds(timeToChargeAttack);

            StartCoroutine(attack());

            yield return new WaitForSeconds(timeToWaitAfterAttack);

            yield return null;
        }
    }

    private IEnumerator attack()
    {
        damageArea.SetActive(true);
        damageAreaParticle.Play();

        yield return new WaitForSeconds(timeToHoldDamageArea);

        damageAreaParticle.Stop();
        damageArea.SetActive(false);
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
