using System.Collections;
using UnityEngine;

public class PlayersBoomerang : MonoBehaviour
{
    public float Speed = 1f;

    private Vector3 target = new Vector3();

    private void Start()
    {
        gameObject.transform.root.parent = null;
        target = transform.position + PlayerController.Instance.PlayerForward * PlayerAttack.Instance.SpecialAttackDistance;

        StartCoroutine(goForwardAndBack());
    }

    private IEnumerator goForwardAndBack()
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            transform.Rotate(Vector3.up, 3f);
            yield return null;
        }

        Vector3 _offset = PlayerController.Instance.transform.position - transform.position;

        while (_offset.sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, Speed * Time.deltaTime);
            transform.Rotate(Vector3.up, 3f);

            yield return null;

            _offset = PlayerController.Instance.transform.position - transform.position;
        }

        PlayerAttack.Instance.OnSpecialReturn();

        Destroy(gameObject);
    }

}
