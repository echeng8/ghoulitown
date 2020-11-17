using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;


//todo state machine
public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerController LocalPlayerInstance;
    public static GameObjectEvent OnLocalPlayerInstanceSet;

    
    [SerializeField]
    private GameObject nameTag;

    
    #region Monobehavior Callback

    private void Awake()
    {
        //initializes events for subscribers
        if (OnLocalPlayerInstanceSet == null)
            OnLocalPlayerInstanceSet = new GameObjectEvent();
        


        //checks PNconnected to allow offline testing
        if (!PhotonNetwork.IsConnected || photonView.IsMine)
        {
            SetLocalInstancePlayer(this);
        } else if (photonView.Owner == null)
        {
            //this is the dummy player to preload static fields, so disable
            if (photonView.Owner == null) 
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        //todo move logic to nametag scripts
        if(PhotonNetwork.IsConnected)
            nameTag.GetComponent<TextMesh>().text = photonView.Owner.NickName;
    }
    
    #endregion

    #region PhotonCallbacks

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        return;
    }

    #endregion

    #region Public Methods
    
    
    #endregion

    #region  Pun RPC and Public RPC-Calling Methods
    
    /// <summary>
    /// handles being attacked. this is called from the attackers client locally.
    /// </summary>
    /// <param name="attackerActorNumber">you know what it is</param>
    public void RPCAttacked(int attackerActorNumber)
    {
        photonView.RPC("GetAttacked", RpcTarget.All, attackerActorNumber);
    }
    
    [PunRPC]
    void GetAttacked(int attackerActorNumber)
    {
        print($"{photonView.Owner.ActorNumber} was attacked by {attackerActorNumber}");
    }
    
    #endregion
    
    #region Private Methods

    void SetLocalInstancePlayer(PlayerController pc)
    {
        LocalPlayerInstance = this;
        OnLocalPlayerInstanceSet.Invoke(LocalPlayerInstance.gameObject);
    }

    #endregion

}
