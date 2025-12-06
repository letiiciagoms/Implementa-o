using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDaCena;
    public void Jogar()
    {
        SceneManager.LoadScene("CENA 1.1");
    }

    public void Sair()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
}
