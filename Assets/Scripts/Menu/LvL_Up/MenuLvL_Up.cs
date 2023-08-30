using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MenuLvL_Up : MonoBehaviour
{
    
    [SerializeField] public GameObject menuLvL_Up;
    private TimeManager timeManager;
    [SerializeField] private InputActionReference openMenu;
    
    private GoBack goBack;

    //Input Actions Functions

    private void OnEnable() {
        openMenu.action.Enable();     
        openMenu.action.performed += OpenMenu;
    }

    private void OnDisable() {
        openMenu.action.Disable();    
        openMenu.action.performed -= OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext obj)
    {
        Menu();
    }

    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<TimeManager>();
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
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