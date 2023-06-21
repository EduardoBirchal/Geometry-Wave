using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class TimeManager : NetworkBehaviour 
{
    public void Resume()
    {
        if(PlayerNetwork.isHost == false) return;
        PausarJogoServerRpc(1);
    }
    public void Pause()
    {
        if(PlayerNetwork.isHost == false) return;
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
