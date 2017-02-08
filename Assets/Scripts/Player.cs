using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    Transform player;
    float cooldown = 0;
    Transform canvas;
    Animator anim;
    public bool paused = false;
    float timer = 0f;

    public float speed = 10f;
    public string surface;
    public AudioClip[] landClip = new AudioClip[3];
    public AudioClip[] footsteps = new AudioClip[3];
    public AudioClip sliding;
    public RectTransform canvasPrefab;
    public bool facingRight = true;
    public Sprite spSide;
    public Sprite spUp;
    public GameObject pause;

    public float footstepDelay;
    float footstepTimer;

    void Awake()
    {
        // Immediately Instantiate canvas so that Glue.cs can access it.
        canvas = Instantiate(canvasPrefab, GameObject.Find("PlayerLevel").transform, true);
    }

    void Start()
    {
        footstepTimer = footstepDelay;
        player = transform;
        anim = GetComponent<Animator>();
        Camera.main.GetComponent<GravityWarp>().player = transform;
        Camera.main.GetComponent<GravityWarp>().boxes.Add(transform);
        Camera.main.GetComponent<CameraZoom>().player = transform;
        Debug.Log(Info.checkpoint.x);
        Debug.Log(Info.load);
        if(Info.load && Info.checkpoint.x != 0){
            Debug.Log("made");
           gameObject.transform.localPosition = Info.checkpoint;
        }
    }

    float CalculateVelocity(float x, float y)
    {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }

    public Transform GetCanvas()
    {
        return canvas;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            GetComponent<AudioSource>().PlayOneShot(landClip[Random.Range(0, 3)], 1);
        }
        else if (other.gameObject.GetComponent<BoxCollision>() != null && surface != "boxM" && surface != "boxW")
        {
            GetComponent<AudioSource>().PlayOneShot(sliding, 1);
        }
    }

    void FixedUpdate()
    {   

        float moveHori = 0;
        float moveVert = 0;

        if (Input.GetKey(InputManager.axisUp)) {
            moveVert = Input.GetAxis("Vertical");
        }
        if (Input.GetKey(InputManager.axisDown)) {
            moveVert = Input.GetAxis("Vertical");
        } 
        if (Input.GetKey(InputManager.axisLeft)) {
            moveHori = Input.GetAxis("Horizontal");
        } 
        if (Input.GetKey(InputManager.axisRight)) {
            moveHori = Input.GetAxis("Horizontal");
        }

        
        float cooldown = Camera.main.GetComponent<GravityWarp>().coolDown;
        if (cooldown != 0f)
        {
            cooldown = 2 - cooldown;
        }
        //canvas.FindChild("Xtext").GetComponent<Text>().text = "V:" + string.Format("{0:N2}", CalculateVelocity(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y));
        if (Camera.main.GetComponent<GravityWarp>().playerDead || paused)
        {
            moveHori = 0;
            moveVert = 0;
        }
        canvas.GetChild(6).GetComponent<Text>().text = "Cool Down: " + string.Format("{0:N2}", cooldown);
        canvas.GetChild(5).GetComponent<Text>().text = "X:" + string.Format("{0:N2}", GetComponent<Rigidbody2D>().velocity.x);
        canvas.GetChild(4).GetComponent<Text>().text = "Y:" + string.Format("{0:N2}", GetComponent<Rigidbody2D>().velocity.y);

        string gravity = GravityWarp.gravity;

        if(gravity == "U"){
            canvas.FindChild("controls").GetComponent<SpriteRenderer>().flipY = true;
            canvas.FindChild("controls").localPosition = new Vector3(-3.141f, -1.84f, 0);
            canvas.FindChild("GravityDirection").localPosition = new Vector3(-5.552f, -1.97f, 0);
            canvas.FindChild("GlueGUI").localPosition = new Vector3(-2.611f, -2.07f, 0);
            canvas.FindChild("Text").localPosition = new Vector3(-2.2731f, -2.22f, 0);
            canvas.FindChild("ytext").localPosition = new Vector3(-2.503f, -3.37f, 0);
            canvas.FindChild("xtext").localPosition = new Vector3(-3.686f, -3.37f, 0);
            canvas.FindChild("Cooldown").localPosition = new Vector3(-5.483f, -2.73f, 0);
        }else if(gravity =="D"){
            canvas.FindChild("controls").GetComponent<SpriteRenderer>().flipY = false;
            canvas.FindChild("controls").localPosition = new Vector3(-3.141f, 1.422f, 0);
            canvas.FindChild("GravityDirection").localPosition = new Vector3(-5.552f, 1.666f, 0);
            canvas.FindChild("GlueGUI").localPosition = new Vector3(-2.611f, 1.755f, 0);
            canvas.FindChild("Text").localPosition = new Vector3(-2.2731f, 1.51f, 0);
            canvas.FindChild("ytext").localPosition = new Vector3(-2.503f, 2.731f, 0);
            canvas.FindChild("xtext").localPosition = new Vector3(-3.686f, 2.731f, 0);
            canvas.FindChild("Cooldown").localPosition = new Vector3(-5.483f, 2.453f, 0);
        }
        if (gravity == "L" || gravity == "R")
        {
            if (!(player.GetComponent<Glue>().isGlued()))
            {
                anim.SetFloat("Speed", Mathf.Abs(moveVert));
                FootSteps(Mathf.Abs(moveVert));
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
                FootSteps(Mathf.Abs(moveHori));                
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

    void FootSteps(float speed)
    {
        if (speed > 0.1f && IsGrounded())
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(footsteps[Random.Range(0, 3)], 1);
                footstepTimer = footstepDelay;
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
            if (Input.GetKey(InputManager.menu) && !paused)
            {
                pause.SetActive(true);
                Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = false;
                Camera.main.GetComponent<GravityWarp>().time = false;
                paused = true;
                timer = 1f;
            }
            else if (Input.GetKey(InputManager.menu) && paused)
            {
                pause.SetActive(false);
                Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
                Camera.main.GetComponent<GravityWarp>().time = true;
                paused = false;
                timer = 1f;
            }
        }


        if (player != null)
        {
            anim.SetBool("isGrounded", IsGrounded());
            ChangeDirection();
            if (!(player.GetComponent<Glue>().isGlued()))
            {
                if (Input.GetKey(InputManager.jump) && Time.realtimeSinceStartup > cooldown + 0.1f && IsGrounded())
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
        bool check = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    check = true;
                    surface = "wall";
                }
                else if (hit.collider.GetComponent<Field>() && !hit.collider.GetComponent<Field>().laser)
                {
                    check = true;
                    surface = "forcefield";
                }
                else if (hit.collider.GetComponent<Button>() != null)
                {
                    check = true;
                    surface = "button";
                }
                else if (hit.collider.GetComponent<BoxCollision>() != null && hit.collider.GetComponent<BoxCollision>().metalBox)
                {
                    check = true;
                    surface = "boxM";
                }
                else if (hit.collider.GetComponent<BoxCollision>() != null && !hit.collider.GetComponent<BoxCollision>().metalBox)
                {
                    check = true;
                    surface = "boxW";
                }

                if (check)
                {
                    switch (grav)
                    {
                        case "U":
                        case "D":
                            if (Mathf.Abs(hit.point.y - transform.position.y) < dDist)
                            {
                                dDist = Mathf.Abs(hit.point.y - transform.position.y);
                            }
                            break;
                        case "L":
                        case "R":
                            if (Mathf.Abs(hit.point.x - transform.position.x) < dDist)
                            {
                                dDist = Mathf.Abs(hit.point.x - transform.position.x);
                            }
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