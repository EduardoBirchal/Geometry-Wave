using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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
    private MoveInimigo mvInimigo;

    void Start() {
        tipos = GameObject.Find("GameManager").GetComponent<TipoTiro>().inimigo;
        atirador = transform.parent.gameObject;
        balaCarregada = false;
        rajadaCarregada = numTirosRajada;
        mvInimigo = GetComponent<MoveInimigo>();

        GetValores();

        if (IsHost) {
            StartCoroutine(Recarrega(tempoRecarregaRajada));
            StartCoroutine(AtiraRajada());
        }
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
                        AtiraEmTodosOsPlayersServerRpc(tipoBala);
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

    // O orbe atira uma bala em cada player
    protected void AtiraEmTodosOsPlayers(int balaTipo)
    {
        if(balaCarregada)
        {
            balaCarregada = false;

            foreach (GameObject playerAtual in GameObject.FindGameObjectsWithTag("Player")) {
                mvInimigo.ViraPraObjeto(playerAtual.transform.position, false);
                CriaBala(tipos[balaTipo]);
            }
            
            StartCoroutine(Recarrega(Random.Range(tipos[balaTipo].cooldownTiro_Min,tipos[balaTipo].cooldownTiro_Max)));
        }
    }

    [ServerRpc]
    protected void AtiraEmTodosOsPlayersServerRpc(int balaTipo)
    {   
        AtiraEmTodosOsPlayers(balaTipo);
    }
}
