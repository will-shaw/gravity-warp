using UnityEngine;

public class EndgameLever : MonoBehaviour
{

    bool once;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!once)
            {
                GetComponent<AudioSource>().Play();
                once = true;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

}
