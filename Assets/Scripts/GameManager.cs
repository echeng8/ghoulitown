using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    [SerializeField]
    private GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
    }

    public override void OnJoinedRoom()
    {
        if (PlayerController.instance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}
