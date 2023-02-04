using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]

public class AIController : InputController
{
    public override bool RetieveJumpInput()
    {
        return true;
    }

    public override float RetieveMoveInput()
    {
        return 1f;
    }
}
