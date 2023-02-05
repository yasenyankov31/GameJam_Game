using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public SpriteRenderer port;
    public BoxCollider2D boxCollider2D;
    public string NextlevelName;
    private bool isOpened;
    void Start()
    {
        isOpened = false;
        port.enabled = false;
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Amolet")
        {
            port.enabled = true;
            isOpened = true;
        }
        else if (collision.gameObject.tag == "Player" && isOpened)
        {
            SceneManager.LoadScene(NextlevelName);
        }

    }
}
