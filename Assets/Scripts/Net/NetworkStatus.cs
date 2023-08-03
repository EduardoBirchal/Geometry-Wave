using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkStatus : NetworkBehaviour 
{
    private const int WAIT_TIME = 1000;
    public enum ConnectionResponse
    {
        Waiting,
        Connected,
        Offline
    }
    private NetworkManager networkManager;
    private Error errorScript;
    [SerializeField] private GameObject errorScreen;
    private static ConnectionResponse status;

    private void Start() 
    {
        errorScript = errorScreen.GetComponent<Error>();
        if(IsHost) status = ConnectionResponse.Connected;
        else status = ConnectionResponse.Waiting;
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    public void OnClientConnectedCallback(ulong obj)
    {
        status = ConnectionResponse.Connected;
        Debug.LogError("Conectado");
    }

    public async void InitialConnection()
    {
        const int TRIES = 5;
        errorScript.state = Error.PopupState.Waiting;
        errorScript.Update();
        
        for(int i = 0; i < TRIES; i++)
        {
            Task WaitForConnection = Count();
            await WaitForConnection;
            if(status == ConnectionResponse.Connected)
            {
                errorScript.state = Error.PopupState.Sucess;
                return;
            }
        }
        status = ConnectionResponse.Offline;
        
        // TODO: Não precisar esperar caso a conexão seja bem-sucedida
        // TODO: Não mostrar o Timeout em cima de outros erros
        errorScript.state = Error.PopupState.Timeout;
        return;
    }
    
    private async Task Count()
    {
        await Task.Delay(WAIT_TIME);
    }
}
