using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPlayer : MonoBehaviour
{
    private PlayerLocomotion player;
    public Animator [] animators;
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
                    animators[0].gameObject.SetActive(true);
                    animators[1].gameObject.SetActive(false);
                    animators[2].gameObject.SetActive(false);
                    player.playerAnimator = animators[0];
                    break;
                case 1:
                    Debug.Log("Monkey");
                    player.isBarbarian = false;
                    player.isMonkey = true;
                    player.isHuman = false;
                    animators[0].gameObject.SetActive(false);
                    animators[1].gameObject.SetActive(true);
                    animators[2].gameObject.SetActive(false);
                    player.playerAnimator = animators[1];
                    break;
                case 2:
                    Debug.Log("Human");
                    player.isBarbarian = false;
                    player.isMonkey = false;
                    player.isHuman = true;
                    animators[0].gameObject.SetActive(false);
                    animators[1].gameObject.SetActive(false);
                    animators[2].gameObject.SetActive(true);
                    player.playerAnimator = animators[2];
                    break;
    
            }
            
            index++;
            StartCoroutine(SwapForms());

        }

    }
    IEnumerator SwapForms()
    {
        canSwap = false;

        yield return new WaitForSeconds(0.5f);

        canSwap = true;
    }
}
