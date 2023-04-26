using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcertaInimigo : FuncoesGerais
{
    public int impacto;
    public float dano;

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        switch (tag)
        {
            case "Inimigo":
                outro.GetComponent<InimigoGerenciaHP>().TomaDano(dano, impacto, transform.eulerAngles.z);
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
