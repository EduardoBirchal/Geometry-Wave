using System.Collections;
using System.Threading.Tasks;
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
    private static ConnectionResponse status;
    [SerializeField] private GameObject errorScreen;
    private Error errorScript;

    private void Start() 
    {
        errorScript = errorScreen.GetComponent<Error>();
        if(IsHost) status = ConnectionResponse.Connected;
        else status = ConnectionResponse.Waiting;
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    public static bool HasTimedOut()
    {
        return status == ConnectionResponse.Offline;
    }

    public void OnClientConnectedCallback(ulong obj)
    {
        status = ConnectionResponse.Connected;
        Debug.LogError("Conectado");
    }

    public async void InitialConnection()
    {
        errorScript.state = Error.PopupState.Waiting;
        errorScript.Update();
        Task WaitForConnection = Count();
        
        // TODO: Não precisar esperar caso a conexão seja bem-sucedida
        await WaitForConnection;
        Debug.LogWarning(status);
        if(status == ConnectionResponse.Offline)
        {
            errorScript.state = Error.PopupState.Error;
        }
        else
        {
            errorScript.state = Error.PopupState.Sucess;
        }
        return;
    }
    
    private async Task Count()
    {
        await Task.Delay(5 * 1000);
        if(status != ConnectionResponse.Connected)
            status = ConnectionResponse.Offline;
    }
}
