using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class DeathManager : NetworkBehaviour
{
    [SerializeField] private GameObject text_Obj;
    [SerializeField] private GameObject death_Screen;
    [SerializeField] private GameObject player_Prefab;
    private TimeManager time_Script;
    private NetStatus net_Status;

    private void Start() 
    {
        time_Script = GameObject.Find("GameManager").GetComponent<TimeManager>();
        net_Status = GameObject.Find("Network").GetComponent<NetStatus>();
        text_Obj.GetComponent<TextMeshProUGUI>().text = "";
    }

    public async Task KillPlayer()
    {
        text_Obj.GetComponent<FuncoesTexto>().MostraFade(2, 2, "Você morreu");
        await Task.Delay(5 * 1000);
        
        // TODO: Quando implementar o fim de jogo no MP, adicionar a condição aqui
        if(NetStatus.isSingleplayer == true || net_Status.PlayersAlive.Value == 0)
            time_Script.Pause();
        else if (net_Status.PlayersAlive.Value > 0)
        {
            text_Obj.GetComponent<FuncoesTexto>().SetText("Esperando o fim da onda");
            bool passedWave = await WaitForWave();
            if(passedWave == true)
            {
                text_Obj.GetComponent<FuncoesTexto>().MostraFade(0.1f, 0.1f, "");
                RespawnServerRpc(NetworkManager.Singleton.LocalClientId);
                return;
            }
        }
        
        TimeManager.localDead = true;
        death_Screen.SetActive(true);
        return;
    }

    [ServerRpc(RequireOwnership = false)]
    void RespawnServerRpc(ulong clientId)
    {
        Instantiate(player_Prefab).GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        net_Status.PlayersAlive.Value++;
    }

    private async Task<bool> WaitForWave()
    {
        GameObject[] inimigos;
        do
        {
            inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            await Task.Delay(1 * 1000);
            if(net_Status.PlayersAlive.Value < 1)
                return false;
        }
        while(inimigos.Length > 0);

        return true;
    }
}
