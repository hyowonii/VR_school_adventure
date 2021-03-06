// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to connect, and join/create room automatically
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------
//?�정�?

using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using System;
using Photon.Pun;
using TMPro;


#pragma warning disable 649

/// <summary>
/// Launch manager. Connect, join a random room or create one if none or all full.
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{

    #region Private Serializable Fields

    public InputField roomName;
    //public InputField playerName;
    public TextMeshProUGUI playerName;

    public GameObject player;
    public TextMeshPro nametag;
    private GameObject oldplayer;
    private GameObject newplayer;
    private int randomPrefab = -1;

    public GameObject controlPanel;
    public GameObject[] playerPrefabs;
    public GameObject VRCamera;
    public GameObject recordUI;

    //private Vector3 lobbyVector = new Vector3(20, 8, -36);  // 3층 입구
    private Vector3 lobbyVector = new Vector3(17, 0, -11);    // 1층 입구


    #endregion

    #region Private Fields
    /// <summary>
    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    bool isConnecting;

    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";

    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {

        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
        roomName.text = "Lobby";
        Connect();

    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Start the connection process. 
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        if (PhotonNetwork.CurrentRoom != null)                     // ?�재 room??참여?�고 ?�는 ?�태?�면 leave
        {     
            LeaveRoom();
        }
            
        isConnecting = true;

        // UI_record ?�시
        if (recordUI)
        {
            recordUI.SetActive(true);
        }
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("isConnected");
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.

            Debug.Log(roomName.text);

            PhotonNetwork.JoinRoom(roomName.text);


        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            Debug.Log("not connected");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }

    #endregion


    #region MonoBehaviourPunCallbacks CallBacks
    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class MonoBehaviourPunCallbacks


    /// <summary>
    /// Called after the connection to the master is established and authenticated
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room. 
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.NickName = playerName.text;

            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRoom(roomName.text);
        }
    }

    /// <summary>
    /// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
    /// </summary>
    /// <remarks>
    /// Most likely all rooms are full or no rooms are available. <br/>
    /// </remarks>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("create new room");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.

        PhotonNetwork.CreateRoom(roomName.text);
 
    }


    /// <summary>
    /// Called after disconnecting from the Photon server.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected");

        isConnecting = false;

    }

    /// <summary>
    /// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
    /// </summary>
    /// <remarks>
    /// This method is commonly used to instantiate player characters.
    /// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
    ///
    /// When this is called, you can usually already access the existing players in the room via PhotonNetwork.PlayerList.
    /// Also, all custom properties should be already available as Room.customProperties. Check Room..PlayerCount to find out if
    /// enough players are in the room to start playing.
    /// </remarks>
    public override void OnJoinedRoom()
    {
        CreatePlayer();
        Debug.Log(PhotonNetwork.NickName);

        roomName.tag = "Untagged";
        roomName.text = "Lobby";

        PhotonNetwork.NickName = playerName.text;
        player.transform.GetChild(1).GetChild(2).GetComponent<TextMeshPro>().text = "";

        Debug.Log(PhotonNetwork.CurrentRoom);

        Time.timeScale = 1;

    }

    private void CreatePlayer()
    {
        oldplayer = player.transform.GetChild(1).gameObject;

        if (roomName.tag == "Untagged") // 로비 ?�장
        {
            player.transform.position = lobbyVector;
            
            //player.transform.position = new Vector3(17, 0, -11);
        }

        if (roomName.tag == "0")       //  강의???�장
        {
            //player.transform.position = new Vector3(43, 0.8f, -27);  // 101
            player.transform.position = new Vector3(45 , 0, -12);  // police
        }

        else if (roomName.tag == "1")  // office ?�장
        {
            player.transform.position = new Vector3(-2, 7.5f, -23);
        }

        else if (roomName.tag == "2")   // conference room ?�장
        {
            player.transform.position = new Vector3(29, 8, -29);  // conference room

        }

        if (randomPrefab == -1)
        {
            randomPrefab = UnityEngine.Random.Range(0, playerPrefabs.Length);
        }
        newplayer = PhotonNetwork.Instantiate(playerPrefabs[randomPrefab].name, player.transform.position, Quaternion.identity, 0);
        newplayer.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        newplayer.transform.SetParent(player.transform);
        newplayer.transform.localEulerAngles = new Vector3(0, 0, 0);
        newplayer.layer = 3;

        foreach (Transform child in newplayer.transform.GetChild(0).transform)
        {
            child.gameObject.layer = 3;
        }

        VRCamera.transform.localPosition = new Vector3(0, 1, 0);
        player.transform.eulerAngles = new Vector3(0, 0, 0);

        newplayer.name = playerName.text;
        
        Debug.Log("nickname : " + PhotonNetwork.NickName);

        player.GetComponent<Rigidbody>().isKinematic = false;

        Destroy(oldplayer);
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.CurrentRoom.Name == "Lobby"){
            lobbyVector = player.transform.position;
        }

        GameObject me = GameObject.CreatePrimitive(PrimitiveType.Cube);
        me.transform.localScale = new Vector3(0.2f, 0.5f, 0.2f);
        me.transform.SetParent(player.transform);

        PhotonNetwork.LeaveRoom();

    }
#endregion
}