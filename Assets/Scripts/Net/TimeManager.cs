using UnityEngine;
using Unity.Netcode;
// TODO: Forçar o pause no player
public class TimeManager : NetworkBehaviour 
{
    public static bool paused = false;
    public void Resume()
    {
        if(IsHost == false) return;
        paused = false;
        PausarJogoServerRpc(1);
    }
    public void Pause()
    {
        if(IsHost == false) return;
        paused = true;
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
