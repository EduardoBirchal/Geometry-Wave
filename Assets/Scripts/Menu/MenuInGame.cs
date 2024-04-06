using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] public GameObject menu;
    [SerializeField] private GameObject quitConfirmation, diedScreen;
    
    private PlayerGerenciaHP playerhp;
    private SceneFadeAnimation fade;
    private GameObject player;
    private GoBack goBack;
    private TimeManager timeManager;
    public static bool isOpen = false;

    void Start()
    {
        player = GameObject.Find("Player");
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
        timeManager = GameObject.Find("GameManager").GetComponent<TimeManager>();
        goBack.Continuar();
    }

    public void OpenMenu()
    {
        isOpen = true;
        timeManager.Pause();
        goBack.menus.Push(menu);
        menu.SetActive(true);
    }


    public void QuitConfirmation()
    {
        quitConfirmation.SetActive(true);
        goBack.menus.Push(quitConfirmation);
    }

    public void Inicio()
    {
        GameObject.Find("GameManager").GetComponent<TimeManager>().Resume();
        fade.FadeToMenu();
    }
}