using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkStatus : NetworkBehaviour 
{
    private enum ConnectionResponse
    {
        Waiting,
        Connected,
        Offline

    }
    private NetworkManager networkManager;
    private ConnectionResponse status;

    private void Start() 
    {
        if(IsHost) status = ConnectionResponse.Connected;
        else status = ConnectionResponse.Offline;
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    public bool PingarServer()
    {
        status = ConnectionResponse.Waiting;
        Debug.Log("Pingando...");
        StartCoroutine(Count());
        do
        {
            PingServerRpc(NetworkManager.Singleton.LocalClientId);
            if(status == ConnectionResponse.Connected)
            {
                Debug.Log("E foi");
                return true;
            }
        } while (status == ConnectionResponse.Waiting);
        Debug.Log("Falso");
        return false;
    }
    private IEnumerator Count()
    {
        yield return new WaitForSeconds(5.0f);
        status = ConnectionResponse.Offline;
    }

    [ServerRpc(RequireOwnership = false)]
    void PingServerRpc(ulong clientId)
    {
        if(!IsServer) return;
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[]{clientId}
            }
        };
        PingClientRpc();
    }

    [ClientRpc]
    void PingClientRpc(ClientRpcParams clientRpcParams = default)
    {
        if(IsOwner) return;
        Debug.LogWarning("Conectado!");
        status = ConnectionResponse.Connected;
    }
}
