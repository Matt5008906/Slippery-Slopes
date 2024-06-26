using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1"); // Replace "Level1" with your actual scene name
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2"); // Replace "Level2" with your actual scene name
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3"); // Replace "Level3" with your actual scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); 
    }
}