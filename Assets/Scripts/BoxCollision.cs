
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    GravityWarp mainWarp;
    AudioManager am;

    public bool metalBox;

    float velocityY = 0f;
    float velocityX = 0f;
    float velocityY1 = 0f;
    float velocityX1 = 0f;
    GameObject activeButton;
    GameObject activeGlue;
    // Use this for initialization
    void Start()
    {
        am = Camera.main.GetComponent<AudioManager>();
        mainWarp = Camera.main.GetComponent<GravityWarp>();
    }

    // Update is called once per frame
    void Update()
    {
        // new audio code
        if (gameObject.transform.GetComponent<Rigidbody2D>().velocity.x == 0f && gameObject.transform.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            while (gameObject.GetComponent<AudioSource>().volume > 0)
            {
                gameObject.GetComponent<AudioSource>().volume -= 1 * Time.deltaTime / 2f;
            }

            gameObject.GetComponent<AudioSource>().Stop();
        }
        if (velocityX < gameObject.transform.GetComponent<Rigidbody2D>().velocity.x)
        {
            velocityX = gameObject.transform.GetComponent<Rigidbody2D>().velocity.x;
        }
        if (velocityY < gameObject.transform.GetComponent<Rigidbody2D>().velocity.y)
        {
            velocityY = gameObject.transform.GetComponent<Rigidbody2D>().velocity.y;
        }
        if (velocityX1 > gameObject.transform.GetComponent<Rigidbody2D>().velocity.x)
        {
            velocityX1 = gameObject.transform.GetComponent<Rigidbody2D>().velocity.x;
        }
        if (velocityY1 > gameObject.transform.GetComponent<Rigidbody2D>().velocity.y)
        {
            velocityY1 = gameObject.transform.GetComponent<Rigidbody2D>().velocity.y;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            GetComponent<AudioSource>().PlayOneShot(am.GetBoxHit(metalBox), 1);
        }

        if (other.transform.CompareTag("box destruct"))
        {
            mainWarp.boxes.Remove(gameObject.transform);
            Destroy(gameObject);
        }
        else if (!(other.transform.CompareTag("Wall")) && !(other.transform.CompareTag("Player")))
        {
            if (other.gameObject.tag == "destructable")
            {
                if (velocityX1 < -20f)
                {
                    if (other.gameObject.GetComponent<BoxCollision>().activeButton != null)
                    {
                        other.gameObject.GetComponent<BoxCollision>().activeButton.GetComponent<ButtonScript>().SubLink();
                    }
                    if (other.gameObject.GetComponent<BoxCollision>().activeGlue != null)
                    {
                        Destroy(other.gameObject.GetComponent<BoxCollision>().activeGlue);
                    }
                    mainWarp.boxes.Remove(other.transform);
                    Destroy(other.gameObject);
                }
                if (velocityX > 20f)
                {
                    if (other.gameObject.GetComponent<BoxCollision>().activeButton != null)
                    {
                        other.gameObject.GetComponent<BoxCollision>().activeButton.GetComponent<ButtonScript>().SubLink();
                    }
                    if (other.gameObject.GetComponent<BoxCollision>().activeGlue != null)
                    {
                        Destroy(other.gameObject.GetComponent<BoxCollision>().activeGlue);
                    }
                    mainWarp.boxes.Remove(other.transform);
                    Destroy(other.gameObject);
                }
                if (velocityY > 30f)
                {
                    if (other.gameObject.GetComponent<BoxCollision>().activeButton != null)
                    {
                        other.gameObject.GetComponent<BoxCollision>().activeButton.GetComponent<ButtonScript>().SubLink();
                    }
                    if (other.gameObject.GetComponent<BoxCollision>().activeGlue != null)
                    {
                        Destroy(other.gameObject.GetComponent<BoxCollision>().activeGlue);
                    }
                    mainWarp.boxes.Remove(other.transform);
                    Destroy(other.gameObject);
                }
                if (velocityY1 < -30f)
                {
                    if (other.gameObject.GetComponent<BoxCollision>().activeButton != null)
                    {
                        other.gameObject.GetComponent<BoxCollision>().activeButton.GetComponent<ButtonScript>().SubLink();
                    }
                    if (other.gameObject.GetComponent<BoxCollision>().activeGlue != null)
                    {
                        Destroy(other.gameObject.GetComponent<BoxCollision>().activeGlue);
                    }
                    mainWarp.boxes.Remove(other.transform);
                    Destroy(other.gameObject);
                }
            }
        }
        else if ((other.transform.CompareTag("Player")))
        {
            if (velocityY > 18f)
            {
                //Destroy(other.gameObject);
                Camera.main.GetComponent<GravityWarp>().playerDead = true;
            }
            if (velocityX > 18f)
            {
                //Destroy(other.gameObject);
                Camera.main.GetComponent<GravityWarp>().playerDead = true;
            }
            if (velocityY1 < -18f)
            {
                //Destroy(other.gameObject);
                Camera.main.GetComponent<GravityWarp>().playerDead = true;
            }
            if (velocityX1 < -18f)
            {
                //Destroy(other.gameObject);
                Camera.main.GetComponent<GravityWarp>().playerDead = true;
            }
        }
        velocityX = 0f;
        velocityY = 0f;
        velocityX1 = 0f;
        velocityY1 = 0f;
    }

    public void setActiveButton(GameObject button)
    {
        activeButton = button;
    }

    public void setActiveGlue(GameObject glue)
    {
        activeGlue = glue;
    }
}