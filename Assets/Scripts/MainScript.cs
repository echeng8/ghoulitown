using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainScript : MonoBehaviour
{
    public void SetNickname(string name)
    {
        PhotonNetwork.NickName = name;
    }
}
