using System.Collections;
using Unity.Properties;
using UnityEngine;

public class AnimationTrashController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Sprite[] sprites_lixo = new Sprite[2];
    public Sprite[] sprites_desaparecer = new Sprite[4];
    SpriteRenderer TRSR;

    void Start()
    {
        TRSR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && TRSR.sprite == sprites_lixo[0]) 
        {
            TRSR.sprite = sprites_lixo[1];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TRSR.sprite = sprites_lixo[0];
    }

    public void destruirLixo() 
    {
        StartCoroutine(AnimacaoDesaparecer());
        Destroy(gameObject, 0.6f);
    }


   
    IEnumerator AnimacaoDesaparecer() 
    {
        TRSR.sortingOrder = 5;
        foreach (Sprite imagem_ in sprites_desaparecer) 
        {
            TRSR.sprite = imagem_;
            yield return new WaitForSeconds(0.10f);
        }
        

    }
}
