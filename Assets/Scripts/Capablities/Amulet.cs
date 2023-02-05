using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulet : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (hasHit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.tag=="Player")
        {
            if (player.GetComponent<PlayerLocomotion>().isHuman)
            {
                player.GetComponentInChildren<GrabAndThrow>().hasAmulet = true;
                player.GetComponentInChildren<GrabAndThrow>().sprite.enabled = true;
                Destroy(this.gameObject);
            }

        }
    }
}
