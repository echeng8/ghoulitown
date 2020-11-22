using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Presents imposter action buttons when a player is an imposter
/// connects imposter action buttons to the imposter 
/// </summary>
public class ImposterUI : MonoBehaviour
{
    public Button attackButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.LocalPlayerInstance)
            ConnectToPlayer(PlayerController.LocalPlayerInstance.gameObject);
        else 
            PlayerController.OnLocalPlayerInstanceSet.AddListener(ConnectToPlayer);
    }

    void ConnectToPlayer(GameObject player)
    {
        ImposterController imposterController = player.GetComponent<ImposterController>();
        imposterController.OnImposterStatusChange.AddListener(ToggleImposterUI);
        
        attackButton.onClick.AddListener(imposterController.AttackNearbyPlayer);
        gameObject.SetActive(false);
    }

    void ToggleImposterUI(bool isImposter)
    {
        gameObject.SetActive(isImposter);
    }
}
