using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Som_ir_para_fase;
    public AudioClip Som_abrir_menu;
    public AudioClip Som_opcao_menu;
    public AudioClip Som_coletar_lixo;
    public AudioClip Som_segurar_objeto;
    public AudioClip Som_pegar_chave;
    public AudioClip Som_pulo_personagem;
    public AudioClip Som_personagem_plantando;
    public AudioClip Som_personagem_regando;
    public AudioClip Som_lixeira_abrindo;
    public AudioClip Som_personagem_enterrando;
    public AudioClip Som_personagem_morrendo;
    public AudioClip Som_passar_fase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TocarSomIrParaFase() 
    {
        audioSource.PlayOneShot(Som_ir_para_fase);
    }

    public void TocarSomAbrirMenu() 
    {
        audioSource.PlayOneShot(Som_abrir_menu);
    }

    public void TocarSomOpcaoMenu() 
    {
        audioSource.PlayOneShot(Som_opcao_menu);
    }

    public void TocarSomColetarLixo() 
    {
        audioSource.PlayOneShot(Som_coletar_lixo);
    }

    public void TocarSomSegurarObjeto()
    {
        audioSource.PlayOneShot(Som_segurar_objeto);
    }

    public void TocarSomPegarChave()
    {
        audioSource.PlayOneShot(Som_pegar_chave);
    }

    public void TocarSomPuloPersonagem()
    {
        audioSource.PlayOneShot(Som_pulo_personagem);
    }

    public void TocarSomPlantarPersonagem()
    {
        audioSource.PlayOneShot(Som_personagem_plantando);

    }

    public void TocarSomRegarPersonagem()
    {
        audioSource.PlayOneShot(Som_personagem_regando);
    }

    public void TocarSomLixeira()
    {
        audioSource.PlayOneShot(Som_lixeira_abrindo);
    }

    public void TocarSomEnterrar()
    {
        audioSource.PlayOneShot(Som_personagem_enterrando);
    }

    public void TocarSomPersonagemMorrendo()
    {
        audioSource.PlayOneShot(Som_personagem_morrendo);
    }

    public void TocarSomPassarFase()
    {
        audioSource.PlayOneShot(Som_passar_fase);
    }


}
