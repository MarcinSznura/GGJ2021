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
    private Collider playerCollider = null;
    private Animator animator = null;

    public Vector3 PlayerForward => model.transform.forward;

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
        TryGetComponent(out playerCollider);

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
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
                gameObject.layer = LayerMask.NameToLayer("Dash");
                controller.Move(model.transform.forward * dashDistance);
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        if (_move != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(_move);
        }
    }

    private IEnumerator dashCooldown()
    {
        yield return dashCooldownTime;

        canDash = true;
        dashNumberLeft = maxDashNumber;
    }
}
