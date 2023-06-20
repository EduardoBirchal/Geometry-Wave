using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class SceneFadeAnimation : MonoBehaviour
{
    public Animator animator;
    public int levelToLoad;

    public void FadeScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void FadetoNextLevel()
    {
        FadeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void FadeToMenu()
    {
        NetworkManager.Singleton.Shutdown();
        if(NetworkManager.Singleton != null) Destroy(NetworkManager.Singleton.gameObject);
        FadeScene(0);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
