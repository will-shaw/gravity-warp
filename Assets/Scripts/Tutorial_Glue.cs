using UnityEngine;

public class Tutorial_Glue : MonoBehaviour
{

    public Transform spawner;
    public Transform gluePrefab;
    public float glueSpawnDelay;
    float glueSpawnTimer;

    void Update()
    {
        glueSpawnTimer -= Time.deltaTime;
        if (glueSpawnTimer <= 0)
        {
            Transform glue = Instantiate(gluePrefab, spawner.position, Quaternion.Euler(Vector2.up));
            //glue.GetComponent<SpriteRenderer>().flipY = true;
            Camera.main.GetComponent<GravityWarp>().tutGlues.Add(glue);
            glueSpawnTimer = glueSpawnDelay;
        }
    }
}
