using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject quitConfirmation;
    [SerializeField] private GameObject diedScreen;

    public PlayerGerenciaHP playerhp;
    private SceneFadeAnimation fade;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        GetPlayerHP();
        Continuar();

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
        fade.FadeToMenu();
    }

    public void Esc()
    {
        if(Time.timeScale == 0 && menu.activeSelf == true){
            Continuar();
        }
        else if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }

    public void Continuar()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

}
