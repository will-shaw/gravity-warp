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
            glueSpawnTimer = glueSpawnDelay;
            glue.GetComponent<GlueObject>().tutGlue = true;
        }
    }
}
