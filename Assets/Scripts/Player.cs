using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /** Object components. */
    Transform player;
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator anim;

    float cooldown = 0;
    Transform canvas;

    AudioManager am;
    float timer = 0f;
    float footstepTimer;

    public bool paused = false;
    public float speed = 10f;
    public string surface;
    public RectTransform canvasPrefab;
    public bool facingRight = true;
    public Sprite spSide;
    public Sprite spUp;
    public GameObject menu;
    public float footstepDelay;

    public GameObject gunRight;
    public GameObject gunLeft;
    public GameObject remoteLeft;
    public GameObject remoteRight;

    void Awake()
    {
        // Immediately Instantiate canvas so that Glue.cs can access it.
        canvas = menu.gameObject.transform.FindChild("PlayerDetails");
    }

    void Start()
    {
        footstepTimer = footstepDelay;
        player = transform;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        menu.GetComponent<MenuHandler>().player = player;
        am = Camera.main.GetComponent<AudioManager>();
        Camera.main.GetComponent<GravityWarp>().player = player;
        Camera.main.GetComponent<CameraZoom>().player = player;
        if (Info.load && Info.checkpoint.x != 0)
        {
            gameObject.transform.localPosition = Info.checkpoint;
            Info.load = false;
            Camera.main.GetComponent<GravityWarp>().leveltmr = Info.checktime;
        }
        anim.SetBool("facingRight", facingRight);
        player.GetChild(0).gameObject.SetActive(facingRight);
        player.GetChild(1).gameObject.SetActive(!facingRight);
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
        if (other.gameObject.tag == "Wall" || (other.collider.gameObject.GetComponent<Field>() != null && !other.collider.gameObject.GetComponent<Field>().laser))
        {
            audioSource.PlayOneShot(am.GetLanding(), Random.Range(40, 100) * 0.01f);
        }
        else if (other.gameObject.GetComponent<BoxCollision>() != null && surface != "boxM" && surface != "boxW")
        {
            //new sound code
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(am.GetBoxSlide(), 1);
            if (!IsBoxAbove(other.transform.position))
            {
                anim.SetBool("isPushing", true);
            }
            other.gameObject.GetComponent<AudioSource>().volume = 1;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BoxCollision>() != null)
        {
            anim.SetBool("isPushing", false);
        }
    }

    void FixedUpdate()
    {

        float moveHori = 0;
        float moveVert = 0;

        if (Input.GetKey(InputManager.axisUp))
        {
            moveVert = Input.GetAxis("Vertical");
        }
        if (Input.GetKey(InputManager.axisDown))
        {
            moveVert = Input.GetAxis("Vertical");
        }
        if (Input.GetKey(InputManager.axisLeft))
        {
            moveHori = Input.GetAxis("Horizontal");
        }
        if (Input.GetKey(InputManager.axisRight))
        {
            moveHori = Input.GetAxis("Horizontal");
        }


        float cooldown = Camera.main.GetComponent<GravityWarp>().coolDown;
        if (cooldown != 0f)
        {
            cooldown = 2 - cooldown;
        }
        if (Camera.main.GetComponent<GravityWarp>().playerDead || paused)
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
                FootSteps(Mathf.Abs(moveVert));
            }
            rb.velocity = new Vector2(rb.velocity.x, moveVert * speed);
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
            rb.velocity = new Vector2(moveHori * speed, rb.velocity.y);
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
                audioSource.PlayOneShot(am.GetFootstep(), Random.Range(60, 100) * 0.01f);
                footstepTimer = footstepDelay;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        anim.SetBool("facingRight", facingRight);
        transform.GetChild(0).gameObject.SetActive(facingRight);
        transform.GetChild(1).gameObject.SetActive(!facingRight);
    }

    void Update()
    {
        if (GetComponent<GlueControl>().glueEnabled)
        {
            gunLeft.SetActive(true);
            gunRight.SetActive(true);
        }
        if (Camera.main.GetComponent<GravityWarp>().gravityControlEnabled)
        {
            remoteLeft.SetActive(true);
            remoteRight.SetActive(true);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(InputManager.menu) && !paused)
            {
                menu.GetComponent<MenuHandler>().ShowPause();
                canvas.gameObject.SetActive(false);
                Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = false;
                Camera.main.GetComponent<GravityWarp>().time = false;
                paused = true;
                timer = 1f;
            }
            else if (Input.GetKey(InputManager.menu) && paused)
            {
                canvas.gameObject.SetActive(true);
                menu.GetComponent<MenuHandler>().Hide();
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
                                rb.AddForce(new Vector2(0, 700.0f));
                                break;
                            case "U":
                                rb.AddForce(new Vector2(0, -700.0f));
                                break;
                            case "L":
                                rb.AddForce(new Vector2(700.0f, 0));
                                break;
                            case "R":
                                rb.AddForce(new Vector2(-700.0f, 0));
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
                    player.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case "U":
                    player.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case "L":
                    player.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case "R":
                    player.eulerAngles = new Vector3(0, 0, 90);
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

    public void antiPhase()
    {
        Vector2 down = Vector2.down;
        Vector2 up = Vector2.up;
        string grav = GravityWarp.gravity;

        switch (grav)
        {
            case "U":
                down = Vector2.up;
                up = Vector2.down;
                break;
            case "L":
                down = Vector2.left;
                up = Vector2.right;
                break;
            case "R":
                down = Vector2.right;
                up = Vector2.left;
                break;
            case "D":
                down = Vector2.down;
                up = Vector2.up;
                break;
        }
        RaycastHit2D[] hitsDown = Physics2D.RaycastAll(transform.position, down, 3.0f);
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(transform.position, up, 3.0f);
        if (hitsDown.Length == 0 && !GetComponent<Glue>().isGlued())
        {
            switch (grav)
            {
                case "U":
                    player.position += new Vector3(0, 1, 0);
                    break;
                case "L":
                    player.position += new Vector3(-1, 0, 0);
                    break;
                case "R":
                    player.position += new Vector3(1, 0, 0);
                    break;
                case "D":
                    player.position += new Vector3(0, -1, 0);
                    break;
            }
        }
        else if (hitsUp.Length == 0 && !GetComponent<Glue>().isGlued())
        {
            switch (grav)
            {
                case "U":
                    player.position += new Vector3(0, -1, 0);
                    break;
                case "L":
                    player.position += new Vector3(1, 0, 0);
                    break;
                case "R":
                    player.position += new Vector3(-1, 0, 0);
                    break;
                case "D":
                    player.position += new Vector3(0, 1, 0);
                    break;
            }
        }
        return;
    }

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

        RaycastHit2D[] hits = Physics2D.RaycastAll(player.position, down, 3.0f);
        Debug.DrawRay(player.position, down, Color.yellow, 10);

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
                else if (hit.collider.GetComponent<ButtonScript>() != null)
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
                    Debug.DrawLine(player.position, hit.point, Color.green, 10);
                }
            }
        }

        if (dDist <= 1.9f)
        {
            return true;
        }
        return false;
    }

    bool IsBoxAbove(Vector2 other)
    {
        float offset = 1.5f;

        switch (GravityWarp.gravity)
        {
            case "D":
                if (other.y > player.position.y + offset)
                {
                    print("Above");
                    return true;
                }
                break;
            case "U":
                if (other.y < player.position.y - offset)
                {
                    print("Above");
                    return true;
                }
                break;
            case "L":
                if (other.x > player.position.x + offset)
                {
                    print("Above");
                    return true;
                }
                break;
            case "R":
                if (other.x < player.position.x - offset)
                {
                    print("Above");
                    return true;
                }
                break;
        }

        return false;
    }

}