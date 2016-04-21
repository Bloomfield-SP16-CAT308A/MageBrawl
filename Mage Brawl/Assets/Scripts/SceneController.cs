using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController {

    public static void Credits()
    {
        SceneManager.LoadScene("Credits");
        //Application.LoadLevel("Credits");
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public static void Instructions()
    {
        SceneManager.LoadScene("Instructions");
        //Application.LoadLevel("Instructions");
    }

    public static void LvlUp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Application.LoadLevel(Application.loadedLevel + 1);
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Start");
        //Application.LoadLevel("Start");
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void targetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
