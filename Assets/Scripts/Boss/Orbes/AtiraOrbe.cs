using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiraOrbe : AtiraInimigo
{
    public bool rajadaAtivada;
    [SerializeField] private float tempoRecarregaRajada;
    [SerializeField] private int numTirosRajada;
    [SerializeField] private int rajadaCarregada = 0;

    private const int idRajada = 1;
    private const int idRaio = 2;

    // void Start() {
    //     tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().inimigo;
    //     atirador = transform.parent.gameObject;
    //     balaCarregada = true;

    //     GetValores();

    //     //StartCoroutine(Recarrega(tempoRecarregaRajada));

    //     StartCoroutine(AtiraRajada());
    // }

    // private IEnumerator AtiraRajada() {
    //     while (true) {
    //         print("while true deu certo");

    //         if  (rajadaAtivada) {
    //             tipoBala = idRajada;

    //             rajadaCarregada = numTirosRajada;

    //             print("rajadaAtivada = true");

    //             while (rajadaCarregada > 0) {
    //                 print("rajadaCarregada = " + rajadaCarregada);

    //                 if (balaCarregada) {
    //                     print("balaCarregada = AAAAGAGH");
    //                     //AtiraServerRpc(tipoBala);
    //                     rajadaCarregada--;
    //                 }

    //                 yield return null;
    //             } 
                
    //             print("recarregando");
    //             yield return new WaitForSeconds(tempoRecarregaRajada);
    //         }
    //         else {
    //             print("womp womp");
    //             tipoBala = idRaio;
    //             //AtiraServerRpc(tipoBala);
    //         }

    //         yield return null;
    //     }
    // }
}
