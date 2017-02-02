using UnityEngine;

public class Emit : MonoBehaviour
{
    bool isActive = true;

    public void toggle()
    {
        if (!isActive)
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
        isActive = !isActive;
    }

}
