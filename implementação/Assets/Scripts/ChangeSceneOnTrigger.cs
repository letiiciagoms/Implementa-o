using UnityEngine;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    [Header("Nome da cena que deseja carregar")]
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca o primeiro SceneFader na cena
            SceneFader fader = SceneFader.FindFirstObjectByType<SceneFader>();
            if (fader != null)
            {
                fader.FadeToScene(sceneName);
            }
            else
            {
                // fallback: se não houver SceneFader, carrega direto
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
    }
}