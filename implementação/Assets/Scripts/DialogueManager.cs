using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    

    public GameObject dialoguePanel;       
    public TextMeshProUGUI dialogueText;   

    private Queue<string> sentences;       
    private bool isTyping;                 
    private string currentSentence;        

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

       
        instance = this;
        
        
        sentences = new Queue<string>();

        
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    

    public void StartDialogue(DialogueObject dialogue)
    {
        dialoguePanel.SetActive(true);   // Abre a caixa de diálogo

        sentences.Clear();               // Limpa frases anteriores

        // Coloca todas as falas definidas no ScriptableObject dentro da fila
        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        // Mostra a primeira frase
        DisplayNextSentence();
    }

    // ======================================
    //        MOSTRAR PRÓXIMA FRASE
    // ======================================

    public void DisplayNextSentence()
    {
        // Se o texto ainda está sendo "digitado",
        // pular para o final instantaneamente.
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        // Se não há mais frases, encerrar diálogo
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Pega a próxima frase da fila
        currentSentence = sentences.Dequeue();

        // Inicia o efeito de digitação
        StartCoroutine(TypeSentence(currentSentence));
    }

    // ======================================
    //       EFEITO DE "TEXTO DIGITANDO"
    // ======================================

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);  // Velocidade da "digitação"
        }

        isTyping = false;
    }

    // ======================================
    //             ENCERRA DIÁLOGO
    // ======================================

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
