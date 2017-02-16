using UnityEngine;

public class BoxFragments : MonoBehaviour
{

    public float explodeForce = 200;

    Rigidbody2D[] fragments;

    void Start()
    {
        fragments = transform.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D fragment in fragments)
        {
            fragment.AddForce(new Vector2(Random.Range(-explodeForce, explodeForce), Random.Range(-explodeForce, explodeForce)));
        }
    }

}
