using UnityEngine;
using UnityEngine.UI;

public class Glue : MonoBehaviour
{
    bool glued;
    float gluedTime;
    public float glueTime;
    public Transform canvas;

    void Start()
    {
        glued = false;
        if (gameObject.GetComponent<Player>() != null)
        {
            canvas = gameObject.GetComponent<Player>().canvas;
        }
    }

    void Update()
    {
        if (glued && gluedTime > 0)
        {
            gluedTime -= Time.deltaTime;
            if (GetComponent<Player>() != null)
            {
                canvas.GetChild(3).GetComponent<Text>().text = string.Format("{0:0}", gluedTime);
            }
        }
        else if (glued)
        {
            glued = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void gluing()
    {
        glued = true;
        gluedTime = glueTime;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Camera.main.GetComponent<CameraZoom>().player.GetComponent<GlueControl>().changeGlueCount(0);
        if (transform.tag == "Player") {
            GetComponent<Animator>().SetFloat("Speed", -1);
        }
    }

    public bool isGlued()
    {
        return glued;
    }
    
}