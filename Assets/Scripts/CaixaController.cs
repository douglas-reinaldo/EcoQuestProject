using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CaixaController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> listaSemente = new List<GameObject>();
    public Sprite[] sprites = new Sprite[2];
    public GameObject botaoPegar;
    public GameObject sementeAtual;
    public PlayerController player;
    public SpriteRenderer SRCC;


    void Start()
    {
        if (botaoPegar != null)
        {
            botaoPegar.SetActive(false);
        }

        SRCC = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.sementeColetada == false && verificarSemente()) {

            SRCC.sprite = sprites[1];
            botaoPegar.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SRCC.sprite = sprites[0];
            botaoPegar.SetActive(false);
        }
    }

    public GameObject fornecerSemente()
    {
        sementeAtual = listaSemente[listaSemente.Count - 1];
        listaSemente.RemoveAt(listaSemente.Count - 1);
        return sementeAtual;

    }

    public bool verificarSemente() 
    {
        if (listaSemente != null && listaSemente.Count == 0)
        {
            return false;
        }
        return true;
    }

}
