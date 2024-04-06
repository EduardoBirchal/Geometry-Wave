using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{

    private MenuLvL_Up lvl_UpMenu;
    private MenuInGame gameMenu;
    private MenuManager menuGeral;
    private TimeManager timeManager;

    [SerializeField] private GameObject rebindingPanel;
    public Stack<GameObject> menus = new Stack<GameObject>();

    void Start()
    {
        lvl_UpMenu = gameObject.GetComponent<MenuLvL_Up>();
        gameMenu = gameObject.GetComponent<MenuInGame>();
        menuGeral = gameObject.GetComponent<MenuManager>();
        timeManager = GameObject.Find("GameManager").GetComponent<TimeManager>();
    }

    void Update()
    {
        bool isPressionadoEsc = Input.GetKeyDown("escape");
        bool menuInGameExist = GameObject.Find("GameManager").GetComponent<MenuInGame>() != null;

        if(menuInGameExist && menus.Count <= 1){

            bool menusDisable = gameMenu.menu.activeSelf == false && lvl_UpMenu.menuLvL_Up.activeSelf == false;

            if(isPressionadoEsc && menusDisable)
            {
                gameMenu.OpenMenu();
            }
            else if(isPressionadoEsc )
            {
                Continuar();
            }
        }
        else if(isPressionadoEsc)
        {
            Debug.Log(rebindingPanel.activeSelf);
            if(menus.Count > 0 &&  rebindingPanel.activeSelf == false)
            {
                GoToLastMenu();
            }
            Debug.Log(rebindingPanel.activeSelf);

        }
    }

    public void SaveSettings()
    {
        if(menus.Peek() == menuGeral.gameplayOptions)
        {
            menuGeral.SaveAutomatics();
        }
        else if(menus.Peek() == menuGeral.graficoshud)
        {
            menuGeral.SaveHudSize();
        }
        else if(menus.Peek() == menuGeral.soundOptions)
        {
            menuGeral.SaveVolume();
        }
    }

    public void GoToLastMenu(){
        SaveSettings();
        menus.Peek().SetActive(false);
        menus.Pop();
    }

    public void Continuar()
    {
        MenuInGame.isOpen = false;
        timeManager?.Resume();
        if(menus.Count > 0){
            menus.Peek().SetActive(false);
            menus.Pop();
        }
    }
}
