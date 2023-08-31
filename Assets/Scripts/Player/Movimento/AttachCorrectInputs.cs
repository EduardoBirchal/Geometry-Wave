using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using Unity.Netcode;

public class AttachCorrectInputs : NetworkBehaviour
{
    // Get "Player Input" component which sould be attached to player prefab
    private PlayerInput playerInput;
    // Assign correct InputActionAsset in the player's prefab inspector
    [SerializeField] private InputActionAsset inputActionAsset;
    
    private void Start()
    {
        // Get "Player Input" component when Player is initialized
        playerInput = gameObject.GetComponent<PlayerInput>();
    }
 
   // On spawn
   public override void OnNetworkSpawn()
   {
        base.OnNetworkSpawn();
        // Make sure this belongs to us
        if (!IsOwner) { return; }
        playerInput.enabled = true;
   }
}
