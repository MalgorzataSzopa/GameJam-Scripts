using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public Animator animator;
    public GameObject player;


    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.GetComponent<PlayerController>().monster){
            animator.SetBool("Crate",true);
            other.gameObject.GetComponent<PlayerController>().speed = 2f;
            
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator= gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
