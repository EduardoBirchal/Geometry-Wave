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

    private void Start()
    {
        localPause = false;
    }
    public void Resume()
    {
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
