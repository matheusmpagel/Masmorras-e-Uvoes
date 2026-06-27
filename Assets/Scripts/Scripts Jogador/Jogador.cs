using UnityEngine;
using UnityEngine.InputSystem;

public class Jogador : MonoBehaviour
{
    [Header("Variáveis")]
    [SerializeField] private int velocidade;
    public bool invulneravel = false;
    private SpriteRenderer render;

    [Header("Componentes")]
    [SerializeField] private Rigidbody2D fisica;
    [SerializeField] private Animator animador;
    private Ataque ataque;
    private SistemaVida vida;
    private Vector2 direcao;
    private Vector2 ultimaDirecao;

    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        vida = GetComponent<SistemaVida>();
        ataque = GetComponent<Ataque>();
        animador = GetComponent<Animator>();

        if (GameManager.instancia != null)
        {
            GameManager.instancia.InicializarNovoJogador(vida, ataque);
        }
    }

    void FixedUpdate()
    {
        fisica.linearVelocity = direcao * velocidade;
        animador.SetFloat("eixoX", direcao.x);
        animador.SetFloat("eixoY", direcao.y);
        animador.SetFloat("lastX", ultimaDirecao.x);
        animador.SetFloat("lastY", ultimaDirecao.y);
        animador.SetBool("EstaCorrendo", direcao != Vector2.zero);

        if(direcao != Vector2.zero)
        {
            ultimaDirecao = direcao;
        }

        if (direcao.x > 0)
        {
            render.flipX = false;
        } else if (direcao.x < 0)
        {
            render.flipX = true;
        }
    }

    public void Movimentar(InputAction.CallbackContext contexto)
    {
        direcao = contexto.ReadValue<Vector2>();
    }

    public void OnTriggerEnter2D(Collider2D objeto)
    {
        if(objeto.gameObject.tag == "portal")
        {
            GameManager.instancia.TrocarCena();
        }
    }

    public void CurarVida(int qnt)
    {
        vida.vidaAtual += qnt;
    }

    public void BoostDano(int qnt)
    {
        ataque.multiplicadorDano += qnt;
    }
}
