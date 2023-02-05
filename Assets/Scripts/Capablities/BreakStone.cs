using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakStone : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        BreakIt();

    }
    private void BreakIt()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            
            PlayerLocomotion player = GetComponentInParent<PlayerLocomotion>();
            float x = GetComponentInParent<PlayerLocomotion>().transform.localScale.x;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, x*Vector2.right);
            if (hit.collider.gameObject.tag=="Boulder"&& player.isBarbarian)
            {
                player.playerAnimator.SetBool("isPunching", true);
                hit.collider.gameObject.GetComponent<Destructable>().DestroyBoulder();
                player.playerAnimator.SetBool("isPunching", false);
            }
        }
    }
}
