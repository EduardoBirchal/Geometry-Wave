using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiraOrbe : AtiraInimigo
{
    [SerializeField] private float tempoRecarregaRajada;
    [SerializeField] private int numTirosRajada;
    private int rajadaCarregada = 0;

    void Start() {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().inimigo;
        atirador = transform.parent.gameObject;
        balaCarregada = false;

        GetValores();

        StartCoroutine(Recarrega(tempoRecarregaRajada));

        StartCoroutine(AtiraRajada());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AtiraRajada() {
        while (true) {
            rajadaCarregada = numTirosRajada;

            while (rajadaCarregada > 0) {
                if (balaCarregada) {
                    print("Tiros sobrando na rajada: " + rajadaCarregada);
                    AtiraServerRpc(tipoBala);
                    rajadaCarregada--;
                }

                yield return null;
            } 
        
            print("Recarregando...");
            yield return new WaitForSeconds(tempoRecarregaRajada);
        }
    }
}
