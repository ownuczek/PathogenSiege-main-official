using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel; // Panel z informacj¹ o przegranej

    private void Start()
    {
        gameOverPanel.SetActive(false); // Na pocz¹tku ekran GameOver jest ukryty
    }

    // Metoda do pokazania ekranu Game Over
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true); // Pokazuje panel Game Over
    }

    // Funkcja do restartu gry
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // £aduje obecn¹ scenê (restartuje)
    }

    // Funkcja do przejœcia do menu g³ównego
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Zmieniamy "MainMenu" na nazwê sceny menu
    }
}
