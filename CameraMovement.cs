using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player; 
    public GameObject Beast; 
    private Vector3 offset = new Vector3(0,1,-3);    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
    transform.position = player.transform.position + offset;
        
    }
}
