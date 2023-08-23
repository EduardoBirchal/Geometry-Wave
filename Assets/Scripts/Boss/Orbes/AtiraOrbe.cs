using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiraOrbe : AtiraInimigo
{
    public bool rajadaAtivada;
    public bool raioAtivado;
    public int rajadasAteAgora = 0;

    [SerializeField] private float tempoRecarregaRajada;
    [SerializeField] private int numTirosRajada;
    [SerializeField] private int rajadaCarregada;

    private const int idRajada = 1;
    private const int idRaio = 2;

    void Start() {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().inimigo;
        atirador = transform.parent.gameObject;
        balaCarregada = false;
        rajadaCarregada = numTirosRajada;

        GetValores();

        StartCoroutine(Recarrega(tempoRecarregaRajada));

        StartCoroutine(AtiraRajada());
    }

    void Update() {

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
                
                rajadasAteAgora++;
                yield return new WaitForSeconds(tempoRecarregaRajada);
            }
            else if (raioAtivado) {
                rajadasAteAgora = 0;
                tipoBala = idRaio;
                AtiraServerRpc(tipoBala);
            }

            yield return null;
        }
    }
}
