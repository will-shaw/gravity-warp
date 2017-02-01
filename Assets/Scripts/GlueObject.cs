using UnityEngine;

public class GlueObject : MonoBehaviour
{

    public Sprite[] sprites = new Sprite[3];

	bool stuck, once;

    string currGrav;

    GravityWarp gravitywarp;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Wall")
        {
            other.GetComponent<Glue>().gluing();
            GetComponent<SpriteRenderer>().sprite = sprites[2];
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			stuck = true;			
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<SpriteRenderer>().sprite = sprites[1];
			stuck = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Wall")
        {
            Destroy(gameObject);
        }
    }

    void start(){
        gravitywarp = Camera.main.GetComponent<GravityWarp>();
    }
    void Update()
    {
        if (gravitywarp.gravity != currGrav && !stuck)
        {
			currGrav = gravitywarp.gravity;
            if (gravitywarp.gravity == "U")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 0));
            }
            else if (gravitywarp.gravity == "R")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (gravitywarp.gravity == "D")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 180));
            }
            else if (gravitywarp.gravity == "L")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 270));
            }
        }
    }

}