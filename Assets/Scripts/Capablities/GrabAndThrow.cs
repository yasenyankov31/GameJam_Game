using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    public GameObject amulet;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    public Vector2 direction;

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;


        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }
    public void Shoot()
    {
        GameObject newAmulet = Instantiate(amulet, shotPoint.position, shotPoint.rotation);
        newAmulet.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

    }
    private Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized *launchForce* t ) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

}

