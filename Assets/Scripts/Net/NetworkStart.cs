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
        });
        startBtn.GetComponent<Button>().onClick.AddListener(() => {
            GetComponent<NetworkState>().gameStarted = true;
            startBtn.SetActive(false);
        });
    }
}