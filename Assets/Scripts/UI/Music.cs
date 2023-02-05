using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource source;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            source.mute = !source.mute;
        }
    }
}
