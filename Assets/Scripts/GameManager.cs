using UnityEngine;
using UnityEngine.SceneManagement;
 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
 
    [SerializeField] private GameObject gameOverPanel; 
 
    public bool IsGameOver { get; private set; }
 
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }
 
    public void GameOver()
    {
        if (IsGameOver) return;
 
        IsGameOver = true;
        Time.timeScale = 0f; // stops the game
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Debug.Log("GAME OVER");
    }
 
    private void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
 