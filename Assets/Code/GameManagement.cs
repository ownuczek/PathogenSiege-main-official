using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManagement : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject levelCompletedPanel; // Panel z informacj� o uko�czeniu poziomu
    [SerializeField] private Text levelCompletedText; // Tekst informuj�cy o uko�czeniu poziomu

    private void Start()
    {
        levelCompletedPanel.SetActive(false); // Na pocz�tku ekran Level Completed jest ukryty
    }

    // Metoda wywo�ywana, gdy poziom zostaje uko�czony
    public void ShowLevelCompleted()
    {
        levelCompletedPanel.SetActive(true); // Pokazujemy ekran Level Completed
        levelCompletedText.text = "Level Completed!"; // Wy�wietlamy tekst

        // Rozpoczynamy nowy poziom po 3 sekundach
        StartCoroutine(LoadNextLevelAfterDelay(3f)); // 3 sekundy op�nienia
    }

    // Korutyna, kt�ra po op�nieniu �aduje nast�pny poziom
    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czekamy przez 3 sekundy

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Pobieramy indeks obecnej sceny
        int nextSceneIndex = currentSceneIndex + 1; // Indeks nast�pnej sceny

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Sprawdzamy, czy jest nast�pny poziom
        {
            SceneManager.LoadScene(nextSceneIndex); // �adujemy nast�pny poziom
        }
        else
        {
            Debug.Log("Nie ma nast�pnego poziomu!"); // Je�li nie ma nast�pnego poziomu
        }
    }

    // Metoda wywo�ywana, gdy wrogowie zostan� pokonani
    public void LevelCompleted()
    {
        ShowLevelCompleted();
    }
}
