using UnityEngine;

public class GlueObject : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[3];
    bool isStuck;
    string currGrav;
    bool expire = true;
    AudioClip splat;
    public float expireTimer = 15;

    public bool tutGlue =false;

    void Start() {
        splat = Camera.main.GetComponent<AudioManager>().GetGlueSplat();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Beam" && other.gameObject.tag != "Glue")
        {
            if (other.gameObject.tag != "Wall" && other.GetComponent<Glue>() != null)
            {
                if(other.tag == "destructable") {
                    other.GetComponent<BoxCollision>().setActiveGlue(gameObject);
                }
                other.GetComponent<Glue>().gluing(tutGlue);
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                expireTimer = 15f;
                isStuck = true;
            }
            else if (other.gameObject.tag == "Wall" || other.gameObject.GetComponent<BoxCollision>() != null)
            {
                GetComponent<AudioSource>().PlayOneShot(splat, 1);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                FlipStuckGlue();
                isStuck = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Wall" && other.gameObject.tag != "Beam" &&other.gameObject.tag != "Glue")
        {
            if(!tutGlue){
                Camera.main.GetComponent<GravityWarp>().glues.Remove(gameObject.transform); 
            }else{
            Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (GravityWarp.gravity != currGrav && !isStuck)
        {
            currGrav = GravityWarp.gravity;
            switch (currGrav)
            {
                case "U":
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case "D":
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case "L":
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case "R":
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
            }
        }
        if (expire && expireTimer < 0)
        {   
            if(!tutGlue){
                Camera.main.GetComponent<GravityWarp>().glues.Remove(gameObject.transform);
                Destroy(gameObject);
                Camera.main.GetComponent<CameraZoom>().player.GetComponent<GlueControl>().changeGlueCount(0);
            }else{
                Destroy(gameObject);
            }
            
        }
        else if (expire)
        {
            expireTimer -= Time.deltaTime;
        }
    }

    void FlipStuckGlue()
    {
        switch (currGrav)
        {
            case "U":
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case "D":
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case "L":
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case "R":
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
    
}