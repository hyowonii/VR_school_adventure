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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom(){
        this.GetComponent<PhotonView>().RPC("setName", RpcTarget.All, this.name);
        Debug.Log(this.name);
        Debug.Log("Onjoinedroom----------------------");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        this.GetComponent<PhotonView>().RPC("setName", RpcTarget.All, this.name);
        Debug.Log(this.name);
        Debug.Log("Onplayerjoinedroom----------------------");
    }

    [PunRPC]
    public void setName(string name)
    {
        this.transform.GetChild(2).GetComponent<TextMeshPro>().text = name;
    }
}
