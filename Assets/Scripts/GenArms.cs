using UnityEngine;

public class GenArms : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<GravityWarp>().playerDead = true;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetBoxCrush(), 1);
            other.gameObject.GetComponent<Animator>().SetBool("isCrushed", true);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetBoxHit(true));
        }
    }

}
