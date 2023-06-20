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
    private NetworkManager netManager;
    public static bool isSingleplayer = true;
    public static bool gameStarted = false;
    public int MaxNumPlayers;

    private void Start()
    {
        MaxNumPlayers = isSingleplayer ? 1 : 4;
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;

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

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        var clientId = request.ClientNetworkId;
        var connectionData = request.Payload;
    
        if(netManager.ConnectedClientsIds.Count < MaxNumPlayers)
        {
            response.Approved = true;
            response.CreatePlayerObject = true;
        }
        else
        {
            // TODO: Mostrar a tela de "sala cheia" e "incapaz de conectar"
            response.Reason = "Nao foi";
            response.Approved = false;
        }
    
        // The prefab hash value of the NetworkPrefab, if null the default NetworkManager player prefab is used
        response.PlayerPrefabHash = null;
    
        // If additional approval steps are needed, set this to true until the additional steps are complete
        // once it transitions from true to false the connection approval response will be processed.
        response.Pending = false;
    }

    public static string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.First(
        f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .ToString();
    }

    private void OnClientDisconnectCallback(ulong obj)
    {
        if (!netManager.IsServer && netManager.DisconnectReason != string.Empty)
        {
            Debug.LogError($"Approval Declined Reason: {netManager.DisconnectReason}");
        }
    }
    
    /*
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        var clientId = request.ClientNetworkId;
        var connectionData = request.Payload;
    
        if(netManager.ConnectedClientsIds.Count < MaxNumPlayers)
        {
            response.Approved = true;
            response.CreatePlayerObject = true;
        }
        else
        {
            // TODO: Mostrar a tela de "sala cheia" e "incapaz de conectar"
            response.Approved = false;
        }
    
        // The prefab hash value of the NetworkPrefab, if null the default NetworkManager player prefab is used
        response.PlayerPrefabHash = null;
    
        // If additional approval steps are needed, set this to true until the additional steps are complete
        // once it transitions from true to false the connection approval response will be processed.
        response.Pending = false;
    }
    */
}