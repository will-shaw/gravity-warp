using UnityEngine;

public class GlueControl : MonoBehaviour
{
    public Transform gluePrefab;
    public Transform noPlacePrefab;
    public int glueLimit;
    public int glueCount;
    public float spawnRange;   
    public bool glueEnabled;
    Transform cantGlue;
    public bool hasGun;

    void Update()
    {
        if (glueEnabled) {
            if (!hasGun)
            {
                hasGun = true;
                //GetComponent<Animator>().SetBool("hasGun", hasGun);
            }
        }
        else if (hasGun)
        {
            hasGun = false;
            //GetComponent<Animator>().SetBool("hasGun", hasGun);
        }

        float distance = Vector2.Distance(transform.position, ValidTarget());
        if (Input.GetKeyDown(InputManager.glue) && spawnRange >= distance && glueEnabled)
        {
            if (glueCount < glueLimit)
            {
                Transform glueNew;
                glueNew = Instantiate(gluePrefab, ValidTarget(), Quaternion.identity);
                glueCount++;
                Camera.main.GetComponent<GravityWarp>().glues.Add(glueNew);
            }else if(glueCount >= glueLimit){
                Camera.main.GetComponent<GravityWarp>().glueExtraPlace();
                Transform glueNew;
                glueNew = Instantiate(gluePrefab, ValidTarget(), Quaternion.identity);
                glueCount++;
                Camera.main.GetComponent<GravityWarp>().glues.Add(glueNew);
            }
        } else if ( Input.GetKeyDown(InputManager.glue) && spawnRange < distance) {
            cantGlue = Instantiate(noPlacePrefab, ValidTarget(), Quaternion.identity);
        }
        if (Input.GetKeyUp(InputManager.glue) && cantGlue != null) {
            Destroy (cantGlue.gameObject);
        }
    }

    Vector3 ValidTarget()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, target);
        Debug.DrawLine(transform.position, target, Color.red, 10);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && (hit.collider.tag == "Wall" || hit.collider.gameObject.GetComponent<Glue>() != null))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green, 10);
                return hit.point;
            }
        }
        return target;
    }

    public void changeGlueCount(int i)
    {
        if (i == 0)
        {
            glueCount--;
        }
        else
        {
            glueCount++;
        }
    }
}
