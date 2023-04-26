using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipoBala
{
    public int numBalas;
    public float velBala, cooldownTiro, imprecisaoBala, arcoTiro, danoBala;
    public string nomeTipo;
    public GameObject obj;

    public TipoBala (int num, float dano, float velB, float cooldown, float imprec, float arco, string nome, GameObject obj) { // Bob o construtor
        numBalas = num;
        danoBala = dano;
        velBala = velB;
        cooldownTiro = cooldown;    // Em segundos
        imprecisaoBala = imprec;    // Em graus
        arcoTiro = arco;            // Em graus
        nomeTipo = nome;
        this.obj = obj;
    }
}


public class Atirador : FuncoesGerais
{
    public GameObject atirador;
    protected bool balaCarregada = true;

    TipoBala[] tipos;

    // Calcula o ângulo da bala e cria ela no ângulo
    protected void CriaBala(TipoBala bala) {
        for (int i=0; i<bala.numBalas; i++) { // Repete pra cada bala
            float anguloBala = 1f; 
            // Se numBalas=1, o trecho "arcoTiro/(bala.numBalas-1)" vai tentar dividir por 0, então quando numBalas=1, o ângulo vira 1 sem fazer o cálculo

            if (bala.numBalas > 1) anguloBala = ((bala.arcoTiro/(bala.numBalas-1)) * i) - (bala.arcoTiro/2); 
            // Subtrai arcoTiro/2 porque desse jeito atira dos dois lados. Se, por exemplo, arcoTiro fosse 90, a primeira bala ia ser criada no ângulo -45 e a última, em +45

            GameObject balaCriada = Instantiate(bala.obj, transform.position, atirador.transform.rotation * Quaternion.Euler(new Vector3(0, 0, (anguloBala + Random.Range(bala.imprecisaoBala * -1, bala.imprecisaoBala))))); // Soma ou subtrai um ângulo aleatório de no máximo [imprecisaoBala]
            // Com quaternions, não dá pra somar, mas multiplicação faz o mesmo efeito que soma. Não pergunta.
        }
    }

    // Espera um tempo e recarrega a arma
    protected IEnumerator Recarrega(float tempo) {
        yield return new WaitForSeconds(tempo);

        balaCarregada = true;
    }

    // Checa se a arma está carregada e, se sim, atira, descarrega a arma e chama a função de recarregar
    protected void AtiraBala(TipoBala bala) {
        if(balaCarregada) {
            balaCarregada = false;

            CriaBala(bala);
            StartCoroutine(Recarrega(bala.cooldownTiro));
        }
    }
}
