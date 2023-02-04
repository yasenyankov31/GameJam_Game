using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance;

        
    public GameObject cube;
    public float throwForce = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
    }
}

