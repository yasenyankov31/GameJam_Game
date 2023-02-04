using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string continue_name;
    public string scene_name;

    public List<string> levels;

    public void ContinueGame()
    {
        SceneManager.LoadScene(continue_name);
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene(scene_name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
