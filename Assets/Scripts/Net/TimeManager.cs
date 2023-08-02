using UnityEngine;
using Unity.Netcode;
// TODO: Forçar o pause no player
public class TimeManager : NetworkBehaviour 
{
    public static bool paused = false;
    public void Resume()
    {
        if(PlayerNetwork.isHost == false) return;
        paused = false;
        PausarJogoServerRpc(1);
    }
    public void Pause()
    {
        if(PlayerNetwork.isHost == false) return;
        paused = true;
        PausarJogoServerRpc(0);
    }
    
    [ClientRpc]
    private void AlterarTempoClientRpc(int tempo)
    {
        Time.timeScale = tempo;
    }
    [ServerRpc]
    private void PausarJogoServerRpc(int tempo)
    {
        AlterarTempoClientRpc(tempo);
    }
}
