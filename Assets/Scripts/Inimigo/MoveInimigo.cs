using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : FuncoesGerais
{
    public GameObject player;
    public float velocidade, velVirar; // velVirar é em radianos
    public bool mudaAngulo, move = true;
    Vector3 posPlayer;
    Vector3 posInimigo;
    public Vector3 novaDirecao;

    void Start() 
    {
        player = GameObject.Find("Player");
        transform.eulerAngles = AnguloPraVetor(GetAngulo(transform.position, player.transform.position) * Mathf.Rad2Deg * -1); // Vira pro player

        Physics2D.IgnoreLayerCollision(3, 6); // Ignora colisões de inimigos (na camada de colisão 3: inimigo) 
    }                                         // com balas de inimigo (na camada de colisão 6: balas inimigo)

    void Update()
    {
        MiraPlayer();
        if (move) MoveFrente();
    }

    void MiraPlayer() {
        posPlayer = player.transform.position;
        posInimigo = transform.position;

        
        if (mudaAngulo) ViraProPlayer();                              
    }                                                                                  
    
    void ViraProPlayer() {
        float meuAngulo = transform.eulerAngles.z;
        float anguloPlayer = GetAngulo(posInimigo, posPlayer) * Mathf.Rad2Deg * -1;

        anguloPlayer = ConvertePra360(anguloPlayer); 
        // O jeito que os ângulos existem na Unity é que os valores deles não são de 0 a 360, mas sim de -179 a 180.
        // Isso torna todos os cálculos um pesadelo e essa função desfaz isso.
        
        float diferencaAngulo = anguloPlayer - meuAngulo;
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

        float velVirarAtual = velVirar * Mathf.Sign(diferencaEscolhida);

        if (Mathf.Abs(diferencaEscolhida) >= velVirar) { // Se a diferença for menor que a velocidade de virar, o inimigo vai ter uma "convulsão",
            transform.eulerAngles = new Vector3(         // porque ele sempre vai virar mais do que precisa, tentar corrigir, virar mais do que
                transform.eulerAngles.x,                 // precisa, ad infinitum.
                transform.eulerAngles.y,
                transform.eulerAngles.z + velVirarAtual
            );
        }
    }

    void MoveFrente() {
        transform.Translate(new Vector3(0, velocidade * Time.deltaTime, 0));
    }
}
