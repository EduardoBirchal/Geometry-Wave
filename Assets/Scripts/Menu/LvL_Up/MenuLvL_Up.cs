using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


using TMPro;

public class MenuLvL_Up : MonoBehaviour
{
    [SerializeField] public GameObject menuLvL_Up;
    [SerializeField] private TextMeshProUGUI [] texto;
    [SerializeField] private TextMeshProUGUI notificacao, points;
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

    public void MudaTextoPontosGastos(int posicao, int pontosGastos, int limite)
    {
        texto[posicao].text = pontosGastos + "/" + limite;
    }

    public void MudaTextoPontosDisponiveis(int valor)
    {
        points.text = "Pontos de level up disponíveis: " +  valor;
    }

    public void Notification(string text)
    {
        notificacao.text = text;
    }

    public void Menu()
    {
        if(MenuInGame.isOpen == false && menuLvL_Up.activeSelf == false && !PlayerGerenciaHP.isDead)
        {
            goBack.menus.Push(menuLvL_Up);
            menuLvL_Up.SetActive(true);
            if(NetStatus.isSingleplayer == true)
                timeManager.Pause();
        }    
    }
}