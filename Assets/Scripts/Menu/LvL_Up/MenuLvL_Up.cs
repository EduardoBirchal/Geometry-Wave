using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuLvL_Up : MonoBehaviour
{
    [SerializeField] public GameObject menuLvL_Up;
    [SerializeField] private TextMeshProUGUI [] texto;

    private TimeManager timeManager;
    private GoBack goBack;

    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<TimeManager>();
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
    }

    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            Menu();
        }
    }

    public void MudaText(int posicao, int pontosGastos, int limite)
    {
        texto[posicao].text = pontosGastos + "/" + limite;
    }

    public void Menu()
    {
        if(MenuInGame.isOpen == false && menuLvL_Up.activeSelf == false)
        {
            goBack.menus.Push(menuLvL_Up);
            menuLvL_Up.SetActive(true);
            if(NetStatus.isSingleplayer == true)
                timeManager.Pause();
        }    
    }
}