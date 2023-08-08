using UnityEngine;
using System.Collections.Generic;

public class AcertaInimigo : FuncoesBala
{
    public int impacto;
    public float dano;
    public int perfuracaoBala;
    private List<GameObject> inimigosAtingidos;

    void Start() {
        inimigosAtingidos = new List<GameObject>();
        perfuracaoBala = 2;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        switch (tag)
        {
            case "Inimigo":
                if(inimigosAtingidos.Find(inimigo => inimigo == outro) == null) {
                    inimigosAtingidos.Add(outro);
                    outro.GetComponent<InimigoGerenciaHP>().TomaDano(dano, impacto, transform.eulerAngles.z);
                    DiminuiPerfuracao();
                }
            break;
            
            default:
            break;
        }
    }

    private void DiminuiPerfuracao() {
        print(perfuracaoBala);
        perfuracaoBala--;

        if (perfuracaoBala < 1) {
            Destruir();
        }
    }

    private void Destruir() {
        if(IsHost) DestroiBalaServerRpc();
    }
}
