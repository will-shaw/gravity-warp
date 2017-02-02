using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Transform bg;
    Transform player;
    public float speed = 10f;
    float cooldown = 0;
    public Rigidbody2D rb2D;

    public RectTransform canvas;
    bool facingRight = true;
    Animator anim;

    public Sprite spSide;

    public Sprite spUp;
    void Start() {
        player = transform;
        rb2D = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Camera.main.GetComponent<GravityWarp>().boxes.Add(transform);
        Camera.main.GetComponent<CameraZoom>().player = transform;
    }

    float CalculateVelocity(float x, float y) {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }

    void FixedUpdate() {
        float move = Input.GetAxis("Horizontal");
        
        //canvas.FindChild("Xtext").GetComponent<Text>().text = "V:" + string.Format("{0:N2}", CalculateVelocity(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y));
        
        canvas.FindChild("xtext").GetComponent<Text>().text = "X:" + string.Format("{0:N2}", GetComponent<Rigidbody2D>().velocity.x);
        canvas.FindChild("ytext").GetComponent<Text>().text = "Y:" + string.Format("{0:N2}", GetComponent<Rigidbody2D>().velocity.y);
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

        Vector3 hello = gameObject.transform.position;
        canvas.position = hello;

        if (player != null) {
            ChangeDirection();
        }
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
            if (Input.GetKey(KeyCode.Space) && Time.realtimeSinceStartup > cooldown + 1)
            {
                if(GravityWarp.gravity == "D") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 700.0f));
                } else if(GravityWarp.gravity == "U") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -700.0f));
                } else if(GravityWarp.gravity == "L") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(550.0f, 0));
                } else if(GravityWarp.gravity == "R") {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-550.0f, 0));
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
            player.transform.rotation = new Quaternion(0,0,-90,0);
        }  
        Transform gravDirect = canvas.FindChild("GravityDirection");
        if(GravityWarp.gravity == "R"){
            gravDirect.GetComponent<SpriteRenderer>().sprite = spSide;
            gravDirect.GetComponent<SpriteRenderer>().flipX = true;
        }else if(GravityWarp.gravity == "U"){
            gravDirect.GetComponent<SpriteRenderer>().flipY = false;
            gravDirect.GetComponent<SpriteRenderer>().sprite = spUp;
        }else if(GravityWarp.gravity == "D"){
            gravDirect.GetComponent<SpriteRenderer>().flipY = true;
            gravDirect.GetComponent<SpriteRenderer>().sprite = spUp;
        }else if(GravityWarp.gravity == "L"){
            gravDirect.GetComponent<SpriteRenderer>().sprite = spSide;
            gravDirect.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    string GetNearestWall() {
        RaycastHit2D[] down = Physics2D.RaycastAll(rb2D.position, Vector2.down, 6.0f);
        RaycastHit2D[] up = Physics2D.RaycastAll(rb2D.position, Vector2.up, 6.0f);
        RaycastHit2D[] left = Physics2D.RaycastAll(rb2D.position, Vector2.left, 6.0f);
        RaycastHit2D[] right = Physics2D.RaycastAll(rb2D.position, Vector2.right, 6.0f);

        Debug.DrawRay(transform.position, Vector2.down, Color.red, 10);
        Debug.DrawRay(transform.position, Vector2.up, Color.red, 10);
        Debug.DrawRay(transform.position, Vector2.left, Color.yellow, 10);
        Debug.DrawRay(transform.position, Vector2.right, Color.yellow, 10);

        float dDist = 100f;
        float uDist = 100f;
        float lDist = 100f;
        float rDist = 100f;

        foreach(RaycastHit2D hit in down) {
            if (hit.collider != null && hit.collider.gameObject.tag == "Wall") {
                dDist = Mathf.Abs(hit.point.y - rb2D.position.y);
                Debug.DrawLine(transform.position, hit.point, Color.green, 10);
            }
        }
        foreach(RaycastHit2D hit in up) {
            if (hit.collider != null && hit.collider.gameObject.tag == "Wall") {
                uDist = Mathf.Abs(hit.point.y - rb2D.position.y);
                Debug.DrawLine(transform.position, hit.point, Color.green, 10);
            }
        }
        foreach(RaycastHit2D hit in left) {
            if (hit.collider != null && hit.collider.gameObject.tag == "Wall") {
                lDist = Mathf.Abs(hit.point.y - rb2D.position.x);
                Debug.DrawLine(transform.position, hit.point, Color.blue, 10);
            }
        }
        foreach(RaycastHit2D hit in right) {
            if (hit.collider != null && hit.collider.gameObject.tag == "Wall") {
                rDist = Mathf.Abs(hit.point.y - rb2D.position.y);
                Debug.DrawLine(transform.position, hit.point, Color.blue, 10);
            }
        }

        float smallest = Mathf.Min(dDist, uDist, lDist, rDist);

        if (smallest == dDist && dDist != 100f) return "Down";
        if (smallest == uDist && uDist != 100f) return "Up";
        if (smallest == lDist && lDist != 100f) return "Left";
        if (smallest == rDist && rDist != 100f) return "Right";

        return "Floating";
    }

}