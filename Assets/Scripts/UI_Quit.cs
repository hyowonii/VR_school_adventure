using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quit : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        UI.SetActive(true);
    }
}
