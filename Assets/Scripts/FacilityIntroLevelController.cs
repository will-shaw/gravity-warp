using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{
    public Transform[] doors = new Transform[0];
    public Collider2D[] triggers = new Collider2D[0];

    float time;

    public float gravityChangeTime;

    float gravityChangeTimer;

    void Start()
    {
        time = Time.realtimeSinceStartup;
        gravityChangeTimer = gravityChangeTime;
        doors[0].GetComponent<Door>().Gravity(true);
        doors[1].GetComponent<Door>().Gravity(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (Collider2D trigger in triggers)
        {
            if (other == trigger)
            {
                print(trigger.name);
            }
        }
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
