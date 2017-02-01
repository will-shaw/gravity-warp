using UnityEngine;

public class GlueObject : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[3];
    bool isStuck, once;
    string currGrav;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Beam")
        {
            if (other.gameObject.tag != "Wall")
            {
                other.GetComponent<Glue>().gluing();
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                isStuck = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                isStuck = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Wall" && other.gameObject.tag != "Beam")
        {
            Destroy(gameObject);
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
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    break;
                case "D":
                    transform.rotation = new Quaternion(0, 0, 180, 0);
                    break;
                case "L":
                    transform.rotation = new Quaternion(0, 0, 270, 0);
                    break;
                case "R":
                    transform.rotation = new Quaternion(0, 0, 90, 0);
                    break;
            }
        }
    }

}