using UnityEngine;
using Unity.Netcode;

public class TimeManager : NetworkBehaviour 
{
    public static NetworkVariable<bool> globalPause = new(
        value:false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );
    public static bool localPause;
    public static bool localDead;

    private void Start()
    {
        localDead = false;
        localPause = false;
    }

    public void Resume()
    {
        if(localDead == true) return;
        localPause = false;
        
        if(IsHost == false) return;
        globalPause.Value = false;
    }
    public void Pause()
    {
        localPause = true;
        
        if(IsHost == false) return;
        globalPause.Value = true;
    }
}
