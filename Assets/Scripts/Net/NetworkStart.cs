using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkStart : MonoBehaviour
{
    [SerializeField] private GameObject hostBtn;
    [SerializeField] private GameObject clientBtn;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject spawner;
    public static bool isSingleplayer = true;
    public static bool gameStarted = false;
    public int MaxNumPlayers;

    private void Start()
    {
        MaxNumPlayers = isSingleplayer ? 1 : 4;
    }

    private void Awake()
    {
        if(isSingleplayer == true)
        {
            NetworkManager.Singleton.StartHost();
            
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
            startBtn.SetActive(false);
            
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            gameStarted = true;
        }
        
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
            gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            startBtn.SetActive(false);
        });
    }
}