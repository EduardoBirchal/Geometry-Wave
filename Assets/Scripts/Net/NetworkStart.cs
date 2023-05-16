using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkStart : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    
    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Awake()
    {
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            Time.timeScale = 1;
        });
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            Time.timeScale = 1;
        });
    }
}