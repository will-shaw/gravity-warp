using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{
    public float gravityChangeTime;

    float gravityChangeTimer;

    void Start()
    {
        gravityChangeTimer = gravityChangeTime;
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