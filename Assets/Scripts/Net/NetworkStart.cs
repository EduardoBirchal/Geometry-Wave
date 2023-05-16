using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkStart : MonoBehaviour
{
    [SerializeField] private GameObject hostBtn;
    [SerializeField] private GameObject clientBtn;
    [SerializeField] private GameObject startBtn;
    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Awake()
    {
        hostBtn.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
            startBtn.SetActive(true);
        });
        clientBtn.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
            Time.timeScale = 1;
        });
        startBtn.GetComponent<Button>().onClick.AddListener(() => {
            startBtn.SetActive(false);
            Time.timeScale = 1;
        });
    }
}