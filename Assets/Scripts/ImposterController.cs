using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ImposterController : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// range of KillNearbyPlayer()
    /// </summary>
    [SerializeField] private float killRange; 
    public BoolEvent OnImposterStatusChange;
    
    
    public bool Imposter
    {
        get => _imposter;
        set
        {
            if (value != _imposter)
            {
                OnImposterStatusChange.Invoke(value);
            }
            
            _imposter = value;
        }
    }

    private bool _imposter = false;
    
    
    private void Awake()
    {
        if (OnImposterStatusChange == null)
            OnImposterStatusChange = new BoolEvent();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("imposterActorNumber"))
        {
            Imposter = (int) propertiesThatChanged["imposterActorNumber"] ==
                                     PhotonNetwork.LocalPlayer.ActorNumber;
        }
    }

    public void AttackNearbyPlayer()
    {
        if (!Imposter || !photonView.IsMine)
            return; 
        
        Collider[] hitColliders = new Collider[16];
        
        var numColliders = Physics.OverlapSphereNonAlloc(transform.position, killRange, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                print("I tried to kill someone.");
                hitColliders[i].gameObject.GetComponent<PlayerController>().RPCAttacked(PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }


    }




}