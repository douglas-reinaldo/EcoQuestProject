using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuracoController : MonoBehaviour
{
    public GameObject botaoSoltar;
    public GameObject botaoPlantar;
    public PlayerController player;
    public bool plantada = false;
    public static int qtd_plantada = 0;
    public Sprite terraCoberta;
    public SpriteRenderer SRBC;
    public static int Preenchidos;
    public static int Regados;
    public GameObject botaoRegar;
    public bool regado = false;
    public Sprite arvoreGrande;
    Animator animator;
    public Button[] botoes = new Button[3];


    void Start()
    {
        if (botaoSoltar != null)
        {
            botaoSoltar.SetActive(false);
        }

        SRBC = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Preenchidos = 0;
        Regados = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.sementeColetada == true && plantada == false)
        {
            botaoSoltar.SetActive(true);
        }
        else if (collision.CompareTag("Player") && player.paSegurada && SRBC.sprite != terraCoberta)
        {
            botaoPlantar.SetActive(true);
        }
        else if (collision.CompareTag("Player") && player.regadorSegurado && regado == false)
        {
            botaoRegar.SetActive(true);
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            botaoSoltar.SetActive(false);
            botaoPlantar.SetActive(false);
            botaoRegar.SetActive(false);
        }
    }

    public void adicionarPreenchido()
    {
        Preenchidos++;
    }

    public int getPreenchidos()
    {
        return Preenchidos;
    }

    public void adicionarRegados()
    {
        Regados++;
    }

    public int getRegados()
    {
        return Regados;
    }

    public void florecer()
    {
        StartCoroutine(esperar());

        
    }

    IEnumerator esperar() 
    {
        yield return new WaitForSeconds(3.5f);
        animator.enabled = true;
        botaoPlantar.SetActive(false);
        regado = true;
        botaoRegar.SetActive(false);
        animator.SetBool("Crescer", true);
        yield return new WaitForSeconds(1.4f);


        animator.enabled = false;
        transform.position = new Vector3(transform.position.x,
        transform.position.y - 0.3f,
        transform.position.z);
        SRBC.sprite = arvoreGrande;

    }

    public void desativarBotoes() 
    {
        foreach (Button button in botoes) 
        {
            button.interactable = false;

        }
    }

    public void ativarBotoes() 
    {
        foreach (Button button in botoes)
        {
            button.interactable = true;
        }
    }

    public void preencherBuraco() 
    {
        animator.enabled = false;
        Debug.Log("Sprite atual antes: " + SRBC.sprite.name);
        SRBC.sprite = terraCoberta;
        Debug.Log("Sprite novo depois: " + SRBC.sprite.name);
        Color corAtual = SRBC.color;
        corAtual.a = 1f;
        SRBC.color = corAtual;
        botaoPlantar.SetActive(false);
        adicionarPreenchido();
    }


}
