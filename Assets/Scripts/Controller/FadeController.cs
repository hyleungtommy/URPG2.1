using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {

    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        Debug.Log("FadeController: FadeToScene " + sceneName);
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        for (float t = fadeDuration; t > 0; t -= Time.deltaTime)
        {
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0;
        fadeImage.color = color;
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        Color color = fadeImage.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1;
        fadeImage.color = color;

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return StartCoroutine(FadeIn());
    }
}
