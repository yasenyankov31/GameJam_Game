using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPlayer : MonoBehaviour
{
    private PlayerLocomotion player;
    private int index = 0;
    public bool canSwap = true;

    private void Start()
    {
        player = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSwap)
        {
            if (index==3)
            {
                index = 0;

            }
            switch (index)
            {
                case 0:
                    Debug.Log("Varvarin");
                    player.isBarbarian = true;
                    player.isMonkey = false;
                    player.isHuman = false;
                    break;
                case 1:
                    Debug.Log("Monkey");
                    player.isBarbarian = false;
                    player.isMonkey = true;
                    player.isHuman = false;
                    break;
                case 2:
                    Debug.Log("Human");
                    player.isBarbarian = false;
                    player.isMonkey = false;
                    player.isHuman = true;
                    break;

            }
            index++;
            StartCoroutine(SwapForms());

        }

    }
    IEnumerator SwapForms()
    {
        canSwap = false;

        yield return new WaitForSeconds(1);

        canSwap = true;
    }
}
