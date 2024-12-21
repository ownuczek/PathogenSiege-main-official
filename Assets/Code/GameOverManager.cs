using UnityEngine;  // Upewnij si�, �e ta linia jest dodana
using TMPro;  // Dodaj przestrze� nazw TextMeshPro, je�li u�ywasz TextMeshPro
using UnityEngine.SceneManagement;  // Dodaj przestrze� nazw dla SceneManager


public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel; // Panel Game Over
    [SerializeField] private GameObject blockerPanel; // Panel blokuj�cy interakcje
    [SerializeField] private TextMeshProUGUI gameOverText; // Komponent TextMeshProUGUI

    private void Start()
    {
        gameOverPanel.SetActive(false); // Ukrywamy ekran Game Over
        blockerPanel.SetActive(false); // Ukrywamy panel blokuj�cy interakcje
    }

    // Metoda do pokazania ekranu Game Over
    public void ShowGameOver()
    {
        blockerPanel.SetActive(true); // Pokazujemy czarny ekran blokuj�cy
        gameOverPanel.SetActive(true); // Pokazujemy ekran Game Over

        // Ustawienie tekstu "Game Over" w przypadku przegranej
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!"; // Ustawiamy komunikat
        }
    }

    // Funkcja do restartu gry
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // �aduje obecn� scen� (restartuje)
    }

    // Funkcja do przej�cia do menu g��wnego
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Zmieniamy "MainMenu" na nazw� sceny menu
    }
}
