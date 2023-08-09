using UnityEngine;
using System.Collections.Generic;

public class AcertaInimigo : AcertaAlvo
{
    private List<GameObject> inimigosAtingidos;

    void Start() {
        inimigosAtingidos = new List<GameObject>();
        tagAlvo = "Inimigo";
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        if (tag == tagAlvo) {
            HandleAlvo(outro);
        }
    }

    private void HandleAlvo(GameObject alvo) {
        if(inimigosAtingidos.Find(inimigo => inimigo == alvo) == null) {
            inimigosAtingidos.Add(alvo);
            alvo.GetComponent<InimigoGerenciaHP>().TomaDano(dano);
            DiminuiPerfuracao();
        }
    }

    private void DiminuiPerfuracao() {
        perfuracaoBala--;

        if (perfuracaoBala < 1) {
            DestroiBala();
        }
    }
}
