using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{

    public Transform leftDoor;

    float time;

    void Start() {
        time = Time.realtimeSinceStartup;
    }

    void Update() {
        if (Time.realtimeSinceStartup > time + 2) {
            leftDoor.GetComponent<Door>().CloseDoor();
        }
    }



}
