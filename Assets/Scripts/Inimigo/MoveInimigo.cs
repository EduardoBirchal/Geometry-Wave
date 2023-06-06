using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : MoveAutomatico
{
    public GameObject player;
    public bool mudaAngulo, move = true;

    void Start() 
    {
        player = GameObject.Find("Player");
        transform.eulerAngles = AnguloPraVetor(GetAngulo(transform.position, player.transform.position) * Mathf.Rad2Deg * -1); // Vira pro player

        Physics2D.IgnoreLayerCollision(3, 6); // Ignora colisões de inimigos (na camada de colisão 3: inimigo) com balas
    }                                         // de inimigo (na camada de colisão 6: balas inimigo)

    void Update()
    {
        if (mudaAngulo) ViraPraObjeto(player.transform.position); 
        if (move) MoveFrente();
    }
}
