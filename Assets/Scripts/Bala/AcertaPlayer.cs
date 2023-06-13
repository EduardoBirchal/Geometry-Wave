using UnityEngine;

public class AcertaPlayer : FuncoesBala
{
    public float dano;

    void OnCollisionEnter2D(Collision2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        switch (tag)
        {
            case "Player":
                outro.GetComponent<PlayerGerenciaHP>().TomaDano(dano);
                if(IsHost) DestroiBalaServerRpc();
            break;
            
            default:
            break;
        }
    }
}
