using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject[] attackStage = new GameObject[0];
    [SerializeField] float timeBeetweenAttackStages = 1f;

    private WaitForSeconds waitForTimeBetweenAttacks = null;

    private bool canAttack = true;

    private void Start()
    {
        for (int i = 0; i < attackStage.Length; i++)
        {
            attackStage[i].SetActive(false);
        }

        waitForTimeBetweenAttacks = new WaitForSeconds(timeBeetweenAttackStages);
    }

    private void Update()
    {
        if (canAttack  && Input.GetButtonDown("Attack")) 
        {
            canAttack = false;

            StartCoroutine(attack());
            StartCoroutine(attackCoroutine());
        }
    }

    private IEnumerator attack()
    {
        for (int i =0; i< attackStage.Length;i++)
        {
            attackStage[i].SetActive(true);

            yield return waitForTimeBetweenAttacks;
        }

        yield return null;

        for (int i = 0; i < attackStage.Length; i++)
        {
            attackStage[i].SetActive(false);
        }
    }

    private IEnumerator attackCoroutine()
    {
        yield return new WaitForSeconds(2f);

        canAttack = true;
    }
}
