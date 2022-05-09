using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;

public class SetName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void setName()
    {
        this.transform.GetChild(2).GetComponent<TextMeshPro>().text = PhotonNetwork.NickName;
    }
}
