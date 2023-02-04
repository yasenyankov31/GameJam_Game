using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

}
