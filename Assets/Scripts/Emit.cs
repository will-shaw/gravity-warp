using UnityEngine;

public class Emit : MonoBehaviour
{
    bool isActive = true;

    public int links=1; 
    public bool startStoped;
   
   public int noLinked;
   
    void Start()
    {
        if(startStoped){
             gameObject.GetComponent<ParticleSystem>().Stop();
             isActive = false;
        }
    }
    public void toggle(int linked)
    {
        if(linked > 0){
            noLinked++;
        }else{
            noLinked--;
        }
        
        if(startStoped){
            if (noLinked >= links){
                gameObject.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                gameObject.GetComponent<ParticleSystem>().Stop();
                
            }
            isActive = !isActive;

        }else{
            if (noLinked >= links){
            
                gameObject.GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                
            }
            isActive = !isActive;
        }
    }

}
