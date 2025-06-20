using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BarraProgressoController : MonoBehaviour
{
    public Image barraPreenchimento;
    public GameObject chave;
    public int totalObjetos = 18; 
    public static int objetosColetados;
    public GameObject audioManager;

    private void Start()
    {
        chave.SetActive(false);
        objetosColetados = 0;
    }
    public void ColetarObjeto()
    {
        objetosColetados++;

        float valorAntes = barraPreenchimento.fillAmount;
        float valorDepois = (float)objetosColetados / totalObjetos;


        StartCoroutine(AnimacaoBarra(valorAntes, valorDepois));

        if (objetosColetados == totalObjetos)
        {
            ChaveAparece();
        }
    }

    IEnumerator AnimacaoBarra(float a, float b) 
    {
        float duracao = 0.4f;
        float tempo = 0.0f;

        while (tempo < duracao) 
        {
            tempo += Time.deltaTime;
            float valorAtual = Mathf.Lerp(a, b, tempo / duracao);
            barraPreenchimento.fillAmount = valorAtual;
            yield return null;
       
        }
        barraPreenchimento.fillAmount = b;
    }

    void ChaveAparece()
    {
        audioManager.GetComponent<AudioManager>().TocarSomPegarChave();
        chave.SetActive(true);
    }
}
