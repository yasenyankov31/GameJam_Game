using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{

    public Transform firePoint;
    public GameObject amulet;
    public float AmuletSpeed = 10f;
    private void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float x = GetComponentInParent<PlayerLocomotion>().transform.localScale.x;
            GameObject amuletIns = Instantiate(amulet, firePoint.position, firePoint.rotation);
            amuletIns.GetComponent<Rigidbody2D>().AddForce(x * transform.right * AmuletSpeed);
        }
    }


}

