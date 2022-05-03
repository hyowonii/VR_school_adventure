using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class VR_control : MonoBehaviour
{
    public float speed = 2;
    public float rotSpeed = 500;

    private Vector2 stickInput;
    private float stickInputX;
    private float stickInputY;
    private float rx;
    private float ry;
    private float mx;
    private float my;
    public Animator animator;

    public Vector3 dir;
    public GameObject VRCamera;
    public GameObject VREyes;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        float scale = gameObject.transform.lossyScale.y;

        Debug.Log(scale);

        if (PhotonNetwork.IsConnected)
        {
            photonView = PhotonView.Get(gameObject.transform.GetChild(1));
            VRCamera.transform.localPosition = new Vector3(0, 1 / scale, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        photonView.RPC("setName", RpcTarget.All);

        if (animator == null)
        {
            animator = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        }

        if (OVRManager.isHmdPresent)
        {
            gameObject.transform.GetChild(1).transform.eulerAngles = new Vector3(0, VREyes.transform.eulerAngles.y, 0);

            stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (stickInput != Vector2.zero)
            {
                animator.SetInteger("actionID", 2);
            }

            else
            {
                animator.SetInteger("actionID", 1);
            }

            dir = VREyes.transform.forward * stickInput.y + VREyes.transform.right * stickInput.x;
            dir.Normalize();

            gameObject.transform.Translate(dir * speed * Time.deltaTime, Space.World); 
        }
    
        else
        {
              stickInputX = Input.GetAxis("Horizontal");
              stickInputY = Input.GetAxis("Vertical");
              dir = new Vector3(stickInputX, 0, stickInputY);

            if (stickInputX != 0 || stickInputY != 0)
            {
                animator.SetInteger("actionID", 2);
            }

            else
            {
                animator.SetInteger("actionID", 1);
            }

            mx = Input.GetAxis("Mouse X");
            my = Input.GetAxis("Mouse Y");
            
            rx += rotSpeed * my * Time.deltaTime;
            ry += rotSpeed * mx * Time.deltaTime;
            rx = Mathf.Clamp(rx, -40, 40);
            transform.rotation = Quaternion.Euler(0, ry, 0);
            VRCamera.transform.localRotation = Quaternion.Euler(-rx, 0, 0);

            dir.Normalize();
            gameObject.transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    [PunRPC]
    void setName()
    {
        TextMeshPro nametag = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        nametag.text = gameObject.name;
    }

}
