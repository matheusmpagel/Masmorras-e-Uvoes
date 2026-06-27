using UnityEngine;
using UnityEngine.InputSystem;

public class Ataque : MonoBehaviour
{
    public Transform PontoLancamento;
    public GameObject balaPrefab;

    [SerializeField] private float tempoEsperaTiro = 0.7f;
    private float proximoTempoTiro = 0f;

    private Vector2 mousePosicaoTela;
    private Vector2 direcaoMirar = Vector2.right;

    public float multiplicadorDano = 1f;

    void Update()
    {   
        Vector3 posicaoMouseMundo = Camera.main.ScreenToWorldPoint(mousePosicaoTela);
        posicaoMouseMundo.z = 0f;

        Vector2 direcaoParaOMouse = ((Vector2)posicaoMouseMundo - (Vector2)PontoLancamento.position);

        direcaoMirar = direcaoParaOMouse.normalized;
    }

    public void Mirar(InputAction.CallbackContext contexto)
    {
        mousePosicaoTela = contexto.ReadValue<Vector2>();
    }

    public void Atirar(InputAction.CallbackContext contexto)
    {   
        if(Time.timeScale == 0f){ return;}

        if (contexto.performed && Time.time >= proximoTempoTiro)
        {
            proximoTempoTiro = Time.time + tempoEsperaTiro;
            
            Bala bala = Instantiate(balaPrefab, PontoLancamento.position, Quaternion.identity).GetComponent<Bala>();
            bala.direcao = direcaoMirar;

            bala.dano *= multiplicadorDano;
        }
    }
}
