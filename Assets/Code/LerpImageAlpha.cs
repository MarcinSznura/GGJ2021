using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LerpImageAlpha : MonoBehaviour
{
    [SerializeField] int indexOfLevelToLoad = 0;

    Image image = null;
    Color blackScereen;

    void Start()
    {
        image = GetComponentInChildren<Image>();

        StartCoroutine(lerpColor());
    }

    private IEnumerator lerpColor()
    {
        float _t = 0f;

        while (_t < 1)
        {
            blackScereen.a = _t;
            image.color = blackScereen;

            _t += Time.deltaTime / 3f;
            yield return null;
        }

        SceneManager.LoadScene(indexOfLevelToLoad);
    }
}
