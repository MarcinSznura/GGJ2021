using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance = null;

    [HideInInspector] public bool canSpecialAttack = true;

    [Header("Attack")]
    public int AttackDamage = 1;
    [SerializeField] GameObject[] attackStage = new GameObject[0];
    [SerializeField] float timeBeetweenAttackStages = 1f;

    [Header("Special attack")]
    [SerializeField] GameObject specialPrefab = null;
    [SerializeField] GameObject specialPrefabToHide = null;
    [SerializeField] Transform specialAttackSpawnPosition = null;
    public float SpecialAttackDistance = 1f;
    public int SpecialAttackDamage = 1;

    private Animator animator = null;
    private WaitForSeconds waitBetweenAttacks = null;

    private bool canAttack = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        for (int i = 0; i < attackStage.Length; i++)
        {
            attackStage[i].SetActive(false);
        }

        waitBetweenAttacks = new WaitForSeconds(timeBeetweenAttackStages);
    }

    private void Update()
    {
        if (canAttack  && Input.GetButtonDown("Attack")) 
        {
            canAttack = false;

            StartCoroutine(attack());
            StartCoroutine(attackCooldownCoroutine());
        }

        if (canSpecialAttack && Input.GetButtonDown("Special"))
        {
            canSpecialAttack = false;

            executeSpecialAttack();
        }
    }

    private IEnumerator attack()
    {
        for (int i =0; i< attackStage.Length;i++)
        {
            attackStage[i].SetActive(true);

            yield return waitBetweenAttacks;
        }

        yield return null;

        for (int i = 0; i < attackStage.Length; i++)
        {
            attackStage[i].SetActive(false);
        }
    }

    private void executeSpecialAttack()
    {
        StartCoroutine(special());
    }

    private IEnumerator special()
    {
        animator.SetTrigger("Special");
        canSpecialAttack = false;

        yield return new WaitForSeconds(0.2f);

        Instantiate(specialPrefab, specialAttackSpawnPosition.position, Quaternion.identity, null); //TODO: objectPool after jam?
        specialPrefabToHide.SetActive(false);
    }

    public void OnSpecialReturn()
    {
        specialPrefabToHide.SetActive(true);
        canSpecialAttack = true;
    }
    private IEnumerator attackCooldownCoroutine()
    {
        yield return new WaitForSeconds(2f);

        canAttack = true;
    }
}
