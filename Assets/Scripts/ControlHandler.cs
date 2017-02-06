using UnityEngine;

public class ControlHandler : MonoBehaviour
{
    public GameObject pause;
    
    public void Back()
    {
        gameObject.SetActive(false);
        pause.SetActive(true);
    }
}