using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [SerializeField] private float velocidade;
    [SerializeField] private float distanciaMax;
    [SerializeField] private float tempoEspera;
    [SerializeField] public Animator animador;

    [Header("Atributos")]
    [SerializeField] public int forca;
    [SerializeField] public float vidaMaxima;
    private float vidaAtual;

    [Header("Sistema de Drops")]
    [SerializeField] private GameObject itemCuraPrefab;
    [SerializeField] private GameObject itemDanoPrefab;
    [SerializeField] [Range(0f, 100f)] private float chanceDropTotal = 30f;

    private Vector2 destino;
    private float tempoParado = 0f;
    private bool esperando = false;
    private GameObject jogador;
    private SpriteRenderer render;
    private Rigidbody2D fisica;
    private float cronometroUnstuck = 0f;
    private int modos = 0;
    private Vector2 ultimaPosicao;
    private float tempoTravado;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        render = GetComponent<SpriteRenderer>();
        fisica = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        vidaAtual = vidaMaxima;
        if (jogador == null)
        Debug.LogError("Jogador não encontrado!");
    }

    void FixedUpdate()
    {
        if(modos == 0)
        {
            Patrulha();
        }
        else
        {
            Perseguicao();
        }
        animador.SetBool("esperando", esperando);
    }

    void OnTriggerEnter2D(Collider2D objeto)
    {   

        if(objeto.gameObject.tag == "Player")
        {
            modos = 1;
        }
    }

    void OnTriggerExit2D(Collider2D objeto)
    {
        if(objeto.gameObject.tag == "Player")
        {
            modos = 0;
        }
    }

    void Patrulha() 
    {
        if(esperando)
        {
            tempoParado -= Time.fixedDeltaTime;
            if(tempoParado <= 0)
            {
                esperando = false;
                NovaPosicao();
            }
            return;
        }

        cronometroUnstuck -= Time.fixedDeltaTime;
        if(cronometroUnstuck <= 0)
        {
            NovaPosicao();
            return;
        }

        fisica.MovePosition(Vector2.MoveTowards(transform.position, destino, velocidade * Time.fixedDeltaTime)); 

        render.flipX = transform.position.x > destino.x;

        float distancia = Vector2.Distance(transform.position, destino);

        if(distancia < 0.1f)
        {
            esperando = true;
            tempoParado = tempoEspera;
        }
    }

    void Perseguicao() 
    {
        esperando = false;
        fisica.MovePosition(Vector2.MoveTowards(transform.position, jogador.transform.position, velocidade * Time.fixedDeltaTime));
        render.flipX = transform.position.x > jogador.transform.position.x;

        // if (Vector2.Distance(transform.position, ultimaPosicao) < 0.02f)
        // {
        //     tempoTravado += Time.fixedDeltaTime;
        // }
        // else
        // {
        //     tempoTravado = 0;
        // }

        // ultimaPosicao = transform.position;

        // if (tempoTravado > 1.5f)
        // {

        //     modos = 0;
        //     tempoTravado = 0;
        //     podePerseguir = false;
        //     esperando = false;
        //     NovaPosicao();
        // }
    }

    void NovaPosicao()
    {
        Vector2 origem = transform.position;

        float x = Random.Range(-distanciaMax, distanciaMax);
        float y = Random.Range(-distanciaMax, distanciaMax);

        destino = origem + new Vector2(x, y);

        cronometroUnstuck = 3f;
    }

    public void TomarDano(float dano)
    {
        vidaAtual -= dano;
        StartCoroutine(Piscar());

        if(vidaAtual <= 0)
        {
            GerarDrop();
            Destroy(gameObject);

            if(gameObject.tag == "vilao")
            {
                GerenciadorInterface.instancia.VenceuJogo();
            }
        }
    }

    public System.Collections.IEnumerator Piscar()
    {
        for(int i = 0; i < 2; i++)
        {
        render.enabled = false;
        yield return new WaitForSeconds(0.1f);

        render.enabled = true;
        yield return new WaitForSeconds(0.1f);
        }
    }

    private void GerarDrop()
    {
        float sorteio = Random.Range(0f, 100f);
        if (sorteio <= chanceDropTotal)
        {
            int escolhaItem = Random.Range(0, 4);

            if (escolhaItem == 0)
            {   
                Instantiate(itemDanoPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(itemCuraPrefab, transform.position, Quaternion.identity);;
            }
        }
    }
}
