using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    Transform player;
    public float speed = 10f;
    float cooldown = 0;
    public Rigidbody2D rb2D;

    public Transform canvas;
    public RectTransform canvasPrefab;

    public bool facingRight = true;
    Animator anim;
    bool active = false;
    public Sprite spSide;
    public Sprite spUp;

    public GameObject pause;
    float timer = 0f;
    void Awake()
    {
        // Immediately Instantiate canvas so that Glue.cs can access it.
        canvas = Instantiate(canvasPrefab, GameObject.Find("PlayerLevel").transform, true);
    }

    void Start()
    {
        player = transform;
        rb2D = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Camera.main.GetComponent<GravityWarp>().player = transform;
        Camera.main.GetComponent<GravityWarp>().boxes.Add(transform);
        Camera.main.GetComponent<CameraZoom>().player = transform;
    }

    float CalculateVelocity(float x, float y)
    {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }

    void FixedUpdate()
    {
        float moveHori = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        float cooldown = Camera.main.GetComponent<GravityWarp>().coolDown;
        if (cooldown != 0f)
        {
            cooldown = 2 - cooldown;
        }
        //canvas.FindChild("Xtext").GetComponent<Text>().text = "V:" + string.Format("{0:N2}", CalculateVelocity(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y));
        if (Camera.main.GetComponent<GravityWarp>().playerDead)
        {
            moveHori = 0;
            moveVert = 0;
        }
        canvas.GetChild(6).GetComponent<Text>().text = "Cool Down: " + string.Format("{0:N2}", cooldown);
        canvas.GetChild(5).GetComponent<Text>().text = "X:" + string.Format("{0:N2}", GetComponent<Rigidbody2D>().velocity.x);
        canvas.GetChild(4).GetComponent<Text>().text = "Y:" + string.Format("{0:N2}", GetComponent<Rigidbody2D>().velocity.y);

        string gravity = GravityWarp.gravity;

        if (gravity == "L" || gravity == "R")
        {
            if (!(player.GetComponent<Glue>().isGlued()))
            {
                anim.SetFloat("Speed", Mathf.Abs(moveVert));
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveVert * speed);
            if (moveVert > 0 && !facingRight && gravity == "R")
            {
                Flip();
            }
            else if (moveVert < 0 && facingRight && gravity == "R")
            {
                Flip();
            }
            else if (moveVert > 0 && facingRight && gravity == "L")
            {
                Flip();
            }
            else if (moveVert < 0 && !facingRight && gravity == "L")
            {
                Flip();
            }
        }
        else
        {
            if (!(player.GetComponent<Glue>().isGlued()))
            {
                anim.SetFloat("Speed", Mathf.Abs(moveHori));
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveHori * speed, GetComponent<Rigidbody2D>().velocity.y);
            if (moveHori > 0 && !facingRight && gravity == "D")
            {
                Flip();
            }
            else if (moveHori < 0 && facingRight && gravity == "D")
            {
                Flip();
            }
            else if (moveHori > 0 && facingRight && gravity == "U")
            {
                Flip();
            }
            else if (moveHori < 0 && !facingRight && gravity == "U")
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        canvas.position = transform.position;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.H) && !active)
            {
                pause.SetActive(true);
                timer = 1f;
            }
            else if (Input.GetKey(KeyCode.H) && active)
            {
                pause.SetActive(false);
                timer = 1f;
            }
        }


        if (player != null)
        {
            anim.SetBool("isGrounded", IsGrounded());
            ChangeDirection();
            if (!(player.GetComponent<Glue>().isGlued()))
            {
                if (Input.GetKey(KeyCode.Space) && Time.realtimeSinceStartup > cooldown + 0.1f && IsGrounded())
                {
                    if (!(Camera.main.GetComponent<GravityWarp>().playerDead))
                    {
                        switch (GravityWarp.gravity)
                        {
                            case "D":
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 700.0f));
                                break;
                            case "U":
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -700.0f));
                                break;
                            case "L":
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(500.0f, 0));
                                break;
                            case "R":
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500.0f, 0));
                                break;
                        }
                        cooldown = Time.realtimeSinceStartup;
                    }
                }
            }
        }
    }

    /* Sets direction of player and floating UI indicator based on Gravity. */
    void ChangeDirection()
    {
        if (!(GetComponent<Glue>().isGlued()))
        {
            switch (GravityWarp.gravity)
            {
                case "D":
                    player.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case "U":
                    player.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case "L":
                    player.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case "R":
                    player.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
            }
        }

        SpriteRenderer spr = canvas.GetChild(1).GetComponent<SpriteRenderer>();
        switch (GravityWarp.gravity)
        {
            case "D":
                spr.flipY = true;
                spr.sprite = spUp;
                break;
            case "U":
                spr.flipY = false;
                spr.sprite = spUp;
                break;
            case "L":
                spr.sprite = spSide;
                spr.flipX = false;
                break;
            case "R":
                spr.sprite = spSide;
                spr.flipX = true;
                break;
        }
    }

    /* Checks how far the player is from the floor. (Object must be tagged with 'Wall').
        Fields & Buttons are detected too */
    bool IsGrounded()
    {
        Vector2 down = Vector2.down;
        string grav = GravityWarp.gravity;
        switch (grav)
        {
            case "U":
                down = Vector2.up;
                break;
            case "L":
                down = Vector2.left;
                break;
            case "R":
                down = Vector2.right;
                break;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, down, 3.0f);
        Debug.DrawRay(transform.position, down, Color.yellow, 10);

        float dDist = 100f;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Wall" || (hit.collider.GetComponent<Field>() && !hit.collider.GetComponent<Field>().laser) || hit.collider.GetComponent<Button>() != null || hit.collider.GetComponent<BoxColision>() != null)
                {
                    switch (grav)
                    {
                        case "U":
                        case "D":
                            dDist = Mathf.Abs(hit.point.y - transform.position.y);
                            break;
                        case "L":
                        case "R":
                            dDist = Mathf.Abs(hit.point.x - transform.position.x);
                            break;
                    }
                    Debug.DrawLine(transform.position, hit.point, Color.green, 10);
                }
            }
        }
        if (dDist <= 1.7f)
        {
            return true;
        }
        return false;
    }

}