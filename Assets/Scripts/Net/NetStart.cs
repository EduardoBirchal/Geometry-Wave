using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System.Collections;

public class NetStart : NetworkBehaviour
{
    private void Start()
    {
        netStatus = GameObject.Find("NetworkStatus").GetComponent<NetStatus>();
        connectHandler = GameObject.Find("ConnectionHandler").GetComponent<NetHandler>();
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        
        NetworkManager.Singleton.ConnectionApprovalCallback = connectHandler.ApprovalCheck;
        NetworkManager.Singleton.OnClientDisconnectCallback += connectHandler.OnClientDisconnectCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += connectHandler.OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += connectHandler.OnClientConnect;
        NetworkManager.Singleton.OnClientConnectedCallback += connectHandler.OnClientDisconnect;

        GameObject.Find("NetworkManager").GetComponent<UnityTransport>().ConnectionData.Address = MenuManager.texto_ip;
        startBtn.SetActive(false);

        if(isSingleplayer == true)
        {
            NetworkManager.Singleton.StartHost();
            gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
        }
        else if(MenuManager.texto_ip == GetLocalIPv4())
        {
            Debug.LogWarning("IP para conectar: " + GetLocalIPv4());
            NetworkManager.Singleton.StartHost();
            startBtn.SetActive(true);
        }
        else
        {
            startBtn.SetActive(false);
            NetworkManager.Singleton.StartClient();
            netStatus.InitialConnection();
        }

        startBtn.GetComponent<Button>().onClick.AddListener(() => {
            gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            startBtn.SetActive(false);
        });
    }
}
