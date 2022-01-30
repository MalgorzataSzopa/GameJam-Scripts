using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KladkaScript : MonoBehaviour
{
   
  
 

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.GetComponent<PlayerController>().monster){
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
        
    
}
