using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakStone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BreakIt();

    }
    private void BreakIt()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float x = GetComponentInParent<PlayerLocomotion>().transform.localScale.x;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, x*Vector2.right);
            if (hit.collider.gameObject.tag=="Boulder")
            {
                hit.collider.gameObject.GetComponent<Destructable>().DestroyBoulder();
            }
        }
    }
}
