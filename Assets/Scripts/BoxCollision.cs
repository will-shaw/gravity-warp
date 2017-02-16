
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    AudioManager am;
    public bool metalBox;

    public float velocityY = 0f;
    public float velocityX = 0f;
    public float velocityY1 = 0f;
    public float velocityX1 = 0f;
    GameObject activeButton;
    GameObject activeGlue;
    bool velocityPositive = false;
    bool velocitynegitive = false;

    public Transform boxFragmentPrefab;

    float tmr = 0.0f;
    void Start()
    {
        am = Camera.main.GetComponent<AudioManager>();
    }

    void Update()
    {


        // new audio code
        if (gameObject.transform.GetComponent<Rigidbody2D>().velocity.x == 0f && gameObject.transform.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            while (gameObject.GetComponent<AudioSource>().volume > 0)
            {
                //Debug.Log(gameObject.GetComponent<AudioSource>().volume);
                gameObject.GetComponent<AudioSource>().volume -= 1 * Time.deltaTime / 2f;
            }

            gameObject.GetComponent<AudioSource>().Stop();
        }
        if (gameObject.transform.GetComponent<Rigidbody2D>().velocity.y < 1 && gameObject.transform.GetComponent<Rigidbody2D>().velocity.x < 1)
        {
            velocityPositive = false;
        }
        else
        {
            velocityPositive = true;
        }
        if (gameObject.transform.GetComponent<Rigidbody2D>().velocity.y > -1 && gameObject.transform.GetComponent<Rigidbody2D>().velocity.x > -1)
        {
            velocitynegitive = false;
        }
        else
        {
            velocitynegitive = true;
        }
        if (!(velocitynegitive) && !(velocityPositive))
        {
            tmr += Time.deltaTime;
            if (tmr > 1)
            {
                velocityX = 0f;
                velocityX1 = 0f;
                velocityY = 0f;
                velocityY1 = 0f;
            }

        }
        else
        {
            tmr = 0f;
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

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            GetComponent<AudioSource>().PlayOneShot(am.GetBoxHit(metalBox), 1);
        }

        if (other.transform.CompareTag("box destruct"))
        {
            Destroy(gameObject);
        }
        else if (!(other.transform.CompareTag("Wall")) && !(other.transform.CompareTag("Player")))
        {
            if (other.gameObject.tag == "destructable")
            {
                if (velocityX1 < -18f || velocityX > 18f || velocityY > 30f || velocityY1 < -30f)
                {
                    if (other.gameObject.GetComponent<BoxCollision>().activeButton != null)
                    {
                        other.gameObject.GetComponent<BoxCollision>().activeButton.GetComponent<ButtonScript>().SubLink();
                    }
                    if (other.gameObject.GetComponent<BoxCollision>().activeGlue != null)
                    {
                        Destroy(other.gameObject.GetComponent<BoxCollision>().activeGlue);
                    }
                    Destroy(other.gameObject);
                    Instantiate(boxFragmentPrefab, other.transform.position, Quaternion.identity);
                    GetComponent<AudioSource>().PlayOneShot(am.GetBoxCrush(), 1);
                }
            }
        }
        else if (other.transform.CompareTag("Player") && !GetComponent<Glue>().isGlued())
        {
            if (velocityY > 23f || velocityX > 23f || velocityY1 < -23f || velocityX1 < -23f)
            {
                Camera.main.GetComponent<GravityWarp>().playerDead = true;
                GetComponent<AudioSource>().PlayOneShot(am.GetBoxCrush(), 1);
                other.gameObject.GetComponent<Animator>().SetBool("isCrushed", true);
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