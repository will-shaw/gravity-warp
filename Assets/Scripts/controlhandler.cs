using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlhandler : MonoBehaviour
{
    public GameObject pause;
    
    public void Back()
    {
        gameObject.SetActive(false);
        pause.SetActive(true);
    }
}