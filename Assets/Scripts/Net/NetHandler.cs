using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System.Collections;

public class NetHandler : NetworkBehaviour
{
    public void OnClientConnectedCallback(ulong obj)
    {
        status = ConnectionResponse.Connected;
    }
    public void OnClientConnect()
    { numPlayers++; }
    public void OnClientDisconnect()
    { numPlayers--; }
    public void OnClientDisconnectCallback(ulong obj)
    {
        if (!IsServer && DisconnectReason != string.Empty)
        {
            errorScreen.GetComponent<Error>().state = Error.PopupState.Error;
            errorScreen.SetActive(true);
        }
    }
    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
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
}