using UnityEngine;

public class GlueObject : MonoBehaviour
{

    public Sprite[] sprites = new Sprite[3];

	bool stuck, once;

    string currGrav;

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

    void Update()
    {
        if (GravityWarp.gravity != currGrav && !stuck)
        {
			currGrav = GravityWarp.gravity;
            if (GravityWarp.gravity == "U")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 0));
            }
            else if (GravityWarp.gravity == "R")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (GravityWarp.gravity == "D")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 180));
            }
            else if (GravityWarp.gravity == "L")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 270));
            }
        }
    }

}