using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDataRetriever : MonoBehaviour
{

    public Vector2 ContactNormal;
    public bool onGround;
    public bool onWall;
    private float friction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        friction = 0;
        onWall = false;
    }

    public void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactNormal = collision.GetContact(i).normal;
            onGround |= ContactNormal.y >= 0.9f;
            onWall = Mathf.Abs(ContactNormal.x) >= 0.9f;
        }
    
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;
        friction = 0;
        if (material!=null)
        {
            friction = material.friction;
        }
    }

    public bool GetOnGround()
    {
        return onGround;
    }
    public float GetFriction()
    {
        return friction;
    }

}
