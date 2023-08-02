using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Error : MonoBehaviour
{
    [SerializeField] private GameObject objMessage;
    public static bool onScreen;
    public static string customMessage = null;
    
    private void Awake()
    {
        onScreen = true;
        
        TMP_Text message = objMessage.GetComponent<TMP_Text>();
        if(customMessage != null)
        {
            NetworkManager netManager = GameObject.Find("NetworkManager")
                .GetComponent<NetworkManager>();
            message.text = netManager.DisconnectReason;
        }
        else 
        {
            message.text = customMessage;
            customMessage = null;
        }
    }
}
