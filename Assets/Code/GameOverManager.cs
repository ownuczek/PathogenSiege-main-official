using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel; // Panel z informacj� o przegranej

    private void Start()
    {
        gameOverPanel.SetActive(false); // Na pocz�tku ekran GameOver jest ukryty
    }

    // Metoda do pokazania ekranu Game Over
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true); // Pokazuje panel Game Over
    }

    // Funkcja do restartu gry
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // �aduje obecn� scen� (restartuje)
    }

    // Funkcja do przej�cia do menu g��wnego
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Zmieniamy "MainMenu" na nazw� sceny menu
    }
}
