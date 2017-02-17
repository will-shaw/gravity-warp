using UnityEngine;

public class Glue : MonoBehaviour
{
    bool glued = false;
    float gluedTime;
    public float glueTime;

    void Update()
    {
        if (glued && gluedTime > 0)
        {
            gluedTime -= Time.deltaTime;
        }
        else if (glued)
        {
            glued = false;
            if (GetComponent<Player>() != null)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            } else {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;                
            }
        }
    }

    public void gluing(bool tutGlue)
    {
        glued = true;
        gluedTime = glueTime;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if(!tutGlue){
           Camera.main.GetComponent<CameraZoom>().player.GetComponent<GlueControl>().changeGlueCount(0);
        }
        if (transform.tag == "Player") {
            GetComponent<Animator>().SetFloat("Speed", -1);
        }
    }

    public bool isGlued()
    {
        return glued;
    }
    
}