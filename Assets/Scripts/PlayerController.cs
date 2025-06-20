using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    public Transform inicialPosition;
    public float playerSpeed = 5.5f;
    public float jumpForce = 10f;
    private float moveDirection;


    public float circleRadius;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsWater;
    private bool isGrounded;
    private bool isWater;

    public List<GameObject> lista = new List<GameObject>();


    public bool coletar = false;
    GameObject lixo;
    GameObject lixoColetado = null;
    private TrashController trash;
    public Transform handPlace;
    public BarraProgressoController brc;



    public GameObject botaoPegar;
    public GameObject botaoRegar;

    public bool sementeColetada = false;
    public CaixaController caixa;
    private GameObject semente;
    public GameObject buracoAtual;
    public GameObject objetoRegador;
    public GameObject objetoPa;


    public bool paSegurada = false;
    public bool regadorSegurado = false;

    private bool podeMover = true;

    public Sprite[] sprites_pa = new Sprite[2];
    public Sprite[] sprites_regador = new Sprite[2];

    public GameObject SoundManager;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        Time.timeScale = 1f;
        paSegurada = false;
        regadorSegurado = false;
    }

    // Update is called once per frame

    public float friction = 10f;
    private bool isMoving = false;

    void Update()
    {

        if (isMoving)
        {
            // Move normalmente enquanto o botão estiver pressionado
            playerRigidbody.linearVelocity = new Vector2(moveDirection * playerSpeed, playerRigidbody.linearVelocity.y);
        }
        else
        {
            // Desliza gradualmente até parar
            float newX = Mathf.MoveTowards(playerRigidbody.linearVelocity.x, 0f, friction * Time.deltaTime);
            playerRigidbody.linearVelocity = new Vector2(newX, playerRigidbody.linearVelocity.y);
        }


        diePlayer();

    }

    public void PressionarDireita()
    {
        if (!podeMover) return;

        moveDirection = 1f;
        isMoving = true;
        FlipCharacter();
        playerAnimator.SetBool("Run", true);


    }

    public void PressionarEsquerda()
    {
        if (!podeMover) return;

        moveDirection = -1f;
        isMoving = true;
        FlipCharacter();
        playerAnimator.SetBool("Run", true);


    }

    public void Soltar()
    {

        if (!podeMover) return;

        isMoving = false;
        playerAnimator.SetBool("Run", false);
    }

    private void FlipCharacter()
    {
        if (moveDirection > 0 && transform.localScale.x < 0)
        {
            // Indo para a direita e virando a direção do player
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection < 0 && transform.localScale.x > 0)
        {
            // Indo para a esquerda e virando a direção do player
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Jump()
    {
        if (!podeMover) return;


        CheckIfGrounded();

        if (isGrounded)
        {
            SoundManager.GetComponent<AudioManager>().TocarSomPuloPersonagem();
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }

    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsGround);
    }

    void CheckIfWater()
    {
        isWater = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsWater);
    }

    void diePlayer()
    {
        CheckIfWater();
        if (isWater)
        {
            SoundManager.GetComponent<AudioManager>().TocarSomPersonagemMorrendo();
            if (lista.Count == 1)
            {

                GameObject obj = lista[0];
                lista.RemoveAt(0);
                Destroy(obj);
                SceneManager.LoadScene("MenuPrincipal");
            }
            else
            {
                GameObject obj = lista[lista.Count - 1];
                lista.RemoveAt(lista.Count - 1);
                Destroy(obj);
                reiniciarPosicao();
            }
        }

    }

    void reiniciarPosicao()
    {
        playerRigidbody.linearVelocity = Vector2.zero;
        transform.position = inicialPosition.position;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Organico") || other.CompareTag("Papel") || other.CompareTag("Metal") || other.CompareTag("Plastico") || other.CompareTag("Vidro"))
        {
            coletar = true;
            lixo = other.gameObject;
        }
        else if (other.CompareTag("Buraco"))
        {
            buracoAtual = other.gameObject;
        }

        else if (other.CompareTag("Pa"))
        {
            if (caixa.verificarSemente() == false && sementeColetada == false)
            {
                botaoPegar.SetActive(true);
                objetoPa = other.gameObject;
                objetoPa.GetComponent<SpriteRenderer>().sprite = sprites_pa[1];

            }
        }
        else if (other.CompareTag("Regador"))
        {
            if (objetoRegador == null && BuracoController.Preenchidos == 5)
            {
                botaoPegar.SetActive(true);
                objetoRegador = other.gameObject;
                objetoRegador.GetComponent<SpriteRenderer>().sprite = sprites_regador[1];
            }
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Organico") || other.CompareTag("Papel") || other.CompareTag("Metal") || other.CompareTag("Plastico") || other.CompareTag("Vidro"))
        {
            coletar = false;
            lixo = null;
        }
        else if (other.CompareTag("Buraco"))
        {
            buracoAtual = null;
        }
        else if (other.CompareTag("Pa"))
        {
            if (objetoPa != null && objetoPa.transform.parent != handPlace)
            {
                objetoPa.GetComponent<SpriteRenderer>().sprite = sprites_pa[0];
                objetoPa = null;
                botaoPegar.SetActive(false);
            }
        }
        else if (other.CompareTag("Regador"))
        {
            if (objetoRegador != null && objetoRegador.transform.parent != handPlace)
            {
                objetoRegador.GetComponent<SpriteRenderer>().sprite = sprites_regador[0];
                objetoRegador = null;
                botaoPegar.SetActive(false);

            }
        }
    }

    public void coletarLixo()
    {
        if (coletar && lixo != null && podeMover)
        {
            SoundManager.GetComponent<AudioManager>().TocarSomColetarLixo();
            podeMover = false;
            StartCoroutine(esperarAnimacao());
            brc.ColetarObjeto();
            lixo.GetComponent<AnimationTrashController>().destruirLixo();
            coletar = false;


        }

    }

    IEnumerator esperarAnimacao()
    {
        playerAnimator.SetBool("Coletar", true);
        yield return new WaitForSeconds(0.8f);
        playerAnimator.SetBool("Coletar", false);
        podeMover = true;
    }

    public void segurarLixo()
    {

        if (coletar && lixo != null && lixo.GetComponent<TrashController>().PodeSerColetado() && lixoColetado == null)
        {
            SoundManager.GetComponent<AudioManager>().TocarSomSegurarObjeto();
            trash = lixo.GetComponent<TrashController>();

            // Pegar lixo
            lixoColetado = lixo;
            pegarObjetos(lixoColetado);
            lixoColetado.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        else if (lixoColetado != null)
        {
            SoundManager.GetComponent<AudioManager>().TocarSomSegurarObjeto();
            // Soltar lixo
            lixoColetado.transform.SetParent(null);
            trash.soltarLixo();
            lixoColetado = null;
            trash = null;
        }
    }




    // Fase 3 Script
    public void coletarObjeto()
    {
        if (caixa.verificarSemente())
        {
            if (sementeColetada == false && semente == null)
            {

                semente = caixa.fornecerSemente();
                pegarObjetos(semente);
                sementeColetada = true;
                botaoPegar.SetActive(false);
            }
        }
        else if (objetoPa != null && caixa.verificarSemente() == false)
        {

            objetoPa.GetComponent<SpriteRenderer>().sprite = sprites_pa[0];
            pegarObjetos(objetoPa);
            paSegurada = true;
            botaoPegar.SetActive(false);
        }

        else if (objetoRegador != null && BuracoController.Preenchidos == 5 && paSegurada == false)
        {
            objetoRegador.GetComponent<SpriteRenderer>().sprite = sprites_regador[0];
            pegarObjetos(objetoRegador);
            regadorSegurado = true;
            botaoPegar.SetActive(false);
        }



    }

    public void soltarObjeto()
    {
        if (caixa.verificarSemente() || sementeColetada)
        {
            if (sementeColetada)
            {

                podeMover = false;
                StartCoroutine(AnimacaoPlantacao());
                SoundManager.GetComponent<AudioManager>().TocarSomPlantarPersonagem();

                buracoAtual.GetComponent<BuracoController>().plantada = true;
                buracoAtual.GetComponent<BuracoController>().botaoSoltar.SetActive(false);

            }
        }

    }

    IEnumerator AnimacaoPlantacao()
    {
        buracoAtual.GetComponent<BuracoController>().desativarBotoes();
        playerAnimator.SetBool("Plantar", true);
        yield return new WaitForSeconds(1f);
        semente.transform.SetParent(buracoAtual.transform);
        semente.transform.localPosition = Vector3.zero;
        sementeColetada = false;
        semente = null;
        playerAnimator.SetBool("Plantar", false);
        buracoAtual.GetComponent<BuracoController>().ativarBotoes();
        podeMover = true;
    }



    public void pegarObjetos(GameObject obj)
    {
        SoundManager.GetComponent<AudioManager>().TocarSomSegurarObjeto();
        obj.transform.SetParent(handPlace);
        obj.transform.localPosition = Vector3.zero;
    }



    public void plantar()
    {
        buracoAtual.GetComponent<BuracoController>().preencherBuraco();
        SoundManager.GetComponent<AudioManager>().TocarSomEnterrar();

        if (buracoAtual.GetComponent<BuracoController>().getPreenchidos() == 5 && objetoPa != null && paSegurada)
        {
            SoundManager.GetComponent<AudioManager>().TocarSomEnterrar();
            objetoPa.transform.SetParent(null);
            objetoPa.transform.localPosition = new Vector3(-19.5990009f, -4.25299978f, 0);
            objetoPa = null;
            paSegurada = false;

        }

    }

    public void regar()
    {
        SoundManager.GetComponent<AudioManager>().TocarSomRegarPersonagem();
        botaoRegar.SetActive(false);
        podeMover = false;
        StartCoroutine(esperarRegar());
        buracoAtual.GetComponent<BuracoController>().florecer();
        buracoAtual.GetComponent<BuracoController>().adicionarRegados();
    }

    IEnumerator esperarRegar()
    {
        objetoRegador.GetComponent<SpriteRenderer>().enabled = false;
        playerAnimator.SetBool("Regar", true);
        yield return new WaitForSeconds(1.5f);
        playerAnimator.SetBool("Regar", false);
        podeMover = true;
        objetoRegador.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(5f);

    }

    IEnumerator esperar()
    {
        yield return new WaitForSeconds(5f);
    }



}
