using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance = null;

    [Header("Attack")]
    public int AttackDamage = 1;
    [SerializeField] GameObject attackArea = null;
    [SerializeField] float timeBeforeDamageArea = 1f;
    [SerializeField] float timeToHoldDamageArea = 1f;

    [Header("Special attack")]
    [SerializeField] GameObject specialPrefab = null;
    [SerializeField] GameObject specialPrefabToHide = null;
    [SerializeField] Transform specialAttackSpawnPosition = null;
    public float SpecialAttackDistance = 1f;
    public int SpecialAttackDamage = 1;

    private int maxSpecialNumber = 0;
    private int currentSpecialNumber = 0;

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
        attackArea.SetActive(false);

        maxSpecialNumber = PlayerPersistantStats.Instance == null ? 1 : 1 + PlayerPersistantStats.Instance.AdditionalSpecialNumber;
        currentSpecialNumber = maxSpecialNumber;

        waitBetweenAttacks = new WaitForSeconds(timeToHoldDamageArea);
    }

    private void Update()
    {
        if (canAttack  && Input.GetButtonDown("Attack")) 
        {
            canAttack = false;

            StartCoroutine(attack());
            StartCoroutine(attackCooldownCoroutine());
        }

        if (currentSpecialNumber > 0 && Input.GetButtonDown("Special"))
        {
            currentSpecialNumber--;

            executeSpecialAttack();
        }
        
        if (currentSpecialNumber <= 0)//TODO do this on throw, no time now 
        {
            specialPrefabToHide.SetActive(false);
        }
        else
        {
            specialPrefabToHide.SetActive(true);
        }
    }

    private IEnumerator attack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(timeBeforeDamageArea);

        attackArea.SetActive(true);

        yield return waitBetweenAttacks;

        attackArea.SetActive(false);
    }

    private void executeSpecialAttack()
    {
        StartCoroutine(special());
    }

    private IEnumerator special()
    {
        animator.SetTrigger("Special");

        yield return new WaitForSeconds(0.2f);

        Instantiate(specialPrefab, specialAttackSpawnPosition.position, Quaternion.identity, null); //TODO: objectPool after jam?
    }

    public void OnSpecialReturn()
    {
        currentSpecialNumber++;
    }
    private IEnumerator attackCooldownCoroutine()
    {
        yield return new WaitForSeconds(2f);

        canAttack = true;
    }
}
