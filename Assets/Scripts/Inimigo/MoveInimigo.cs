using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : MoveAutomatico
{
    public GameObject player;
    public bool mudaAngulo, move;

    void Start() 
    {
        GetValores();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.eulerAngles = AnguloPraVetor(GetAngulo(transform.position, player.transform.position) * Mathf.Rad2Deg * -1); // Vira pro player

        Physics2D.IgnoreLayerCollision(3, 6); // Ignora colisões de inimigos (na camada de colisão 3: inimigo) com balas
    }                                         // de inimigo (na camada de colisão 6: balas inimigo)

    void Update()
    {
        if(!IsHost ) return;
        
        player = ProcuraObjMaisProximo("Player");
        if(player == null)
        player = this.gameObject;

        if (mudaAngulo) ViraPraObjeto(player.transform.position, false);
        if (move) MoveFrente();
    }

    void GetValores() {
        ValoresSpawn valSpawn = GetComponent<ValoresSpawn>();

        if (valSpawn && IsHost)
            velocidade.Value = valSpawn.velMovimento;
    }
}
