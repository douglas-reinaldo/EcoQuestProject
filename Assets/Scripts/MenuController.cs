using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject pause;
    public Sprite imagem_desbloqueada;

    public Button fase_2;
    public Button fase_3;
    private bool fase2_desbloqueada = false;
    private bool fase3_desbloqueada = false;

    public GameObject posicao1;
    public GameObject posicao2;
    public GameObject posicao3;

    public CinemachineCamera virtualCamera;

    public GameObject SoundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MenuPrincipal")
            return;

        if (PlayerPrefs.GetInt("fase2Desbloqueada", 0) == 1)
        {
            fase_2.image.sprite = imagem_desbloqueada;
            fase_2.interactable = true;
            virtualCamera.Follow = posicao2.transform;
        }
        else
        {
            fase_2.interactable = false;
        }

        if (PlayerPrefs.GetInt("fase3Desbloqueada", 0) == 1)
        {
            fase_3.image.sprite = imagem_desbloqueada;
            fase_3.interactable = true;
            virtualCamera.Follow = posicao3.transform;
        }
        else
        {
            fase_3.interactable = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void TrocarCena(string cena)
    {
        SceneManager.LoadScene(cena);
        Time.timeScale = 1f;
    }

    public void SairJogo()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void SairMenu()
    {
        panelMenu.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1f;
    }

    public void AbrirMenu()
    {
        panelMenu.SetActive(true);
        pause.SetActive(false);
        Time.timeScale = 0f;
    }

}
