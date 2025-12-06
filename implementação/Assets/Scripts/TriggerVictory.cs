using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerVictory : MonoBehaviour
{
    public GameObject victory;
    [Header("UI")]
    public GameObject victoryPanel;   // painel de vitória
    public Button backToMenuButton;   // botão para voltar ao menu

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false); // começa invisível

        if (backToMenuButton != null)
            backToMenuButton.onClick.AddListener(BackToMenu);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject preFab = Instantiate(victory, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            Destroy(preFab.gameObject, 1f);
            ShowVictoryPanel();
        }
    }

    void ShowVictoryPanel()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        // pausa o jogo se quiser
        Time.timeScale = 0f;
    }

    void BackToMenu()
    {
        // retoma o tempo
        Time.timeScale = 1f;

        // carrega a cena do menu (substitua "Menu" pelo nome da sua cena de menu)
        SceneManager.LoadScene("Menu");
    }
}