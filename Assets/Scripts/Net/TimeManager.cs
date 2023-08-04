using UnityEngine;
using Unity.Netcode;
// TODO: Forçar o pause no player
public class TimeManager : NetworkBehaviour 
{
    public static bool paused = false;
    public void Resume()
    {
        paused = false;
        if(IsHost == false) return;
        PausarJogoServerRpc(1);
    }
    public void Pause()
    {
        paused = true;
        if(IsHost == false) return;
        PausarJogoServerRpc(0);
    }
    
    [ClientRpc]
    private void AlterarTempoClientRpc(int tempo)
    {
        paused = tempo == 0;
        Time.timeScale = tempo;
    }
    [ServerRpc]
    private void PausarJogoServerRpc(int tempo)
    {
        AlterarTempoClientRpc(tempo);
    }
}
