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
    
    public void Start()
    {
        player = new TipoBala[]{
        // METRALHADORA
        new TipoBala(
            1, // Número de balas
            1f, // Dano
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
            1,
            10f, 
            26f, 
            1f, 
            1f, 
            5f, 
            40f,
            3,
            "Sniper",
            prefab_balaPlayer 
        ),
        // SHOTGUN
        new TipoBala(
            5, 
            5f, 
            9f, 
            0.75f, 
            0.75f, 
            0f, 
            40f,
            1,
            "Espingarda",
            prefab_balaPlayer
        ),
        // GUIDED
        new TipoBala(
            1, 
            2f, 
            6f, 
            0.3f, 
            0.3f, 
            90f, 
            90f,
            1,
            "Mísseis", 
            prefab_balaGuiada
        )};

        inimigo = new TipoBala[]{
            // ENEMY BASE SHOOT
            new TipoBala(
                1,
                2,
                6f,
                0.75f,
                2f,
                0,
                40,
                1,
                "Matador",
                prefab_balaInimigo
            )
        };
    }
}

