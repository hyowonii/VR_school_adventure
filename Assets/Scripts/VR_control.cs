using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class VR_control : MonoBehaviour
{
    public float speed = 5;
    Vector3 dir;
    public GameObject VRCamera;
    public GameObject Player;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        float scale = gameObject.transform.lossyScale.y;

        Debug.Log(scale);

        if (PhotonNetwork.IsConnected)
        {
            photonView = PhotonView.Get(this);
            VRCamera.transform.localPosition = new Vector3(0, 1/scale, 0);
        }
        gameObject.name = "player";
        gameObject.layer = 3;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        dir = new Vector3(stickInput.x, 0, stickInput.y);

        dir.Normalize();

        transform.forward = VRCamera.transform.forward;
        gameObject.transform.Translate(dir * speed * Time.deltaTime);
    }
}
