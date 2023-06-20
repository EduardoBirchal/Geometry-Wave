using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;

public class NetworkStart : MonoBehaviour
{
    [SerializeField] private GameObject hostBtn;
    [SerializeField] private GameObject clientBtn;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject spawner;
    public static bool isSingleplayer = false;
    public static bool gameStarted = false;
    public int MaxNumPlayers;

    private void Start()
    {
        MaxNumPlayers = isSingleplayer ? 1 : 4;
        GameObject.Find("NetworkManager").GetComponent<UnityTransport>().ConnectionData.Address = MenuManager.texto_ip;
        if(isSingleplayer == true)
        {
            NetworkManager.Singleton.StartHost();
            
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
            startBtn.SetActive(false);
            
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            gameStarted = true;
        }
        else if(MenuManager.texto_ip == GetLocalIPv4())
        {
            Debug.LogWarning(GetLocalIPv4());
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            NetworkManager.Singleton.StartClient();
        }
        /*
        hostBtn.GetComponent<Button>().onClick.AddListener(() => {
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
            startBtn.SetActive(true);
        });

        clientBtn.GetComponent<Button>().onClick.AddListener(() => {
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
        });
        */
        startBtn.GetComponent<Button>().onClick.AddListener(() => {
            gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            startBtn.SetActive(false);
        });

    }

    public static string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.First(
        f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .ToString();
    }
}