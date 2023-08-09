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
        Time.timeScale = 1;
        if(menus.Count > 0){
            menus.Peek().SetActive(false);
            menus.Pop();
        }
    }
}
