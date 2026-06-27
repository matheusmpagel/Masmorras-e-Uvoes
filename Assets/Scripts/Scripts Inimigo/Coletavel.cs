using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public enum TipoItem { Cura, Dano }
    public TipoItem tipoDoItem;
    
    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "Player")
        {   
            Jogador jogador = colisao.GetComponent<Jogador>();

            if (jogador != null)
            {
                if (tipoDoItem == TipoItem.Cura)
                {
                    jogador.CurarVida(1);
                }
                else if (tipoDoItem == TipoItem.Dano)
                {
                    jogador.BoostDano(1);
                }
            }

            Destroy(gameObject);
        }
    }
}
