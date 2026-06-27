using UnityEngine;

public class Bala : MonoBehaviour
{
    public Rigidbody2D fisica;
    public Vector2 direcao = Vector2.right;
    public float vidaUtil = 2;
    public float velocidade;
    public float dano = 1f;
    public Animator animador;

    void Start()
    {
        animador = GetComponent<Animator>();
        fisica.linearVelocity = direcao * velocidade;
        Destroy(gameObject, vidaUtil);
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {   
        if (colisao.isTrigger) return;

        Inimigo inimigo = colisao.GetComponent<Inimigo>();

        if (inimigo != null){ inimigo.TomarDano(dano);}

        Destroy(gameObject);
    }
}
