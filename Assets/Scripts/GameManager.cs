using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    [SerializeField]
    private GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero + Vector3.up * 4, Quaternion.identity);
    }
}
