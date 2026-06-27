using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class GerenciadorInterface : MonoBehaviour
{  
    public static GerenciadorInterface instancia {private set; get; }

    [Header("Telas")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuOpcoes;
    [SerializeField] private GameObject hud;


    [Header("Áudio")]
    [SerializeField] private TMP_Text txtBtnSom;
    [SerializeField] private AudioSource somController;
    [SerializeField] private Slider volumeGeral;

    [Header("Painéis da UI")]
    [SerializeField] private GameObject painelGameOver;
    public GameObject painelPausa;
    public GameObject menuVenceu;

    private bool jogoPausado = false;
    private bool mute = false;
    private bool emGameplay = false;
    
    void Awake()
    {
        if(instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        DespausarJogo();
        menuPrincipal.SetActive(true);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        painelPausa.SetActive(false);
        menuVenceu.SetActive(false);
        hud.SetActive(false);
        emGameplay = false;
    }

    public void Jogar()
    {
        if (GameManager.instancia != null){GameManager.instancia.ReiniciarJogo();}

        SceneManager.LoadScene("Fase1");
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        painelPausa.SetActive(false);
        menuVenceu.SetActive(false);
        Physics2D.IgnoreLayerCollision(6, 9, false);
        hud.SetActive(true);
        emGameplay = true;
    }

    public void MenuOpcoes()
    {
        if(!menuOpcoes.activeSelf)
        {
            menuPrincipal.SetActive(false);
            menuOpcoes.SetActive(true);
            painelGameOver.SetActive(false);
            painelPausa.SetActive(false);
            hud.SetActive(false);
        }
        else
        {
            menuOpcoes.SetActive(false);
            menuPrincipal.SetActive(true);
            painelGameOver.SetActive(false);
            painelPausa.SetActive(false);
            hud.SetActive(false);
        }
    }

    public void VoltarMenu()
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        menuPrincipal.SetActive(true);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        painelPausa.SetActive(false);
        menuVenceu.SetActive(false);
        hud.SetActive(false);
        jogoPausado = false;
        emGameplay = false;
    }

    public void Pausar(InputAction.CallbackContext contexto)
    {   
        if (!emGameplay) return;

        if(contexto.started)
        {
            if (jogoPausado) { DespausarJogo();}
            else { PausarJogo();}
        }
    }

    public void PausarJogo()
    {
        jogoPausado = true;
        painelPausa.SetActive(true);
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        hud.SetActive(false);
        Time.timeScale = 0f;
    }

    public void DespausarJogo()
    {
        jogoPausado = false;
        painelPausa.SetActive(false);
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        painelPausa.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1f;

    }

    public void GameOver()
    {
        painelPausa.SetActive(false);
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(true);
        painelPausa.SetActive(false);
        hud.SetActive(false);
        emGameplay = false;
        Time.timeScale = 0f;
    }

    public void ReiniciarFase()
    {
        Time.timeScale = 1f;
        string cenaAtual = SceneManager.GetActiveScene().name;
        painelPausa.SetActive(false);
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        painelPausa.SetActive(false);
        menuVenceu.SetActive(false);
        hud.SetActive(true);
        jogoPausado = false;
        emGameplay = true;
        Physics2D.IgnoreLayerCollision(6, 9, false);
        SceneManager.LoadScene(cenaAtual);
    }

    public void VenceuJogo()
    {
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(false);
        painelGameOver.SetActive(false);
        painelPausa.SetActive(false);
        menuVenceu.SetActive(true);
        hud.SetActive(false);
        emGameplay = false;
        Time.timeScale = 0f;
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Mutar()
    {
        mute = !mute;
        somController.mute = mute;
        if(mute) { txtBtnSom.text = "DESATIVADO";} 
        else{ txtBtnSom.text = "ATIVADO";}
    }

    public void AjustarSom()
    {
        somController.volume = volumeGeral.value;
    }
}
