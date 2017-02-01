﻿using UnityEngine;

public class GlueObject : MonoBehaviour
{

    public Sprite[] sprites = new Sprite[3];

	bool stuck, once;

    string currGrav;

    GravityWarp gravitywarp;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Beam"){
            if (other.gameObject.tag != "Wall" )
            {
                other.GetComponent<Glue>().gluing();
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                stuck = true;			
            }
            else
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                stuck = true;
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

    void start(){
        gravitywarp = Camera.main.GetComponent<GravityWarp>();
    }
    void Update()
    {
        if (Camera.main.GetComponent<GravityWarp>().gravity != currGrav && !stuck)
        {
			currGrav = Camera.main.GetComponent<GravityWarp>().gravity;
            if (Camera.main.GetComponent<GravityWarp>().gravity == "U")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 0));
            }
            else if (Camera.main.GetComponent<GravityWarp>().gravity == "R")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (Camera.main.GetComponent<GravityWarp>().gravity == "D")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 180));
            }
            else if (Camera.main.GetComponent<GravityWarp>().gravity == "L")
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 270));
            }
        }
    }

}