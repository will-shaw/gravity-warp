using UnityEngine;

public class FacilityIntroLevelController : MonoBehaviour
{

    public Transform[] doors = new Transform[2];

    public Transform[] lights = new Transform[4];

    void Update()
    {
        if (doors[0].position.y > -4f)
        {
            lights[0].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            lights[1].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            lights[0].GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            lights[1].GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        }
        if (doors[1].position.y > -4f)
        {
            lights[2].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            lights[3].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            lights[2].GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            lights[3].GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        }
    }

}
