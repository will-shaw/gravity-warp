using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{
    public float gravityChangeTime;

    public float shipLandDelay = 2;

    public Rigidbody2D hatch;

    public Animator ship;

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
            hatch.constraints = RigidbodyConstraints2D.None;
            ship.SetBool("Landed", true);
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