using UnityEngine;

public class TrashController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Rigidbody2D rd2D;
    public Collider2D cll2D;


    public bool podeSerColetado;
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
        cll2D = GetComponent<Collider2D>();
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rd2D.bodyType = RigidbodyType2D.Static;
            cll2D.isTrigger = true;
            podeSerColetado = true;
        }
    }

    public void soltarLixo()
    {

        rd2D.bodyType = RigidbodyType2D.Dynamic;
        cll2D.isTrigger = false;
        podeSerColetado = false;
    }

    public bool PodeSerColetado()
    {
        return podeSerColetado;
    }

}
