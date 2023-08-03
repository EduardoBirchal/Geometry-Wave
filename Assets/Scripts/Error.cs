using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Error : MonoBehaviour
{
    public enum PopupState
    {
        Error,
        Timeout,
        Waiting,
        Sucess,
        Idle
    }
    [SerializeField] private GameObject objMessage;
    [SerializeField] private GameObject objButton;
    [SerializeField] private GameObject objFlag;
    public string text = null;
    private bool lockText;
    public PopupState state = PopupState.Idle;

    public void Start()
    {
        lockText = true;
        objFlag.SetActive(false);
        objButton.SetActive(false);
    }
    public void Update()
    {
        switch(state)
        {
            case PopupState.Idle:
                this.gameObject.SetActive(false);
                break;
            case PopupState.Sucess:
                text = NetworkStart.GetLocalIPv4();
                this.gameObject.SetActive(true);
                break;
            case PopupState.Error:
                text = GameObject.Find("NetworkManager")
                        .GetComponent<NetworkManager>()
                        .DisconnectReason;
                this.gameObject.SetActive(true);
                objFlag.SetActive(true);
                objButton.SetActive(true);
                break;
            case PopupState.Timeout:
                text = "Tempo esgotado";
                this.gameObject.SetActive(true);
                objFlag.SetActive(true);
                objButton.SetActive(true);
                break;
            case PopupState.Waiting:
                text = "Conectando...";
                this.gameObject.SetActive(true);
                break;
        }

        TMP_Text message = objMessage.GetComponent<TMP_Text>();
        message.text = text;
    }
}
