using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System.Collections;

public class NetworkStart : MonoBehaviour
{
    private enum ConnectionResponse
    {
        Waiting,
        Connected,
        Offline

    }
    [SerializeField] private GameObject hostBtn;
    [SerializeField] private GameObject clientBtn;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject errorScreen;
    private NetworkManager netManager;
    private ConnectionResponse status;
    public static bool isSingleplayer = true;
    public static bool gameStarted = false;
    public int MaxNumPlayers;
    
    private void Start()
    {
        MaxNumPlayers = isSingleplayer ? 1 : 4;
        status = ConnectionResponse.Waiting;
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;

        GameObject.Find("NetworkManager").GetComponent<UnityTransport>().ConnectionData.Address = MenuManager.texto_ip;
        startBtn.SetActive(false);

        if(isSingleplayer == true)
        {
            NetworkManager.Singleton.StartHost();
            
            hostBtn.SetActive(false);
            clientBtn.SetActive(false);
            
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            gameStarted = true;
            status = ConnectionResponse.Connected;
        }
        else if(MenuManager.texto_ip == GetLocalIPv4())
        {
            Debug.Log("IP para conectar: " + GetLocalIPv4());
            NetworkManager.Singleton.StartHost();
            status = ConnectionResponse.Connected;
            startBtn.SetActive(true);
        }
        else
        {
            NetworkManager.Singleton.StartClient();

            // StartCoroutine(Count());
            do
            {
                
                if(status == ConnectionResponse.Connected)
                {
                    return;
                }

            } while (status == ConnectionResponse.Waiting);
            
            // errorScreen.SetActive(true);
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
        response.PlayerPrefabHash = null;
    
        if(netManager.ConnectedClientsIds.Count >= MaxNumPlayers)
        {
            response.Reason = "A sala está cheia";
            response.Approved = false;
        }
        else if(gameStarted == true)
        {
            response.Reason = "Jogo em andamento";
            response.Approved = false;
        }
        else
        {
            response.Approved = true;
            response.CreatePlayerObject = true;
        }
    
        // The prefab hash value of the NetworkPrefab, if null the default NetworkManager player prefab is used
    
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

    private void Count(float tempo)
    {

    }

    private IEnumerator Count()
    {
        yield return new WaitForSeconds(5.0f);
        status = ConnectionResponse.Offline;
    }

    [ServerRpc(RequireOwnership = false)]
    void TestConnectionServerRpc(ulong clientId)
    {
        if (!IsServer) return;
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[]{clientId}
            }
        };
        TestConnectionClientRpc();
    }

    [ClientRpc]
    void TestConnectionClientRpc(ClientRpcParams clientRpcParams = default)
    {
        if (IsOwner) return;
        Debug.Debug.LogWarning("Conectado!");
        status = ConnectionResponse.Connected;
    }

}