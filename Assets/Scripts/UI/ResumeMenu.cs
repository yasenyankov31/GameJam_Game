using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeMenu : MonoBehaviour
{
    public string backToMenu;
    public GameObject CanvasResume;
    private bool isStopped;

    private void Start()
    {
        isStopped = false;
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(backToMenu);
    }

    public void ResumeWithEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isStopped)
        {
            CanvasResume.SetActive(true);
            Time.timeScale = 0;
            isStopped = true;
        }
        else if (Input.GetKey(KeyCode.Escape) && isStopped)
        {
            CanvasResume.SetActive(false);
            Time.timeScale = 1;
            isStopped = false;
        }
    }

    public void ResumeButton()
    {
        CanvasResume.SetActive(false);
        isStopped = false;
    }
}
