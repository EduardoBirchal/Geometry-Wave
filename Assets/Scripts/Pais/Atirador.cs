using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class Atirador : FuncoesGerais
{
    public GameObject atirador;
    protected bool balaCarregada = true;

    TipoBala[] tipos;

    // Calcula o ângulo da bala e cria ela no ângulo
    protected void CriaBala(int balaTipo) {
        TipoBala bala = TipoTiro.tipos[balaTipo];
        // GameObject prefab = Resources.Load<GameObject>("Assets/Prefabs/" + bala.prefab + ".prefab", typeof(GameObject)) as GameObject;
        for (int i=0; i<bala.numBalas; i++) { // Repete pra cada bala
            float anguloBala = 1f; 
            // Se numBalas=1, o trecho "arcoTiro/(bala.numBalas-1)" vai tentar dividir por 0, então quando numBalas=1, o ângulo vira 1 sem fazer o cálculo

            if (bala.numBalas > 1) anguloBala = ((bala.arcoTiro/(bala.numBalas-1)) * i) - (bala.arcoTiro/2); 
            // Subtrai arcoTiro/2 porque desse jeito atira dos dois lados. Se, por exemplo, arcoTiro fosse 90, a primeira bala ia ser criada no ângulo -45 e a última, em +45

            
            GameObject balaCriada = Instantiate(bala.prefab, transform.position, atirador.transform.rotation * Quaternion.Euler(new Vector3(0, 0, (anguloBala + Random.Range(bala.imprecisaoBala * -1, bala.imprecisaoBala))))); // Soma ou subtrai um ângulo aleatório de no máximo [imprecisaoBala]
            balaCriada.GetComponent<NetworkObject>().Spawn();
            // Com quaternions, não dá pra somar, mas multiplicação faz o mesmo efeito que soma. Não pergunta.
        }
    }

    // Espera um tempo e recarrega a arma
    protected IEnumerator Recarrega(float tempo) {
        yield return new WaitForSeconds(tempo);

        balaCarregada = true;
    }

    // Checa se a arma está carregada e, se sim, atira, descarrega a arma e chama a função de recarregar
    [ServerRpc]
    protected void AtiraBalaServerRpc(int balaTipo) {
        if(balaCarregada) {
            balaCarregada = false;

            CriaBala(balaTipo);
            StartCoroutine(Recarrega(TipoTiro.tipos[balaTipo].cooldownTiro));
        }
    }
}
