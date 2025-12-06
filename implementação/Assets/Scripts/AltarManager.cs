using UnityEngine;

public class AltarManager : MonoBehaviour
{
    public static AltarManager instance;

    [Header("Altares das Rosas (exatamente 4)")]
    public GameObject[] altaresDeRosa = new GameObject[4];

    [Header("Ouro que aparece após completar os 4 altares")]
    public GameObject ouro;

    [Header("Altares das Maçãs (exatamente 4)")]
    public GameObject[] altaresDeMaca = new GameObject[4];

    [Header("Ametista que aparece após completar os 4 altares de maçã")]
    public GameObject ametista;

    private int rosasColocadas = 0;
    private int macasColocadas = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Desativa todos os altares no início
        foreach (GameObject altar in altaresDeRosa)
            altar.SetActive(false);

        foreach (GameObject altar in altaresDeMaca)
            altar.SetActive(false);

        if (ouro != null)
            ouro.SetActive(false);

        if (ametista != null)
            ametista.SetActive(false);
    }

    // Chamado quando o jogador usa uma rosa
    public void ColocarRosaEmAltar()
    {
        if (rosasColocadas >= altaresDeRosa.Length) return;

        altaresDeRosa[rosasColocadas].SetActive(true);
        rosasColocadas++;

        if (rosasColocadas == altaresDeRosa.Length)
            AtivarOuro();
    }

    private void AtivarOuro()
    {
        if (ouro != null)
            ouro.SetActive(true);
        else
            Debug.LogWarning("Ouro não foi atribuído no Inspector!");
    }

    // Chamado quando o jogador usa uma maçã
    public void ColocarMacaEmAltar()
    {
        if (macasColocadas >= altaresDeMaca.Length) return;

        altaresDeMaca[macasColocadas].SetActive(true);
        macasColocadas++;

        if (macasColocadas == altaresDeMaca.Length)
            AtivarAmetista();
    }

    public void AtivarAmetista()
    {
        if (ametista != null)
            ametista.SetActive(true);
        else
            Debug.LogWarning("Ametista não foi atribuída no Inspector!");
    }
}
