using UnityEngine;

public class Door : MonoBehaviour
{

    int linkCount = 0;
    public bool open = false;
    public int linksRequired = 0;
    public bool opensUpward = false;
	public float openSpeed = 2;
    float origin;
	float offset = 3;

    void Start()
    {
        origin = gameObject.transform.position.y;
    }

    public void Active(int source)
    {
        if (source == 1)
        {
            linkCount++;
            UpdateState();
        }
        else
        {
            linkCount--;
            UpdateState();
        }
    }

    void UpdateState()
    {
        if (linkCount >= linksRequired)
        {
            open = true;
        }
        else
        {
            open = false;
        }
    }

    void Update()
    {
        //if (gameObject.transform.position.y == origin && open)
        //{
            float y = Mathf.MoveTowards(gameObject.transform.position.y, origin + offset, openSpeed * Time.deltaTime);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
        //}
        //else if (open)
       // {
        //    float y = Mathf.MoveTowards(gameObject.transform.position.y, origin, openSpeed * Time.deltaTime);
        //    gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
        //}
    }

}
