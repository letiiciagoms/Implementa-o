using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadePanel;  // painel que faz o fade
    public float fadeDuration = 1f;

    void Start()
    {
        // começa transparente
        if (fadePanel != null)
            fadePanel.alpha = 0f;
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutIn(sceneName));
    }

    private IEnumerator FadeOutIn(string sceneName)
    {
        // Fade para preto
        yield return StartCoroutine(Fade(0f, 1f));

        // Carrega a nova cena
        SceneManager.LoadScene(sceneName);

        // Espera 1 frame para o painel existir na nova cena
        yield return null;

        // Fade de volta
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if (fadePanel != null)
                fadePanel.alpha = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            yield return null;
        }

        if (fadePanel != null)
            fadePanel.alpha = endAlpha;
    }
}
