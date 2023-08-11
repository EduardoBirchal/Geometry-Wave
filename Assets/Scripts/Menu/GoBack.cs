using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{

    private MenuLvL_Up lvl_UpMenu;
    private MenuInGame gameMenu;
    private MenuManager menuGeral;
    private TimeManager timeManager;

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
        if(GameObject.Find("GameManager").GetComponent<MenuInGame>() != null && menus.Count <= 1){
            if(Input.GetKeyDown("escape") && gameMenu.menu.activeSelf == false && lvl_UpMenu.menuLvL_Up.activeSelf == false)
            {
                MenuInGame.isOpen = true;
                timeManager.Pause();
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

    public void SaveSettings()
    {
        if(menus.Peek() == menuGeral.gameplayOptions)
        {
            if(menuGeral.toggle_AutoAim.isOn == false){
                PlayerPrefs.SetInt("AutoAim", 0);
            }
            else PlayerPrefs.SetInt("AutoAim", 1); 

            if(menuGeral.toggle_AutoFire.isOn == false){
                PlayerPrefs.SetInt("AutoFire", 0);
            }
            else PlayerPrefs.SetInt("AutoFire", 1);
        }
        else if(menus.Peek() == menuGeral.graficoshud)
        {
            PlayerPrefs.SetFloat("HudSizeValue", menuGeral.sliderHud.value);
            PlayerPrefs.Save();
        }
        else if(menus.Peek() == menuGeral.soundOptions)
        {
            PlayerPrefs.SetFloat("SliderVolGeral", menuGeral.VolGeral.value);
            PlayerPrefs.SetFloat("SliderVolTiro", menuGeral.VolTiro.value);
            PlayerPrefs.SetFloat("SliderVolWave", menuGeral.VolWave.value);
            PlayerPrefs.Save();
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
