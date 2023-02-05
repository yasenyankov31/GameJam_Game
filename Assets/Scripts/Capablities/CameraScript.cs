using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float zPos = -14.51f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector3(player.transform.position.x, player.transform.position.y, zPos) ;

       
    }
}
