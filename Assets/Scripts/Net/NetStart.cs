using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Threading.Tasks;

public class NetStart : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject btn_Start;
    [SerializeField] private GameObject screen_Error;
    private NetStatus netStatus;
    private NetworkManager netManager;
    private NetHandler connectHandler;


    private void Start()
    {
        netStatus = GameObject.Find("Network").GetComponent<NetStatus>();
        connectHandler = GameObject.Find("Network").GetComponent<NetHandler>();
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        
        NetworkManager.Singleton.ConnectionApprovalCallback = connectHandler.ApprovalCheck;
        NetworkManager.Singleton.OnClientDisconnectCallback += connectHandler.OnClientDisconnectCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += connectHandler.OnClientConnectedCallback;

        netManager.GetComponent<UnityTransport>().ConnectionData.Address = MenuManager.texto_ip;
        string ip = NetHandler.GetLocalIPv4();

        if(NetStatus.isSingleplayer == true)
        {
            NetworkManager.Singleton.StartHost();
            NetStatus.gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
        }
        else if(MenuManager.texto_ip == ip)
        {
            GameObject.Find("DeathText").GetComponent<FuncoesTexto>().SetText("IP da sala:             " + ip);
            NetworkManager.Singleton.StartHost();
            btn_Start.SetActive(true);
        }
        else
        {
            NetworkManager.Singleton.StartClient();
            InitialConnection();
        }

        btn_Start.GetComponent<Button>().onClick.AddListener(() => {
            GameObject.Find("DeathText").GetComponent<FuncoesTexto>().SetText("");
            NetStatus.gameStarted = true;
            Instantiate(spawner).GetComponent<NetworkObject>().Spawn();
            btn_Start.SetActive(false);
        });
    }

    public async void InitialConnection()
    {
        const int TRIES = 5;
        const int WAIT_TIME = 1 * 1000;
        Error script_Error = screen_Error.GetComponent<Error>();
        script_Error.state = Error.PopupState.Waiting;
        script_Error.UpdateState();
        
        for(int i = 0; i < TRIES; i++)
        {
            await Task.Delay(WAIT_TIME);
            if(NetStatus.status == ConnectionResponse.Connected)
            {
                script_Error.state = Error.PopupState.Sucess;
                script_Error.UpdateState();
                return;
            }
        }
        
        if(script_Error.state != Error.PopupState.Error)
        {
            script_Error.state = Error.PopupState.Timeout;
            script_Error.text = "Tempo esgotado";
        }
        script_Error.UpdateState();
        return;
    }
}
