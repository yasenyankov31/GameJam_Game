using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance;


    public GameObject grabbedObject;
    public float throwForce = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (Input.GetKeyDown(KeyCode.Mouse1) && hitInfo.collider.gameObject.tag == "Amulet" && grabbedObject == null)
        {
            grabbedObject = hitInfo.collider.gameObject;
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            grabbedObject.transform.position = grabPoint.position;
            grabbedObject.transform.SetParent(transform);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody2D>().AddForce(grabPoint.right*transform.localScale.x * throwForce, ForceMode2D.Impulse);
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
    }
}

