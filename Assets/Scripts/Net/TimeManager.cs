using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using System.Threading.Tasks;

public class TimeManager : NetworkBehaviour 
{
    public NetworkVariable<bool> globalPause = new(
        value:false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );
    public static bool localPause;
    public static bool localDead;
    public TextMeshProUGUI text_Pause;

    private void Start()
    {
        localDead = false;
        localPause = false;
        text_Pause = GameObject.Find("PauseText").GetComponent<TextMeshProUGUI>();
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

    private void Update()
    {
        if(IsHost) return;
        text_Pause.text = globalPause.Value == true ? "Jogo pausado" : "";
    }
}
