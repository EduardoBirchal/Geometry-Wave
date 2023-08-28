using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MenuLvL_Up : MonoBehaviour
{
    
    [SerializeField] public GameObject menuLvL_Up;
    [SerializeField] private InputActionReference openMenu;
    
    private GoBack goBack;
    private bool singlePlayer;

    //Input Actions Functions

    private void OnEnable() {
        openMenu.action.Enable();     
        openMenu.action.performed += OpenMenu;
    }

    private void OnDisable() {
        openMenu.action.Disable();    
        openMenu.action.performed -= OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext value)
    {
        Menu();
    }

    void Start()
    {
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
        singlePlayer = NetworkStart.isSingleplayer;
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown("p"))
    //     {
    //         Menu();
    //     }

    // }

    public void Menu()
    {
        if(MenuInGame.isOpen == false && menuLvL_Up.activeSelf == false)
        {
            goBack.menus.Push(menuLvL_Up);
            menuLvL_Up.SetActive(true);
            if(singlePlayer == true)
            {
                Time.timeScale = 0;
            }
        }    
    }

}