using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{
    public Transform[] doors = new Transform[0];

    public float gravityChangeTime;

    float gravityChangeTimer;

    void Start()
    {
        gravityChangeTimer = gravityChangeTime;
        doors[0].GetComponent<Door>().Gravity(true);
        doors[1].GetComponent<Door>().Gravity(true);
    }

    void Update()
    {
        gravityChangeTimer -= Time.deltaTime;
        if (gravityChangeTimer <= 0)
        {
            GravityWarp.gravity = GravityWarp.gravity == "D" ? "U" : "D";
            gravityChangeTimer = gravityChangeTime;
        }
    }

}