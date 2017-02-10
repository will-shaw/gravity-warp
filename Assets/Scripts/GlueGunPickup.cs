using UnityEngine;

public class GlueGunPickup : MonoBehaviour {

    public Sprite opened;
    public AudioClip clip;

    public GameObject menu;

    bool once;

    void OnTriggerEnter2D(Collider2D other)
	{
		if(!once && other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<GlueControl>().glueEnabled = true;			
            GetComponent<SpriteRenderer>().sprite = opened;
            GetComponent<AudioSource>().PlayOneShot(clip, 1);
            once = true;
            MenuHandler.hasGlue = true;
            menu.GetComponent<MenuHandler>().ShowGlueControls();
        }
	}
}
