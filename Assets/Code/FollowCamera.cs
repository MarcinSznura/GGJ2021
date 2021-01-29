using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] PlayerController player = null;

    private void Start()
    {
        player = PlayerController.Instance;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
