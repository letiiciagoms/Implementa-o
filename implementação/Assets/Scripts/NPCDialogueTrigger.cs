using UnityEngine;
using System.Collections;

public class NPCDialogueTrigger : MonoBehaviour
{
    public DialogueObject dialogue;      // ScriptableObject com falas
    public GameObject promptText;        // UI que mostra "Pressione E"
    public float promptDuration = 2f;    // tempo que a mensagem fica visível

    private bool playerNear = false;

    void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Inicia diálogo ou vai para a próxima fala
            if (DialogueManager.instance.dialoguePanel.activeSelf)
            {
                DialogueManager.instance.DisplayNextSentence();
            }
            else
            {
                DialogueManager.instance.StartDialogue(dialogue);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;

            if (promptText != null)
                StartCoroutine(ShowPrompt());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerNear = false;
    }

    private IEnumerator ShowPrompt()
    {
        promptText.SetActive(true);
        yield return new WaitForSeconds(promptDuration);
        promptText.SetActive(false);
    }
}