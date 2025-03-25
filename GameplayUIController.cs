using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUIScript : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
