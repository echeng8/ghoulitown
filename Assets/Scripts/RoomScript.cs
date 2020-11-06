using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomScript : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    private void OnEnable()
    {
        nameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void LeaveRoom()
    {
        CanvasScript.instance.SetMenuStage(CanvasScript.MenuStage.Lobby);
        PhotonNetwork.LeaveRoom();
    }
}
