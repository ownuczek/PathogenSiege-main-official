using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // U�ywamy TextMeshPro
using System.Collections;

public class GameManagement : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject levelCompletedPanel; // Panel z napisem "Level Completed"
    [SerializeField] private GameObject blockerPanel; // Panel blokuj�cy interakcje (czarny ekran)

    private TextMeshProUGUI levelCompletedText; // Komponent TextMeshProUGUI w panelu levelCompletedPanel

    private void Start()
    {
        blockerPanel.SetActive(false); // Na pocz�tku ukrywamy blockerPanel
        levelCompletedPanel.SetActive(false); // Na pocz�tku ukrywamy levelCompletedPanel

        // Pobieramy komponent TextMeshProUGUI z panelu levelCompletedPanel
        levelCompletedText = levelCompletedPanel.GetComponentInChildren<TextMeshProUGUI>();

        // Debugowanie - sprawd�, czy znaleziono komponent TextMeshProUGUI
        if (levelCompletedText == null)
        {
            Debug.LogError("Nie znaleziono komponentu TextMeshProUGUI w dzieciach levelCompletedPanel!");
        }
        else
        {
            Debug.Log("Komponent TextMeshProUGUI znaleziony: " + levelCompletedText.name);
        }
    }

    // Metoda do wy�wietlania komunikatu o zako�czeniu poziomu
    public void ShowLevelCompleted()
    {
        blockerPanel.SetActive(true); // Pokazuje czarny ekran
        levelCompletedPanel.SetActive(true); // Pokazuje panel z napisem "Level Completed"

        // Ustawienie tekstu w zale�no�ci od sytuacji
        if (levelCompletedText != null)
        {
            levelCompletedText.text = "Level Completed!"; // Ustawia tekst w panelu
        }

        StartCoroutine(LoadNextLevelAfterDelay(3f)); // Po 3 sekundach �aduje nast�pny poziom
    }

    // Metoda do wy�wietlania komunikatu o Game Over
    public void ShowGameOver()
    {
        blockerPanel.SetActive(true); // Pokazuje czarny ekran
        levelCompletedPanel.SetActive(true); // Pokazuje panel z napisem "Game Over"

        // Ustawienie tekstu w zale�no�ci od sytuacji
        if (levelCompletedText != null)
        {
            levelCompletedText.text = "Game Over!"; // Ustawia tekst w panelu
        }

        StartCoroutine(LoadMainMenuAfterDelay(3f)); // Po 3 sekundach �aduje g��wne menu
    }

    // Przej�cie do nast�pnego poziomu
    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czeka 3 sekundy

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Pobiera indeks obecnej sceny
        int nextSceneIndex = currentSceneIndex + 1; // Oblicza indeks nast�pnej sceny

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Sprawdza, czy istnieje nast�pna scena
        {
            SceneManager.LoadScene(nextSceneIndex); // �aduje nast�pn� scen�
        }
        else
        {
            Debug.Log("Brak nast�pnego poziomu!"); // Je�li nie ma nast�pnego poziomu
        }
    }

    // Powr�t do g��wnego menu po Game Over
    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czeka 3 sekundy
        SceneManager.LoadScene("MainMenu"); // �aduje scen� g��wnego menu (upewnij si�, �e masz odpowiedni� nazw� sceny)
    }
}
