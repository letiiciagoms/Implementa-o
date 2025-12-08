using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // ============================
    //      SINGLETON
    // ============================

    // Instância pública e estática — permite que outros scripts acessem
    // DialogueManager.instance de qualquer lugar.
    public static DialogueManager instance;

    // ============================
    //     VARIÁVEIS DE DIÁLOGO
    // ============================

    public GameObject dialoguePanel;       // Painel da UI onde o texto aparece
    public TextMeshProUGUI dialogueText;   // Texto dentro da caixa de diálogo

    private Queue<string> sentences;       // Fila de frases do diálogo
    private bool isTyping;                 // Indica se o texto está sendo "digitado"
    private string currentSentence;        // Armazena a frase atual

    private void Awake()
    {
        // ======================================
        //      IMPLEMENTAÇÃO CORRETA DO SINGLETON
        // ======================================

        // Se já existe um DialogueManager na cena E não é este,
        // destruímos este objeto para manter apenas um.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Caso contrário, este passa a ser a instância oficial.
        instance = this;

        // Se quiser que o DialogueManager continue entre cenas:
        // DontDestroyOnLoad(gameObject);

        // Inicializando a fila de frases
        sentences = new Queue<string>();

        // Garante que o painel comece fechado
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    // ======================================
    //           INÍCIO DO DIÁLOGO
    // ======================================

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
