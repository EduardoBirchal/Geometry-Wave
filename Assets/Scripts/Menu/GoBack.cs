using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{

    private MenuLvL_Up lvl_UpMenu;
    private MenuInGame gameMenu;
    private MenuManager menuGeral;

    public Stack<GameObject> menus = new Stack<GameObject>();

    void Start()
    {
        lvl_UpMenu = GameObject.Find("GameManager").GetComponent<MenuLvL_Up>();
        gameMenu = GameObject.Find("GameManager").GetComponent<MenuInGame>();
        menuGeral = GameObject.Find("GameManager").GetComponent<MenuManager>();
    }

    void Update()
    {
        // if(Input.GetKeyDown("escape") && gameMenu.menu.activeSelf == false && lvl_UpMenu.menuLvL_Up.activeSelf == false)
        // {
        //     MenuInGame.isOpen = true;
        //     Time.timeScale = 0;
        //     gameMenu.menu.SetActive(true);
        // }
        if(Input.GetKeyDown("escape"))
        {
            GoToLastMenu();
        }
    }

    void GoToLastMenu(){
        menus.Peek().SetActive(false);
        menus.Pop();
    }
}
