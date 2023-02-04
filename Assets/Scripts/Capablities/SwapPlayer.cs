using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPlayer : MonoBehaviour
{
    public SpriteRenderer player;
    public Sprite monkey;
    public Sprite barbarian;
    public Sprite human;
    private int index;
    private float delay = 1f;
    private float timePassed = 0;
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && timePassed>=delay)
        {
            if (index==0)
            {
                player.sprite = monkey;
                timePassed = 0f;
                index++;
            }
            else if (index==1)
            {
                player.sprite = barbarian;
                timePassed = 0f;
                index++;
            }
            else if (index ==2)
            {
                player.sprite = human;
                timePassed = 0f;
                index = 0;
            }
        }

        timePassed += Time.deltaTime;
    }
}
