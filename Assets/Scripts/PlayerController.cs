using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;



public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerController localPlayerInstance;
    public static GameObjectEvent OnLocalPlayerInstanceSet;

    
    [SerializeField]
    private GameObject nameTag;



    bool _isFiring;


    private void Awake()
    {
        //initializes events for subscribers
        if (OnLocalPlayerInstanceSet == null)
            OnLocalPlayerInstanceSet = new GameObjectEvent();
        
        //this is the dummy player to preload static fields, so disable
        if (photonView.Owner == null) 
        {
            gameObject.SetActive(false);
            return;
        }

        if (photonView.IsMine)
        {
            SetLocalInstancePlayer(this);
        }
    }

    private void Start()
    {
        //todo bring this to nametag components for single responsibility
        nameTag.GetComponent<TextMesh>().text = photonView.Owner.NickName;

    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // push data to other clients
        if (stream.IsWriting)
        {
            stream.SendNext(_isFiring);
        }
        // interpret data
        else
        {
            this._isFiring = (bool)stream.ReceiveNext();
            Debug.Log(_isFiring);
        }
    }

    void SetLocalInstancePlayer(PlayerController pc)
    {
        localPlayerInstance = this;
        OnLocalPlayerInstanceSet.Invoke(localPlayerInstance.gameObject);
    }
}
