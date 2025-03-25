using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    [SerializeField]
    private GameObject[] characters;

    private GameObject gameOverPanel;
    private GameObject winPanel;

    private bool gameEnded = false; // ✅ Prevent multiple triggers

    private int _charIndex;
    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay")
        {
            Instantiate(characters[CharIndex]);

            GameObject canvas = GameObject.Find("Canvas");

            if (canvas != null)
            {
                gameOverPanel = canvas.transform.Find("GameOverPanel")?.gameObject;
                winPanel = canvas.transform.Find("WinPanel")?.gameObject;
            }

            // ✅ Ensure both panels are off at start
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            else Debug.LogError("GameOverPanel not found in the Gameplay scene!");

            if (winPanel != null) winPanel.SetActive(false); // ❌ Fixes win panel showing at start
            else Debug.LogError("WinPanel not found in the Gameplay scene!");

            gameEnded = false; // ✅ Reset game state
            Time.timeScale = 1f; // Ensure time runs when restarting
        }
    }

    public void ShowGameOverScreen()
    {
        if (!gameEnded) // ✅ Prevent multiple triggers
        {
            gameEnded = true;
            FindUIElements();

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogError("GameOverPanel is still missing!");
            }
        }
    }

    public void ShowWinScreen()
    {
        if (!gameEnded) // ✅ Prevent multiple triggers
        {
            gameEnded = true;
            FindUIElements();

            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogError("WinPanel is still missing!");
            }
        }
    }



    private void FindUIElements()
    {
        if (gameOverPanel == null || winPanel == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                gameOverPanel = gameOverPanel ?? canvas.transform.Find("GameOverPanel")?.gameObject;
                winPanel = winPanel ?? canvas.transform.Find("WinPanel")?.gameObject;
            }
        }
    }
}
