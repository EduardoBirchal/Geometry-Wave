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
    [SerializeField] private GameObject errorScreen;
    private NetworkManager netManager;
    public static bool isSingleplayer = true;
    public static bool gameStarted = false;
    public int MaxNumPlayers;

    private void Start()
    {
        MaxNumPlayers = isSingleplayer ? 1 : 1;
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;

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
            startBtn.SetActive(false);
        }
        startBtn.GetComponent<Button>().onClick.AddListener(() => {
            gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            startBtn.SetActive(false);
        });
        // TODO: Corrigir a sincronização dos inimigos
    }

    // TODO: Desconectar os players quando o host fecha o jogoy
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
            response.Reason = "A sala está cheia";
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

    // 
    private void OnClientDisconnectCallback(ulong obj)
    {
        if (!netManager.IsServer && netManager.DisconnectReason != string.Empty)
        {
            errorScreen.SetActive(true);
        }
    }
}