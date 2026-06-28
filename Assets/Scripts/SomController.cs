using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SomController : MonoBehaviour
{
    public static SomController instancia {private set; get; }
    private bool mute = false;

    [Header("Controladores")]
    [SerializeField] private TMP_Text txtBtnSom;
    [SerializeField] private Slider volumeGeral;

    [Header("Sons")]
    public AudioSource musicaMenu;
    public AudioSource musicaJogo;
    public AudioSource musicaGameOver;
    public AudioSource musicaVitoria;
    
    [SerializeField] private AudioClip fase1;
    [SerializeField] private AudioClip fase2;
    [SerializeField] private AudioClip fase3;

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

    void Update()
    {
        AjustarSom();
    }

    public void Mutar()
    {
        mute = !mute;
        musicaMenu.mute = mute;
        musicaJogo.mute = mute;
        musicaGameOver.mute = mute;
        musicaVitoria.mute = mute;
        if(mute) { txtBtnSom.text = "DESATIVADO";} 
        else{ txtBtnSom.text = "ATIVADO";}
    }

    public void AjustarSom()
    {
        musicaMenu.volume = volumeGeral.value;
        musicaJogo.volume = volumeGeral.value;
        musicaGameOver.volume = volumeGeral.value;
        musicaVitoria.volume = volumeGeral.value;
    }

    public void TrocarMusica(string nomeCena)
    {
        musicaJogo.Stop();

        switch(nomeCena)
        {
            case "Fase1":
                musicaJogo.clip = fase1;
                break;

            case "Fase2":
                musicaJogo.clip = fase2;
                break;

            case "Fase3":
                musicaJogo.clip = fase3;
                break;
        }

        musicaJogo.Play();
    }
}

