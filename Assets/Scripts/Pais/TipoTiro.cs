using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System.Collections.Generic;

public struct TipoBala
{
    public int numBalas, perfuracao;
    public float velBala, cooldownTiro_Min, cooldownTiro_Max, imprecisaoBala, arcoTiro, danoBala;
    public string nomeTipo;
    public GameObject prefab;

    public TipoBala (int num, float dano, float velB, float cooldownMin, float cooldownMax, float imprec, float arco, int perfuracao, string nome, GameObject prefab) { // Bob o construtor
        this.numBalas = num;
        this.danoBala = dano;
        this.velBala = velB;
        this.cooldownTiro_Max = cooldownMax; // Em segundos
        this.cooldownTiro_Min = cooldownMin; // Em segundos
        this.imprecisaoBala = imprec;        // Em graus
        this.arcoTiro = arco;                // Em graus
        this.perfuracao = perfuracao;        // Em número de inimigos
        this.nomeTipo = nome;
        this.prefab = prefab;
    }
}

public class TipoTiro : MonoBehaviour
{
    [SerializeField] private GameObject prefab_balaPlayer;
    [SerializeField] private GameObject prefab_balaInimigo;
    [SerializeField] private GameObject prefab_balaGuiada;
    public TipoBala[] player;
    public TipoBala[] inimigo;
    
    public void Awake()
    {
        player = new TipoBala[]{
            // METRALHADORA
            new TipoBala(
                1, // Número de balas
                2f, // Dano
                13f, // Velocidade da bala
                0.1f, // Cooldown Minimo (em segundos)
                0.1f, // Cooldown Maximo (em segundos)
                20f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                1, // Perfuração (em número de inimigos)
                "Metralhadora", // Nome
                prefab_balaPlayer // Objeto da bala
            ),
            // SNIPER
            new TipoBala(
                1, // Número de balas
                10f, // Dano
                50f, // Velocidade da bala
                1f, // Cooldown Minimo (em segundos)
                1f, // Cooldown Maximo (em segundos)
                5f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                3, // Perfuração (em número de inimigos)
                "Sniper",
                prefab_balaPlayer 
            ),
            // SHOTGUN
            new TipoBala(
                5, // Número de balas
                2f, // Dano
                9f, // Velocidade da bala
                0.75f, // Cooldown Minimo (em segundos)
                0.75f, // Cooldown Maximo (em segundos)
                0f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                1, // Perfuração (em número de inimigos)
                "Espingarda",
                prefab_balaPlayer
            ),
            // GUIDED
            new TipoBala(
                1, // Número de balas
                2f, // Dano
                6f, // Velocidade da bala
                0.3f, // Cooldown Minimo (em segundos)
                0.3f, // Cooldown Maximo (em segundos)
                90f, // Imprecisão (em graus)
                90f, // Arco de tiro (em graus)
                1, // Perfuração (em número de inimigos)
                "Mísseis", 
                prefab_balaGuiada
            )
        };

        inimigo = new TipoBala[]{
            // ENEMY BASIC WEAPON
            new TipoBala(
                1, // Número de balas
                2f, // Dano
                6f, // Velocidade da bala
                0.75f, // Cooldown Minimo (em segundos)
                2f, // Cooldown Maximo (em segundos)
                0f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                1, // Perfuração (em número de inimigos)
                "Rifle de Replicador Padrão",
                prefab_balaInimigo
            ),
            // BOSS BURST WEAPON
            new TipoBala(
                1, // Número de balas
                3f, // Dano
                30f, // Velocidade da bala
                0.05f, // Cooldown Minimo (em segundos)
                0.05f, // Cooldown Maximo (em segundos)
                8f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                1, // Perfuração (em número de inimigos)
                "Metralhadora de Rajada",
                prefab_balaInimigo
            ),
            // BOSS BEAM WEAPON
            new TipoBala(
                1, // Número de balas
                3f, // Dano
                30f, // Velocidade da bala
                0.005f, // Cooldown Minimo (em segundos)
                0.005f, // Cooldown Maximo (em segundos)
                0f, // Imprecisão (em graus)
                40f, // Arco de tiro (em graus)
                1, // Perfuração (em número de inimigos)
                "Raio de Energia",
                prefab_balaInimigo
            )
        };
    }
}

