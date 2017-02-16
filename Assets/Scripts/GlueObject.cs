using UnityEngine;

public class GlueObject : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[3];
    bool isStuck;
    string currGrav;
    bool expire = true;
    AudioClip splat;
    public float expireTimer = 15;

    public float playerImmunity = 5F;
    public float playerImmunityTimer;

    public bool tutGlue =false;

    void Start() {
        splat = Camera.main.GetComponent<AudioManager>().GetGlueSplat();
        SetInitalVelocity();
        playerImmunityTimer = 0;
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
                if(other.gameObject.tag == "Player" && playerImmunityTimer < playerImmunity) {
                    return;
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
            }
            if(playerImmunityTimer < playerImmunity)
            {
                return;
            }
            Destroy(gameObject);
            
        }
    }

    void Update()
    {
        if (/*GravityWarp.gravity != currGrav && */!isStuck)
        {
            /*currGrav = GravityWarp.gravity;
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
            }*/
            //float angle = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 angleFromVelocity = GetComponent<Rigidbody2D>().velocity;
            float currentX = angleFromVelocity.x;
            float currentY = angleFromVelocity.y;
            float currentZ = angleFromVelocity.z;
            switch(GravityWarp.gravity) {
                case "D" :
                    transform.right = new Vector3(currentX+90F,currentY,currentZ);  
                    break;
                case "U" :
                    transform.right = new Vector3(currentX-90F,currentY,currentZ);
                    break;
                case "L" :
                    transform.right = new Vector3(currentX,currentY-90F,currentZ);
                    break;
                case "R" :
                    transform.right = new Vector3(currentX,currentY+90F,currentZ);
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
        if(playerImmunityTimer < playerImmunity) {
            playerImmunityTimer += Time.deltaTime;
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

    void SetInitalVelocity()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector3 direction = worldMousePosition - Camera.main.GetComponent<CameraZoom>().player.transform.position;
        direction.Normalize();
        direction *= 70;
        GetComponent<Rigidbody2D>().velocity = direction;
        Debug.Log(GetComponent<Rigidbody2D>().velocity);
    }
    
}