using UnityEngine;
public class MenuInGame : MonoBehaviour 
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject quitConfirmation;
    [SerializeField] private GameObject diedScreen;
    private TimeManager timeManager;

    public PlayerGerenciaHP playerhp;
    private SceneFadeAnimation fade;
    public GameObject player;

    void Start()
    {
        timeManager = GameObject.Find("Funcoes").GetComponent<TimeManager>();
        player = GameObject.Find("Player");
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        GetPlayerHP();
        menu.SetActive(false);
        timeManager.Resume();
    }

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Esc();
        }
        
        if(player != null){
            if(playerhp.hp <= 0)
            {
                Time.timeScale = 0;
                diedScreen.SetActive(true);
            }
        }
    }

    public void GetPlayerHP()
    {
        if(player != null) playerhp = GameObject.Find("Player").GetComponent<PlayerGerenciaHP>();
    }

    public void QuitConfirmation()
    {
        quitConfirmation.SetActive(true);
        menu.SetActive(false);
    }

    public void CloseQuitConfirmation()
    {
        menu.SetActive(true);
        quitConfirmation.SetActive(false);
    }

    public void Inicio()
    {
        Time.timeScale = 1;
        Error.onScreen = false;
        fade.FadeToMenu();
    }

    public void Esc()
    {
        if(menu.activeSelf == true)
        {
            menu.SetActive(false);
            timeManager.Resume();
        }
        else
        {
            menu.SetActive(true);
            timeManager.Pause();
        }
    }
}
