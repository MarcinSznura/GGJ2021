using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Camera mainCamera = null;

    private void Awake()
    {
        mainCamera = Camera.main;    
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
