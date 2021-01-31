using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintHide : MonoBehaviour
{
    bool attacked = false;
    bool specialed = false;
    bool dash = false;

    private void Update()
    {
        if (attacked && specialed && dash)
        {
            gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Special"))
        {
            specialed = true;
        }

        if (Input.GetButtonDown("Attack"))
        {
            attacked = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            dash = true;
        }
    }
}
