using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvL_Up : MonoBehaviour
{
    private GameObject player;
    private PlayerDash playerDash;
    private PlayerGerenciaHP playerHp;
    private PlayerGerenciaXP playerXp;

    private int qntUpgrades = 0;

    void Start()
    {
        GetPlayerComponents();
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
        GetPlayerComponents();
        if(playerHp != null && MaxUpgrades()){
            playerHp.hp +=2;
            playerHp.maxHp += 2;
            qntUpgrades++;
        }
    }

    public void DashCooldown()
    {
        GetPlayerComponents();
        if(playerDash != null && MaxUpgrades()){
            playerDash.tempoCarrega -= 0.05f;
            qntUpgrades++;
        }
    }



}
