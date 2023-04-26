using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcertaPlayer : MonoBehaviour
{
    public float dano;

    void OnCollisionEnter2D(Collision2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        switch (tag)
        {
            case "Player":
                outro.GetComponent<PlayerGerenciaHP>().TomaDano(dano);
                DestroiBala();
            break;
            
            default:
            break;
        }
    }

    void DestroiBala() {
        Destroy(gameObject);
    }
}
