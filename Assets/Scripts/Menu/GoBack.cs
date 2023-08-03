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
        if(GameObject.Find("GameManager").GetComponent<MenuInGame>() != null && menus.Count <= 1){
            if(Input.GetKeyDown("escape") && gameMenu.menu.activeSelf == false && lvl_UpMenu.menuLvL_Up.activeSelf == false)
            {
                MenuInGame.isOpen = true;
                Time.timeScale = 0;
                menus.Push(gameMenu.menu);
                gameMenu.menu.SetActive(true);
            }
            else if(Input.GetKeyDown("escape") )
            {
                Continuar();
            }
        }
        else if(Input.GetKeyDown("escape"))
        {
            if(menus.Count > 0)
            {
                GoToLastMenu();
            }
        }
    }

    public void GoToLastMenu(){
        menus.Peek().SetActive(false);
        menus.Pop();
    }

    public void Continuar()
    {
        MenuInGame.isOpen = false;
        Time.timeScale = 1;
        if(menus.Count > 0){
            menus.Peek().SetActive(false);
            menus.Pop();
        }
    }
}
