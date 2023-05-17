using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AtiraPlayer : Atirador
{
    public int balaAtual = 0;
    private MudaBala mudaBala;
    private TipoBala[] tipos;
    public GameObject objBala, objTeleguiada;

    void Start() {
        mudaBala = atirador.GetComponent<MudaBala>();

        tipos = new TipoBala[] {
            
            // METRALHADORA
            new TipoBala(
                1, // Número de balas
                1f, // Dano
                13f, // Velocidade da bala
                0.1f, // Cooldown (em segundos)
                20f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                "Metralhadora", // Nome
                objBala // Objeto da bala
            ), 

            // KILL AURA
            new TipoBala(
                90,
                100f, 
                20f, 
                0f, 
                0f, 
                360f,
                "KillAura", 
                objTeleguiada
            ), 

            // SHOTGUN
            new TipoBala(
                5, 
                5f, 
                9f, 
                0.75f, 
                0f, 
                40f,
                "Espingarda", 
                objBala
            ),

            // MISSEIS
            new TipoBala(
                1, 
                2f, 
                6f, 
                0.3f, 
                20f, 
                40f,
                "Mísseis", 
                objTeleguiada
            ),
        };
    }

    // Update is called once per frame
    void Update()
    {
        balaAtual = mudaBala.modoTiro;
        Atira();
    }

    void Atira() {
        if(Input.GetMouseButton(0)) {
            AtiraBala(tipos[balaAtual]);
        }
    }
}
