﻿using UnityEngine;

public class GlueControl : MonoBehaviour
{
    public Transform gluePrefab;
    public Transform noPlacePrefab;
    public int glueLimit;
    public int glueCount;
    public float spawnRange;   
    public bool glueEnabled;
    Transform cantGlue;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(1) && spawnRange >= distance && glueEnabled)
        {
            GravityWarp gw = Camera.main.GetComponent<GravityWarp>();
            if (glueCount < glueLimit)
            {
                Transform glueNew;
                glueNew = Instantiate(gluePrefab, VaildateTarget(), Quaternion.identity);
                glueCount++;
                gw.glues.Add(glueNew);
            }
        } else if ( Input.GetMouseButtonDown(1) && spawnRange < distance) {
            cantGlue = Instantiate(noPlacePrefab, VaildateTarget(), Quaternion.identity);
        }
        if (Input.GetMouseButtonUp(1) && cantGlue != null) {
            Destroy (cantGlue.gameObject);
        }
    }

    Vector3 VaildateTarget()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = -9;
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
