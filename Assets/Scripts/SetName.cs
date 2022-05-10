using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using System;
using Photon.Pun;
using TMPro;

public class SetName : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        setName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom(){
        this.GetComponent<PhotonView>().RPC("setName", RpcTarget.AllBuffered);
        Debug.Log(PhotonNetwork.NickName);
        Debug.Log("Onjoinedroom----------------------");
    }

    /*
    public override void OnPlayerEnteredRoom(Player newPlayer){
        this.GetComponent<PhotonView>().RPC("setName", RpcTarget.AllBuffered, this.name);
        Debug.Log(this.name);
        Debug.Log("Onplayerjoinedroom----------------------");
    }
    */

    [PunRPC]
    public void setName()
    {
        this.transform.GetChild(2).GetComponent<TextMeshPro>().text = this.GetComponent<PhotonView>().Owner.NickName;
    }
}
