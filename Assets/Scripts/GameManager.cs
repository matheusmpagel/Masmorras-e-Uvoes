using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia {get; private set;}
    [SerializeField] private List<string> fases;
    private int cenaAtual = 0;

    private int vidaSalva = -1;
    private float danoSalvo = 1f;

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

    public void ReiniciarJogo()
    {
        cenaAtual = 0;
        vidaSalva = -1;
        danoSalvo = 1f;
    }

    public void TrocarCena()
    {   
        SalvarDadosJogador();

        GameObject jogador = GameObject.FindWithTag("Player");
        vidaSalva += 2;

        if(cenaAtual + 1 < fases.Count)
        {
            cenaAtual++;
            string proximaCena = fases[cenaAtual];
            SomController.instancia.TrocarMusica(proximaCena);
            SceneManager.LoadScene(fases[cenaAtual]);
            Physics2D.IgnoreLayerCollision(6, 9, false);
        } 
        
    }

    private void SalvarDadosJogador()
    {

        GameObject jogador = GameObject.FindWithTag("Player");

        if (jogador != null)
        {
            SistemaVida vida = jogador.GetComponent<SistemaVida>();
            Ataque ataque = jogador.GetComponent<Ataque>();

            if (vida != null){ vidaSalva = vida.vidaMaxima;}
            if (ataque != null){ danoSalvo = ataque.multiplicadorDano;}

        }
    }

    public void InicializarNovoJogador(SistemaVida vida, Ataque ataque)
    {
        if(vidaSalva > -1 && vida != null){ vida.vidaMaxima = vidaSalva;
                                            vida.vidaAtual = vidaSalva;}
        if(ataque != null){ ataque.multiplicadorDano = danoSalvo;}        
    }
}