using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{
    public float gravityChangeTime;

    public float shipLandDelay = 5;

    public Rigidbody2D hatch;

    public Animator ship;

    public Transform player;

    public AudioClip rocket;

    float gravityChangeTimer;
    float shipLandTimer;

    bool playerActive;

    void Start()
    {
        gravityChangeTimer = gravityChangeTime;
        shipLandTimer = shipLandDelay;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(rocket, 1);
    }

    void Update()
    {
        if (!playerActive)
        {
            GetComponent<GravityWarp>().player.gameObject.SetActive(false);
        }
        gravityChangeTimer -= Time.deltaTime;
        shipLandTimer -= Time.deltaTime;
        if (shipLandTimer <= 0 && !playerActive)
        {
            hatch.constraints = RigidbodyConstraints2D.None;
            ship.SetBool("Landed", true);
            GetComponent<GravityWarp>().player.gameObject.SetActive(true);
            player.GetComponent<Animator>().SetBool("facingRight", true);
            player.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetDoorClip(), 1);
            playerActive = true;
            MusicPlayer.Instance.PlayMusic();
        }
        if (gravityChangeTimer <= 0)
        {
            GravityWarp.gravity = GravityWarp.gravity == "D" ? "U" : "D";
            gravityChangeTimer = gravityChangeTime;
        }
    }

}