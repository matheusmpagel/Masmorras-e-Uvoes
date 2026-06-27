using UnityEngine;
using UnityEngine.UI;

public class SistemaVida : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] public int vidaAtual;
    [SerializeField] public int vidaMaxima;

    [Header("Sprites")]
    [SerializeField] public Image[] coracao;
    [SerializeField] public Sprite cheio;
    [SerializeField] public Sprite vazio;

    [Header("IFrames")]
    [SerializeField] private float tempoIFrames;
    [SerializeField] private int numPiscar;
    private SpriteRenderer sprite;

    void Start()
    {   
        sprite = GetComponent<SpriteRenderer>();


        if (coracao == null || coracao.Length == 0)
        {
            GameObject ui = GameObject.Find("HeartController");
            
            if (ui != null)
            {
                coracao = ui.GetComponentsInChildren<Image>();
            }
            
        }
    }

    void Update()
    {
        LogicaVida();
        Morto();
    }

    void LogicaVida()
    {   
        if(vidaAtual > vidaMaxima){ vidaAtual = vidaMaxima;}

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vidaAtual){coracao[i].sprite = cheio;} else{ coracao[i].sprite = vazio;}

            if (i < vidaMaxima){coracao[i].enabled = true;} else{ coracao[i].enabled = false;}
        }
    }

    void Morto()
    {
        if(vidaAtual <= 0)
        {
            GerenciadorInterface ui = FindFirstObjectByType<GerenciadorInterface>();
            if (ui != null)
            {
                ui.GameOver();
            }
            
            GetComponent<Jogador>().enabled = false;
            Destroy(gameObject);
        }
    }

    public System.Collections.IEnumerator Invulnerabilidade()
    {
        Physics2D.IgnoreLayerCollision(6, 9, true);
        for (int i = 0; i < numPiscar; i++)
        {
            sprite.color = Color.clear;
            yield return new WaitForSeconds(0.1f * tempoIFrames);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f * tempoIFrames);
        }
        Physics2D.IgnoreLayerCollision(6, 9, false);
    }
}
