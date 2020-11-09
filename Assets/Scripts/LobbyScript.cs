using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    private byte MAX_PLAYERS = 10;

    [SerializeField]
    private InputField createRoomName;
    [SerializeField]
    private InputField joinRoomName;
    [SerializeField]
    private GameObject roomList;
    [SerializeField]
    private GameObject roomListingPrefab;

    private void Start()
    {
        if (roomList.transform.childCount > 0)
        {
            Destroy(roomList.transform.GetChild(0).gameObject);
        }
    }

    // join random room
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // create and join a room
    public void CreateRoom()
    {
        string roomName = createRoomName.text;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    // join room by name
    public void JoinRoom()
    {
        string roomName = joinRoomName.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    // add a room to list
    private void AddRoomListing(string roomName)
    {
        roomListingPrefab = Instantiate(roomListingPrefab, roomList.transform);
        roomListingPrefab.GetComponent<RoomListing>().SetName(roomName);
    }

    // remove a room from list
    private void RemoveRoomListing(string roomName)
    {
        foreach(Transform child in roomList.transform)
        {
            RoomListing rl = child.GetComponent<RoomListing>();
            if (rl.GetName() == roomName)
            {
                Destroy(child.gameObject);
            }
        }
    }

    // callback for room list update
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        CanvasScript.instance.SetStatus("Room List Update!");
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                RemoveRoomListing(room.Name);
            }
            else
            {
                AddRoomListing(room.Name);
            }
        }
    }

    // callback if failed to join random room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CanvasScript.instance.SetStatus("No random room available, so we create one.");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MAX_PLAYERS });
        
    }

    // callback if successfully joined a room
    public override void OnJoinedRoom()
    {
        CanvasScript.instance.SetStatus("Joined a room!");
        CanvasScript.instance.SetMenuStage(CanvasScript.MenuStage.Room);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    // callback if failed to join a room by name
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        CanvasScript.instance.SetStatus(message);
    }
}
