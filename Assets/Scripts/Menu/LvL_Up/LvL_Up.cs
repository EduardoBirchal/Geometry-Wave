using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvL_Up : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private AcertaInimigo balaComum, balaTeleguiada;
    
    
    private GameObject player;
    private PlayerDash playerDash;
    private PlayerGerenciaHP playerHp;
    private PlayerGerenciaXP playerXp;


    private int qntUpgrades = 0;

    void Start()
    {
        GetPlayerComponents();
    }

    void Update()
    {
        if(playerXp == null){
            GetPlayerComponents();
        }
        if(MaxUpgrades())
        {
            points.text = "Pontos de level up disponíveis: " +  (playerXp.level - qntUpgrades);

        }
    }

    public bool MaxUpgrades()
    {
        if(qntUpgrades < playerXp.level)
        {
            return true;
        }
        else return false;

    }

    public void GetPlayerComponents()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        if(player != null)
        {
            playerHp = player.GetComponent<PlayerGerenciaHP>();
            playerXp = player.GetComponent<PlayerGerenciaXP>();
            playerDash = player.GetComponent<PlayerDash>();
        }
    }

    public void AumentaHP()
    {
        if(playerHp != null && MaxUpgrades()){
            playerHp.hp +=2;
            playerHp.maxHp += 2;
            qntUpgrades++;
        }
    }

    public void DashCooldown()
    {
        if(playerDash != null && MaxUpgrades()){
            playerDash.tempoCarrega -= 0.05f;
            qntUpgrades++;
        }
    }

    public void AumentaDano()
    {
        if(MaxUpgrades()){
            balaComum.dano++;
            balaTeleguiada.dano++;
            qntUpgrades++;
        }
    }

    public void AumentaVelAtaque()
    {
        if(playerDash != null && MaxUpgrades()){
            //atiraPlayer.tipos[0].cooldownMin -= 0.01f;
            //atiraPlayer.tipos[0].cooldownMax -= 0.01f;
            qntUpgrades++;
        }
    }
}