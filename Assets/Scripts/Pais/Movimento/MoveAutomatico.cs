using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveAutomatico : FuncoesGerais
{
    public float velVirar;
    public NetworkVariable<float> velocidade = new(
        value:1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    protected void MoveFrente() {
        transform.Translate(new Vector2(0, velocidade.Value * Time.deltaTime));
    }

    protected void MoveDireita() {
        transform.Translate(new Vector2(velocidade.Value * Time.deltaTime, 0));
    }

    public void ViraPraObjeto(Vector3 posAlvo, bool inverter) {
        Vector3 posInimigo = transform.position;

        float meuAngulo = transform.eulerAngles.z;
        float anguloAlvo = GetAngulo(posInimigo, posAlvo) * Mathf.Rad2Deg * -1;

        anguloAlvo = ConvertePra360(anguloAlvo); 
        // O jeito que os ângulos existem na Unity é que os valores deles não são de 0 a 360, mas sim de -179 a 180.
        // Isso torna todos os cálculos um pesadelo e essa função desfaz isso.

        if (inverter)
            anguloAlvo += 180;
        
        float diferencaAngulo = anguloAlvo - meuAngulo;
        float diferencaAnguloInvertida = (Mathf.Abs(diferencaAngulo) - 360) * Mathf.Sign(diferencaAngulo);
        /*
        Num círculo existem duas diferenças entre dois ângulos. diferencaAnguloInvertida (d.A.I.) representa a segunda diferença.
        Para representar que é a segunda diferença, ele sempre vai ter o sinal inverso da diferencaAngulo (d.A.).
        Já que d.A.I. é [d.A. - 360], e d.A. nunca pode ser maior que 360, d.A.I vai sempre ser negativo. Para ele ser o oposto
        do sinal de d.A., nós multiplicamos ele pelo sinal de d.A. Se d.A. é positivo, estaremos multiplicando d.A.I. por 1,
        deixando o sinal dele inalterado. Já que d.A.I. é sempre negativo, o sinal inalterado dele é -1. Se d.A. for negativo,
        estaremos multiplicando ele por -1, transformando o sinal dele em +1.

        Esse provavelmente é o maior comentário que eu já escrevi.
        */

        float diferencaEscolhida = MenorNumeroAbs(diferencaAngulo, diferencaAnguloInvertida);
        // O sinal das diferenças apenas serve para facilitar o cálculo de direção, então não queremos ligar pro sinal
        // quando escolhemos a menor das duas.

        float velVirarAtual = velVirar * Mathf.Sign(diferencaEscolhida) * Time.deltaTime;

        if (Mathf.Abs(diferencaEscolhida) >= velVirar * Time.deltaTime) { // Se a diferença for menor que a velocidade de virar, o inimigo vai ter uma
            transform.eulerAngles = new Vector3(         // "convulsão", porque ele sempre vai virar mais do que precisa, tentar corrigir, virar mais
                transform.eulerAngles.x,                 // do que precisa, ad infinitum.
                transform.eulerAngles.y,
                transform.eulerAngles.z + velVirarAtual
            );
        }
        else {
            transform.eulerAngles = new Vector3(         // Se a diferença for maior, vira pra cobrir a diferença. Desse jeito, inimigos com
                transform.eulerAngles.x,                 // velocidades muito grandes não ficam só parados nem miram imprecisamente.
                transform.eulerAngles.y,
                transform.eulerAngles.z + diferencaEscolhida
            );
        }
    }
}
