using UnityEngine;

public class TomarDano : MonoBehaviour
{
    private SistemaVida vida;
    private Jogador jogador;
    private Inimigo inimigo;

    void Start()
    { 
        inimigo = GetComponent<Inimigo>();
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if(colisao.gameObject.tag == "Player")
        { 
            Jogador jogador = colisao.gameObject.GetComponent<Jogador>();
            SistemaVida vida = colisao.gameObject.GetComponent<SistemaVida>();

            if(jogador.invulneravel) {return;}

            vida.vidaAtual -= inimigo.forca;

            StartCoroutine(vida.Invulnerabilidade());
        }
    }
}
