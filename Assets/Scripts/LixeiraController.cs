using System.Collections;
using UnityEngine;

public class LixeiraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public string tipoLixo;
    public BarraProgressoController brc;
    public Animator animator;
    public GameObject audioManager;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tipoLixo))
        {
            audioManager.GetComponent<AudioManager>().TocarSomLixeira();
            GameObject lixo = collision.gameObject;
            lixo.transform.SetParent(gameObject.transform);
            lixo.transform.localPosition = Vector3.zero;
            lixo.GetComponent<AnimationTrashController>().destruirLixo();
            brc.ColetarObjeto();
            StartCoroutine(AguardarAnimacao());
           
        }
    }

    IEnumerator AguardarAnimacao() 
    {
        animator.SetBool("Coletado", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Coletado", false);
    }
}
