using UnityEngine;

public class GravGen : MonoBehaviour
{

    Animator anim;

    public Transform door;

    bool once;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && !once)
        {
            anim.SetBool("explode", true);
            GetComponent<AudioSource>().Play();
            door.GetComponent<Door>().ActivateLink(1);
            GravityWarp.gravity = "D";
            Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = false;
            once = true;
        }
    }

    void FixedUpdate() {
        switch(GravityWarp.gravity) {
            case "U":
                anim.SetInteger("gravity", 1);
                break;
            case "D":
                anim.SetInteger("gravity", 0);
                break;
            case "L":
                anim.SetInteger("gravity", 2);
                break;
            case "R":
                anim.SetInteger("gravity", 3);
                break;
        }
    }

}
