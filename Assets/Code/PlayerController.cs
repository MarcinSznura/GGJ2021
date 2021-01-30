using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance = null;

    [SerializeField] float speed = 1f;
    [SerializeField] float dashDistance = 1f;
    [SerializeField] GameObject model = null;

    [SerializeField] int maxDashNumber = 1;
    [SerializeField] float dashCooldownDurations = 1f;
    private bool canDash = true;
    private int dashNumberLeft = 0;

    private WaitForSeconds dashCooldownTime = null;

    private CharacterController controller = null;
    private Animator animator = null;
    private PlayerHealth health = null;
    private Collider playerCollider = null;

    public Vector3 PlayerForward => model.transform.forward;
    public PlayerHealth PlayerHealth => health;

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

        TryGetComponent(out controller);
        TryGetComponent(out health);
        TryGetComponent(out playerCollider);

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (PlayerPersistantStats.Instance != null)
        {
            maxDashNumber = maxDashNumber + PlayerPersistantStats.Instance.AdditionalDashNumber;
        }

        dashNumberLeft = maxDashNumber;
        dashCooldownTime = new WaitForSeconds(dashCooldownDurations);
    }

    void Update()
    {
        float _x = Input.GetAxis("Horizontal");
        float _z = Input.GetAxis("Vertical");

        Vector3 _move = transform.right * _x + transform.forward * _z;

        controller.Move(_move * speed * Time.deltaTime);

        animator.SetFloat("Walk", _move.magnitude);

        if (Input.GetButtonDown("Jump"))
        {
            if (canDash)
            {
                canDash = false;
                StartCoroutine(dashCooldown());
            }

            if (dashNumberLeft > 0)
            {
                dashNumberLeft--;
                StartCoroutine(dash());
            }
        }

        if (_move != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(_move);
        }
    }

    private IEnumerator dash()
    {
        animator.SetTrigger("Dash");

        health.isInvulnerable = true;
        speed = speed * 3f;

        yield return new WaitForSeconds(0.2f);

        health.isInvulnerable = false;
        speed = speed / 3f;

        playerCollider.enabled = false;
        playerCollider.enabled = true;

        animator.SetTrigger("DashEnd");
    }

    private IEnumerator dashCooldown()
    {
        yield return dashCooldownTime;

        canDash = true;
        dashNumberLeft = maxDashNumber;
    }
}
