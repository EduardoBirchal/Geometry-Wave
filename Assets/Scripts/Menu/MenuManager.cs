using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private string nomeDaFase;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] private GameObject gameplayOptions;
    [SerializeField] private GameObject soundOptions;
    [SerializeField] private GameObject moreOptions;
    [SerializeField] private GameObject inicio;


    public void Jogar()
    {
        SceneManager.LoadScene(nomeDaFase);
    }

    public void Continuar()
    {
        menuInicial.SetActive(false);
    }

    public void Options()
    {
        menuInicial.SetActive(false);
        painelOptions.SetActive(true);
    }

    public void GamePlayOptions()
    {
        gameplayOptions.SetActive(true);
        painelOptions.SetActive(false);
    }

    public void SoundOptions()
    {
        soundOptions.SetActive(true);
        painelOptions.SetActive(false);
    }

    public void MoreOptions()
    {
        moreOptions.SetActive(true);
        painelOptions.SetActive(false);
    }

    public void CloseGamePlayOptions()
    {
        painelOptions.SetActive(true);
        gameplayOptions.SetActive(false);
    }

    public void CloseSoundOptions()
    {
        painelOptions.SetActive(true);
        soundOptions.SetActive(false);
    }

    public void CloseMoreOptions()
    {
        painelOptions.SetActive(true);
        moreOptions.SetActive(false);
    }

    public void CloseOptions()
    {
        painelOptions.SetActive(false);
        menuInicial.SetActive(true);
    }

    public void ExitGame()
    {
        print("Saiu do jogo");
        Application.Quit();
    }
}
