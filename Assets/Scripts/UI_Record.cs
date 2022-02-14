using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Record : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip recording;
    //public AudioClip aud;
    int sampleRate = 44100;
    public float[] samples;
    

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        //samples = new float[sampleRate];
    }

    public void RecSnd()
    {
        if (aud.clip != null) Destroy(aud.clip);
        //aud.clip = Microphone.Start(null, false, 10, sampleRate);
        aud.clip = Microphone.Start(null, false, 900, sampleRate);
        while (!(Microphone.GetPosition(null) > 0)) ;
    }

    public void StopREC()
    {
       Microphone.End(Microphone.devices[0]);
    }

    public void PlaySnd()
    {
       aud.volume = 1.0f;
       aud.Play();
    }

    public void StopPlaying()
    {
        aud.Stop();
    }

    public void Saving()
    {
        //samples = new float[aud.clip.samples * aud.clip.channels];
        //recording.GetData(samples, 0);
        //aud.clip.setData(samples, 0);
        DateTime t = DateTime.Now;
        string today = t.ToString(" yyyy-MM-dd-HH-mm-ss");
        SavWav.Save("D:/recording/"+ today, aud.clip);
    }

}
