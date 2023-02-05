using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField]private HingeJoint2D parentJoint;
    [SerializeField] private Transform parent;
    [SerializeField] private int JumpSwingRange;
    private PlayerLocomotion player;

    private bool isSwinging;
    private float wallJumpingDirection;
    private bool dropBool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dropBool && parentJoint != null)
        {
            dropBool = false;
            parentJoint.breakForce = 1;
            StartCoroutine(ResetSwing());
        }
        if (isSwinging && Input.GetButtonDown("Jump")&& parentJoint!=null)
        {
            wallJumpingDirection = transform.localScale.x;
            float force = parent.GetChild(1).transform.eulerAngles.z;
            float jumpingPower = Mathf.Abs(Map(force, 201, 360, JumpSwingRange, JumpSwingRange));

            rb.AddForce(new Vector2(wallJumpingDirection * jumpingPower, jumpingPower/2),ForceMode2D.Impulse);
            parentJoint.breakForce = 1;
            StartCoroutine(ResetSwing());

            
        }

    }
    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GetComponentInParent<PlayerLocomotion>();
        if (collision.gameObject.tag == "Rope" && !isSwinging&&player.isMonkey)
        {
            isSwinging = true;
            HingeJoint2D hingeJoint = collision.gameObject.AddComponent<HingeJoint2D>();
            hingeJoint.connectedBody = rb;
            parentJoint = hingeJoint;
            parent = collision.gameObject.transform.parent;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        player = GetComponentInParent<PlayerLocomotion>();
        if (!player.isMonkey&&isSwinging)
        {
            dropBool = true;
        }

    }

    IEnumerator ResetSwing()
    {
        yield return new WaitForSeconds(2);

        isSwinging = false;
    }




}
