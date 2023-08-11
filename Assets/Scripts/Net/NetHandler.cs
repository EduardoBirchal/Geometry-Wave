using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using System.Net;
using System.Linq;
using System.Collections;

public class NetHandler : NetworkBehaviour
{
    [SerializeField] private NetworkManager NetManager;
    [SerializeField] private GameObject screen_Error;

    private void Start()
    {
        NetManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    public void OnClientConnectedCallback(ulong obj)
    {
        NetStatus.status = ConnectionResponse.Connected;
        if(!IsHost) return;

        if(NetManager.ConnectedClientsIds.Count > NetStatus.MaxNumPlayers)
            Debug.LogError("Numero de players exedido. Revise seu código!");
        NetStatus.PlayersAlive++;
    }
    
    public void OnClientDisconnectCallback(ulong obj)
    {
        NetStatus.status = ConnectionResponse.Offline;
        if (IsClient && NetManager.DisconnectReason != string.Empty)
        {
            screen_Error.GetComponent<Error>().state = Error.PopupState.Error;
            screen_Error.SetActive(true);
        }
        if(!IsHost) return;

        if(NetManager.ConnectedClientsIds.Count < 0)
            Debug.LogError("Numero negativo de players. Revise seu código!");
        NetStatus.PlayersAlive--;
        if(NetStatus.PlayersAlive < 0)
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