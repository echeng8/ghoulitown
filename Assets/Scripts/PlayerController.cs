using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerController instance;

    [SerializeField]
    private Text nametag;
    [SerializeField]
    private SpriteRenderer sr;

    bool isFiring;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            instance = this;
        }
    }

    private void Start()
    {
        nametag.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C))
        {
            int r = Random.Range(0, 255);
            int g = Random.Range(0, 255);
            int b = Random.Range(0, 255);
            photonView.RPC("ChangeColor", RpcTarget.All, r, g, b);
        }
    }

    // Example of RPC
    [PunRPC]
    public void ChangeColor(int r, int g, int b)
    {
        sr.color = new Color(r / 255.0f, g / 255.0f, b / 255.0f, 1);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // push data to other clients
        if (stream.IsWriting)
        {
            stream.SendNext(isFiring);
        }
        // interpret data
        else
        {
            this.isFiring = (bool)stream.ReceiveNext();
            Debug.Log(isFiring);
        }
    }
}
