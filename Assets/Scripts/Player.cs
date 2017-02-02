using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bg;
    Transform player;
    public float speed = 10f;
    float cooldown = 0;
    public Rigidbody2D rb2D;
    bool facingRight = true;
    Animator anim;

    void Start() {
        player = transform;
        rb2D = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Camera.main.GetComponent<GravityWarp>().boxes.Add(transform);
        Camera.main.GetComponent<CameraZoom>().player = transform;
    }

    void FixedUpdate() {
        float move = Input.GetAxis("Horizontal");
        
        anim.SetFloat("Speed", Mathf.Abs(move));

        if(GravityWarp.gravity =="L" || GravityWarp.gravity == "R"){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, move * speed );
        }else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (move > 0 && !facingRight) {
            Flip();
        } else if (move < 0 && facingRight) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        ChangeDirection();
        if(!(player.GetComponent<Glue>().isGlued())) {
         /*   if (Input.GetKey(KeyCode.D)) {
                //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * 250f, 0));
                if(GravityWarp.gravity == "R") {
                    player.transform.Translate(new Vector3(0, Time.deltaTime * speed * -1,0), Space.World);
                } else if (GravityWarp.gravity == "U" || GravityWarp.gravity == "D"){
                    player.transform.Translate(new Vector3(Time.deltaTime * speed,0,0), Space.World);
                } else if (GravityWarp.gravity == "L"){
                    player.transform.Translate(new Vector3(0, Time.deltaTime * speed,0), Space.World);
                } else if (GravityWarp.gravity == "U"){
                    player.transform.Translate(new Vector3(Time.deltaTime * speed,0,0), Space.World);
                }		
            } else if (Input.GetKey(KeyCode.A)) {
                //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -250f, 0));
                if(GravityWarp.gravity == "R") {
                    player.transform.Translate(new Vector3(0, Time.deltaTime * speed,0), Space.World);
                } else if (GravityWarp.gravity == "U" || GravityWarp.gravity == "D"){
                    player.transform.Translate(new Vector3(Time.deltaTime * speed * -1,0,0), Space.World);
                } else if (GravityWarp.gravity == "L"){
                    player.transform.Translate(new Vector3(0, Time.deltaTime * speed * -1,0), Space.World);
                }								
            }
*/
            if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
            {
                if(GravityWarp.gravity == "D") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 700.0f));
                } else if(GravityWarp.gravity == "U") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -700.0f));
                } else if(GravityWarp.gravity == "L") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500.0f, 0));
                } else if(GravityWarp.gravity == "R") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(500.0f, 0));
                } 
                cooldown = Time.realtimeSinceStartup;
            }
        }
    }

    void ChangeDirection() {
        string nearestWall = GetNearestWall();
        if (nearestWall == "Down" && GravityWarp.gravity == "D") {
            player.transform.rotation = new Quaternion(0,0,0,0);
        } else if (nearestWall == "Up" && GravityWarp.gravity == "U") {
            player.transform.rotation = new Quaternion(0,0,180,0);
        } else if (nearestWall == "Left" && GravityWarp.gravity == "L") {
            player.transform.rotation = new Quaternion(0,0,270,0);
        } else if (nearestWall == "Right" && GravityWarp.gravity == "R") {
            player.transform.rotation = new Quaternion(0,0,90,0);
        }
    }

    string GetNearestWall() {
        RaycastHit2D down = Physics2D.Raycast(rb2D.position, Vector2.down, 6.0f);
        RaycastHit2D up = Physics2D.Raycast(rb2D.position, Vector2.up, 6.0f);
        RaycastHit2D left = Physics2D.Raycast(rb2D.position, Vector2.left, 6.0f);
        RaycastHit2D right = Physics2D.Raycast(rb2D.position, Vector2.right, 6.0f);
        float dDist = 100f;
        float uDist = 100f;
        float lDist = 100f;
        float rDist = 100f;

        if (down.collider != null && down.collider.gameObject.tag == "Wall") {
            dDist = Mathf.Abs(down.point.y - rb2D.position.y);
        }
        if (up.collider != null && up.collider.gameObject.tag == "Wall") {
            uDist = Mathf.Abs(up.point.y - rb2D.position.y);
        }
        if (left.collider != null && left.collider.gameObject.tag == "Wall") {
            lDist = Mathf.Abs(left.point.x - rb2D.position.x);
        }
        if (right.collider != null && right.collider.gameObject.tag == "Wall") {
            rDist = Mathf.Abs(right.point.x - rb2D.position.x);
        }

        float smallest = Mathf.Min(dDist, uDist, lDist, rDist);

        if (smallest == dDist && dDist != 100f) return "Down";
        if (smallest == uDist && uDist != 100f) return "Up";
        if (smallest == lDist && lDist != 100f) return "Left";
        if (smallest == rDist && rDist != 100f) return "Right";

        return "Floating";
    }

}