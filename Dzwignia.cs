using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dzwignia : MonoBehaviour
{

    public GameObject door;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= gameObject.GetComponent<Animator>();

    }
    // Update is called once per frame
    
        private void OnCollisionEnter2D(Collision2D other) {
        
        if(!other.gameObject.GetComponent<PlayerController>().monster){
            animator.SetBool("dzwignia",true);
            Destroy(door);
        }
    }
}
