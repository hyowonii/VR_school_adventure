using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Recording : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip recording;
    int sampleRate = 44100;
    public float[] samples;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();

        Debug.Log("recording ready");
    }

    // Update is called once per frame
    void Update()
    {
        BtnDown();
    }

    void BtnDown()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three) || Input.GetKeyDown("1"))
        {
            if (aud.clip != null) Destroy(aud.clip);
            aud.clip = Microphone.Start(null, false, 900, sampleRate);
            while (!(Microphone.GetPosition(null) > 0)) ;
        }

        if (OVRInput.GetDown(OVRInput.Button.Four) || Input.GetKeyDown("2"))
        {
            Microphone.End(Microphone.devices[0]);
        }

        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown("3"))
        {
            DateTime t = DateTime.Now;
            string today = t.ToString(" yyyy-MM-dd-HH-mm-ss");
            SavWav.Save("D:/recording/" + today, aud.clip);
        }
    }


}
