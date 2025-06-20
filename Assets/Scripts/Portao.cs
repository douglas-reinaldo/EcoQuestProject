using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portao : MonoBehaviour
{
    public GameObject chaveNaMao;
    public GameObject botaoAbrir;
    public GameObject audioManager;

    private bool jogadorPerto = false;

    void Update()
    {
        if (jogadorPerto)
        {
            botaoAbrir.SetActive(true);
        }
        else
        {
            botaoAbrir.SetActive(false);
        }
    }
    public void DefinirProximidade(bool valor)
    {
        jogadorPerto = valor;
    }

    public void AbrirPortao() 
    {
        audioManager.GetComponent<AudioManager>().TocarSomPassarFase();
        StartCoroutine(esperar(1f));
        if (chaveNaMao.activeSelf)
        {
            if (PlayerPrefs.GetInt("fase2Desbloqueada", 0) == 0) 
            {
                PlayerPrefs.SetInt("fase2Desbloqueada", 1);
                SceneManager.LoadScene("MenuPrincipal");
            }

            else if (PlayerPrefs.GetInt("fase2Desbloqueada", 0) == 1) 
            {
                PlayerPrefs.SetInt("fase3Desbloqueada", 1);
                SceneManager.LoadScene("MenuPrincipal");
            }
            
        }
    }

    IEnumerator esperar(float tempo) 
    {
        yield return new WaitForSeconds(tempo);
    }


}

