using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Herdar desta classe pra pegar certas funções gerais
public class FuncoesGerais : MonoBehaviour
{
    // Pega o ângulo entre duas posições.
    protected float GetAngulo(Vector3 pos1, Vector3 pos2) {
        float angulo;
        float distanciaX = pos2.x - pos1.x;
        float distanciaY = pos2.y - pos1.y;

        angulo = Mathf.Atan2(distanciaX, distanciaY);       // Pega o arco tangente da tangente do triângulo retângulo cuja hipotenusa é uma linha 
        return angulo;                                      // imaginária entre pos1 e pos2. O arco tangente da tangente de um triângulo 
    }                                                       // é igual ao ângulo. Mathf.Rad2Deg transforma radianos em graus.

    // Retorna um vetor que aponta na direção do ângulo.
    protected Vector2 AnguloPraVetor(float angulo) {
        return new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
    }

    // Transforma o número em -1 ou 1, dependendo do sinal do número.
    protected float TransformaEm1(float num) {
        if (num != 0) return num/Mathf.Abs(num);
        else return 0;
    }

    // Faz TransformaEm1 em um vetor inteiro.
    protected Vector3 TransformaVetorEm1(Vector3 vetor) {
        return new Vector3(TransformaEm1(vetor.x), TransformaEm1(vetor.y), TransformaEm1(vetor.z));
    }

    // Retorna -1 ou 1 aleatoriamente.
    protected int SinalAleatorio() {
        int sinal = (int) Mathf.Pow(-1, (int)Random.Range(1, 3)); // Eleva -1 a um número que ou é 1 ou é 2 (tem uma probabilidade bem baixa de ser 3 também,
        return sinal;                                             // mas a probabilidade chega a ser negligível), gerando ou 1 ou -1 como resultado.
    }

    // Converte um ângulo de -180 a 180 a um ângulo de 0 a 360.
    protected float ConvertePra360 (float ang) {
        return ang < 0 ? 360 + ang : ang;
    }      

    // Retorna o menor de dois números.
    protected float MenorNumero (float n1, float n2) {
        return n1 < n2 ? n1 : n2;
    }

    // Retorna o menor dos valores absolutos de dois números.
    protected float MenorNumeroAbs (float n1, float n2) {
        return Mathf.Abs(n1) < Mathf.Abs(n2) ? n1 : n2;
    }

    // Retorna o GameObject mais próximo, ou null se não achar.
    protected GameObject ProcuraObjMaisProximo(string tagAlvo) {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);

        if(alvos.Length == 0) {
            return null;
        }
        else {
            GameObject maisProximo = null;
            float menorDistancia = Mathf.Infinity;

            foreach (GameObject obj in alvos) {
                Vector3 diferencaVetorial = obj.transform.position - transform.position;
                float distancia = diferencaVetorial.sqrMagnitude;

                if (distancia < menorDistancia) {
                    maisProximo = obj;
                    menorDistancia = distancia;
                }
            }

            return maisProximo;
        }
    }
}
