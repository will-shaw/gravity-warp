using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{
    public float gravityChangeTime;

    public float shipLandDelay = 2;

    float gravityChangeTimer;
    float shipLandTimer;

    bool playerActive;

    void Start()
    {
        gravityChangeTimer = gravityChangeTime;
        shipLandTimer = shipLandDelay;
    }

    void Update()
    {
        if (!playerActive) {
            GetComponent<GravityWarp>().player.gameObject.SetActive(false);
        }
        gravityChangeTimer -= Time.deltaTime;
        shipLandTimer -= Time.deltaTime;
        if (shipLandTimer <= 0) {
            GetComponent<GravityWarp>().player.gameObject.SetActive(true);
            playerActive = true;
        }
        if (gravityChangeTimer <= 0)
        {
            GravityWarp.gravity = GravityWarp.gravity == "D" ? "U" : "D";
            gravityChangeTimer = gravityChangeTime;
        }
    }

}