using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using System.Net;
using System.Linq;
using System.Collections;

public class NetHandler : NetworkBehaviour
{
    [SerializeField] private GameObject screen_Error;
    private NetworkManager NetManager;
    private NetStatus net_Status;

    private void Start()
    {
        NetManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        net_Status = GameObject.Find("Network").GetComponent<NetStatus>();
    }
    public void ShutdownServer()
    {
        if(IsHost)
        {
            for (int i = NetworkManager.ConnectedClientsIds.Count - 1; i >= 0; i--)
            {
                var id = NetworkManager.ConnectedClientsIds[i];
                if (id != NetworkManager.LocalClientId)
                    NetworkManager.DisconnectClient(id, "Jogo encerrado pelo host");
            }
        }
        NetworkManager.Singleton.Shutdown();
        if(NetworkManager.Singleton != null) Destroy(NetworkManager.Singleton.gameObject);
    }

    public void OnClientConnectedCallback(ulong obj)
    {
        NetStatus.status = ConnectionResponse.Connected;
        if(!IsServer) return;

        if(NetManager.ConnectedClientsIds.Count > NetStatus.MaxNumPlayers)
            Debug.LogError("Numero de players exedido. Revise seu código!");
        net_Status.PlayersAlive.Value++;
    }
    
    public void OnClientDisconnectCallback(ulong obj)
    {
        NetStatus.status = ConnectionResponse.Offline;
        if (!IsServer && NetManager.DisconnectReason != string.Empty)
        {
            Error script_Error = screen_Error.GetComponent<Error>();
            script_Error.state = Error.PopupState.Error;
            script_Error.text = NetManager.DisconnectReason;
            script_Error.UpdateState();
        }
        if(!IsServer) return;

        if(NetManager.ConnectedClientsIds.Count < 0)
            Debug.LogError("Numero negativo de players. Revise seu código!");
        net_Status.PlayersAlive.Value--;
        if(net_Status.PlayersAlive.Value < 0)
            Debug.LogWarning("Todos os players estão mortos!");
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        var clientId = request.ClientNetworkId;
        var connectionData = request.Payload;
        response.PlayerPrefabHash = null;
    
        if(NetManager.ConnectedClientsIds.Count >= NetStatus.MaxNumPlayers)
        {
            response.Reason = "A sala está cheia";
            response.Approved = false;
        }
        else if(NetStatus.gameStarted == true)
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
}