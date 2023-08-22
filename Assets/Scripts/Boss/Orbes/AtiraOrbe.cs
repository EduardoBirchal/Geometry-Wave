using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiraOrbe : AtiraInimigo
{
    public bool rajadaAtivada;
    [SerializeField] private float tempoRecarregaRajada;
    [SerializeField] private int numTirosRajada;
    private int rajadaCarregada = 0;

    private const int idRajada = 1;
    private const int idRaio = 2;

    void Start() {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().inimigo;
        atirador = transform.parent.gameObject;
        balaCarregada = false;

        GetValores();

        StartCoroutine(Recarrega(tempoRecarregaRajada));

        StartCoroutine(AtiraRajada());
    }

    private IEnumerator AtiraRajada() {
        while (true) {
            if  (rajadaAtivada) {
                tipoBala = idRajada;

                rajadaCarregada = numTirosRajada;

                while (rajadaCarregada > 0) {
                    if (balaCarregada) {
                        AtiraServerRpc(tipoBala);
                        rajadaCarregada--;
                    }

                    yield return null;
                } 

                yield return new WaitForSeconds(tempoRecarregaRajada);
            }
            else {
                ViraPraObjeto(atirador.transform.position);

                tipoBala = idRaio;
                AtiraServerRpc(tipoBala);
            }

            yield return null;
        }
    }

    private Vector3 OlhaPraForaDoCentro() {
        print (transform.position - atirador.transform.position);
        return transform.position - atirador.transform.position;
    }
}
