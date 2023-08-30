using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
public class Atirador : FuncoesGerais
{
    public GameObject atirador;
    public AudioSource fonteAudio;
    [SerializeField] private AudioClip[] efeitosArmas;
    [SerializeField] protected int childIndex;
    protected TipoBala[] tipos;
    protected bool balaCarregada = true;

    // Calcula o ângulo da bala e cria ela no ângulo
    protected void CriaBala(TipoBala bala)
    {
        for (int i=0; i<bala.numBalas; i++) { // Repete pra cada bala
            float anguloBala = 1f; 
            // Se numBalas=1, o trecho "arcoTiro/(bala.numBalas-1)" vai tentar dividir por 0, então quando numBalas=1, o ângulo vira 1 sem fazer o cálculo

            if (bala.numBalas > 1) anguloBala = ((bala.arcoTiro/(bala.numBalas-1)) * i) - (bala.arcoTiro/2); 
            // Subtrai arcoTiro/2 porque desse jeito atira dos dois lados. Se, por exemplo, arcoTiro fosse 90, a primeira bala ia ser criada no ângulo -45 e a última, em +45

            // Com quaternions, não dá pra somar, mas multiplicação faz o mesmo efeito que soma. Não pergunta.
            GameObject balaCriada = Instantiate(bala.prefab, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, (anguloBala + Random.Range(bala.imprecisaoBala * -1, bala.imprecisaoBala))))); // Soma ou subtrai um ângulo aleatório de no máximo [imprecisaoBala]
            
            balaCriada.GetComponent<NetworkObject>().Spawn();
            balaCriada.GetComponent<MoveConstante>().CorrectPositionClientRpc(transform.parent.GetComponent<NetworkObject>(), childIndex);
            
            ConfiguraEstatisticasBala(balaCriada, bala);
        }
    }

    protected void ConfiguraEstatisticasBala(GameObject balaCriada, TipoBala tipo) {
        AcertaAlvo scriptAcerto = GetScriptAcerto(balaCriada);

        balaCriada.GetComponent<MoveConstante>().velocidade.Value = tipo.velBala;
        scriptAcerto.dano = tipo.danoBala;
        scriptAcerto.perfuracaoBala = tipo.perfuracao;
    }

    protected AcertaAlvo GetScriptAcerto(GameObject bala) {
        AcertaAlvo scriptAcerto = bala.GetComponent<AcertaInimigo>();

        if (scriptAcerto) return scriptAcerto;
        else return bala.GetComponent<AcertaPlayer>();
    }

    // Espera um tempo e recarrega a arma
    protected IEnumerator Recarrega(float tempo) {
        yield return new WaitForSeconds(tempo);

        balaCarregada = true;
    }

    // Checa se a arma está carregada e, se sim, atira, descarrega a arma e chama a função de recarregar
    protected void Atira(int balaTipo)
    {
        if(balaCarregada)
        {
            if (fonteAudio) {
                PlayGunSoundClientRpc(balaTipo);
            }
            
            balaCarregada = false;
            CriaBala(tipos[balaTipo]);
            StartCoroutine(Recarrega(Random.Range(tipos[balaTipo].cooldownTiro_Min,tipos[balaTipo].cooldownTiro_Max)));
        }
    }

    [ServerRpc]
    protected void AtiraServerRpc(int balaTipo)
    { Atira(balaTipo); }
    
    [ClientRpc]
    private void PlayGunSoundClientRpc(int balaTipo)
    { fonteAudio.PlayOneShot(efeitosArmas[balaTipo], 1); }
}
