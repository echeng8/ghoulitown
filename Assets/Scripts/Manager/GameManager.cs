using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Manager
{
    /// <summary>
    /// game logic manager (for loading, transfering, exiting, etc see application manager)
    /// </summary>
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static GameManager instance;

        [SerializeField] private GameObject playerPrefab;

        #region Unity Callbacks

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if(PhotonNetwork.IsConnected)
                PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero + Vector3.up * 4, Quaternion.identity);
        }

        #endregion
        
        #region PUN Callbacks

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            return;
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// run on master, sets the imposter id on the room custom properties
        /// </summary>
        public void selectImposter()
        {
            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                return;
            
            int imposterActorNumber = getRandomPlayer().ActorNumber;
            ExitGames.Client.Photon.Hashtable newRoomProp = new ExitGames.Client.Photon.Hashtable
            {
                {"imposterActorNumber", imposterActorNumber}
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(newRoomProp);
        }

        #endregion
        
        #region Private Helper Functions
        
        Player getRandomPlayer()
        {
            Player[] playerList = PhotonNetwork.PlayerList; //docs recommend caching this value due to high computation overhead
            return playerList[Random.Range(0, playerList.Length)];
        } 

        #endregion
        
        #region Debug
        
        #endregion
    }
}
