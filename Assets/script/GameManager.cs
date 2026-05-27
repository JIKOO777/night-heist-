using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Collecting")]
    [SerializeField] private int totalItemsToWin = 6;
    [SerializeField] private TMP_Text counterText;

    [Header("Timer")]
    [SerializeField] private float levelTime = 240f;
    [SerializeField] private TMP_Text timerText;

    [Header("UI Popups")]
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject gameOverPopup;

    [Header("Popup Delays")]
    [SerializeField] private float gameOverPopupDelay = 1.5f;
    [SerializeField] private float victoryPopupDelay = 1.5f;

    [Header("Scene Transition")]
    [SerializeField] private string nextLevelSceneName = "2_level";
    [SerializeField] private float nextLevelDelay = 3f;

    [Header("Main Menu")]
    [SerializeField] private int mainMenuBuildIndex = 0;

    private int collectedCount = 0;
    private bool ended = false;

    private float currentTime;
    private bool timerRunning = true;

    // 🔥 ПАМЯТЬ: был ли уже переход на 2_level
    private static bool hasAlreadyLoadedNextLevel = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        ended = false;

        collectedCount = 0;

        currentTime = levelTime;
        timerRunning = true;

        if (victoryPopup != null)
            victoryPopup.SetActive(false);

        if (gameOverPopup != null)
            gameOverPopup.SetActive(false);

        UpdateCounterUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (ended || !timerRunning) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;
            GameOver();
        }
    }

    // =========================
    // ITEM COLLECTION
    // =========================

    public void CollectOne()
    {
        if (ended) return;

        collectedCount++;
        UpdateCounterUI();

        if (collectedCount >= totalItemsToWin)
            Victory();
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
            counterText.text = $"{collectedCount}/{totalItemsToWin}";
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    // =========================
    // GAME OVER
    // =========================

    public void GameOver()
    {
        if (ended) return;

        ended = true;
        timerRunning = false;

        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(gameOverPopupDelay);

        Time.timeScale = 0f;

        if (gameOverPopup != null)
            gameOverPopup.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(nextLevelDelay);

        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuBuildIndex);
    }

    // =========================
    // VICTORY
    // =========================

    private void Victory()
    {
        if (ended) return;

        ended = true;
        timerRunning = false;

        Time.timeScale = 0f;

        StartCoroutine(VictorySequence());
    }

    private IEnumerator VictorySequence()
    {
        yield return new WaitForSecondsRealtime(victoryPopupDelay);

        if (victoryPopup != null)
            victoryPopup.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(nextLevelDelay);

        Time.timeScale = 1f;

        // 🔥 ЛОГИКА ПЕРЕХОДОВ
        if (!hasAlreadyLoadedNextLevel)
        {
            hasAlreadyLoadedNextLevel = true;
            SceneManager.LoadScene(nextLevelSceneName); // 2_level
        }
        else
        {
            SceneManager.LoadScene(mainMenuBuildIndex); // меню
        }
    }

    // =========================
    // BUTTONS
    // =========================

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuBuildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}