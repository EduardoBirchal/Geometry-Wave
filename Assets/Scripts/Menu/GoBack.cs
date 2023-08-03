using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{

    public MenuLvL_Up lvl_UpMenu;
    public MenuInGame gameMenu;
    public MenuManager menuGeral;

    public Stack<GameObject> menus = new Stack<GameObject>();

    void Start()
    {
        lvl_UpMenu = GameObject.Find("GameManager").GetComponent<MenuLvL_Up>();
        gameMenu = GameObject.Find("GameManager").GetComponent<MenuInGame>();
        menuGeral = GameObject.Find("GameManager").GetComponent<MenuManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown("escape") && gameMenu.menu.activeSelf != true && lvl_Up.menuLvL_Up.activeSelf == false)
        {
            gameMenu.isOpen = true;
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        else if(Input.GetKeyDown("escape"))
        {
            GoToLastMenu();
        }
    }

    void GoToLastMenu(){
        menus.Peek().SetActive(false);
        menus.Pop();
    }
}
