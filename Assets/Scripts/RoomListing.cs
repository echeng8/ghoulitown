using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text nameText;

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public string GetName()
    {
        return nameText.text;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(nameText.text);
    }
}
