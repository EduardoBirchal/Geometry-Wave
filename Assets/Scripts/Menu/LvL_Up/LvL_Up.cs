using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvL_Up : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;
    private GameObject player;
    private PlayerDash playerDash;
    private PlayerGerenciaHP playerHp;
    private PlayerGerenciaXP playerXp;
    private int qntUpgrades = 0;

    void Update()
    {
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

    public void GetPlayerComponents(GameObject player)
    {
        playerHp = player.GetComponent<PlayerGerenciaHP>();
        playerXp = player.GetComponent<PlayerGerenciaXP>();
        playerDash = player.GetComponent<PlayerDash>();
    }

    public void AumentaHP()
    {
        if(playerHp != null && MaxUpgrades()){
            playerHp.hp.Value += 2;
            playerHp.maxHp.Value += 2;
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
}
