using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuInGame : MonoBehaviour
{
    [SerializeField] public GameObject menu;
    [SerializeField] private GameObject quitConfirmation;
    [SerializeField] private GameObject diedScreen;
    
    private PlayerGerenciaHP playerhp;
    private SceneFadeAnimation fade;
    private GameObject player;
    private GoBack goBack;
    public static bool isOpen = false;

    void Start()
    {
        player = GameObject.Find("Player");
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
        GetPlayerHP();
        goBack.Continuar();

    }



    public void GetPlayerHP()
    {
        if(player != null) playerhp = GameObject.Find("Player").GetComponent<PlayerGerenciaHP>();
    }


    public void QuitConfirmation()
    {
        quitConfirmation.SetActive(true);
        goBack.menus.Push(quitConfirmation);
    }

    public void CloseQuitConfirmation()
    {
        menu.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void Inicio()
    {
        fade.FadeToMenu();
    }

}
