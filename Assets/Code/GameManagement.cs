using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManagement : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject levelCompletedPanel; // Panel z informacj¹ o ukoñczeniu poziomu
    [SerializeField] private Text levelCompletedText; // Tekst informuj¹cy o ukoñczeniu poziomu

    private void Start()
    {
        levelCompletedPanel.SetActive(false); // Na pocz¹tku ekran Level Completed jest ukryty
    }

    // Metoda wywo³ywana, gdy poziom zostaje ukoñczony
    public void ShowLevelCompleted()
    {
        levelCompletedPanel.SetActive(true); // Pokazujemy ekran Level Completed
        levelCompletedText.text = "Level Completed!"; // Wyœwietlamy tekst

        // Rozpoczynamy nowy poziom po 3 sekundach
        StartCoroutine(LoadNextLevelAfterDelay(3f)); // 3 sekundy opóŸnienia
    }

    // Korutyna, która po opóŸnieniu ³aduje nastêpny poziom
    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czekamy przez 3 sekundy

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Pobieramy indeks obecnej sceny
        int nextSceneIndex = currentSceneIndex + 1; // Indeks nastêpnej sceny

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Sprawdzamy, czy jest nastêpny poziom
        {
            SceneManager.LoadScene(nextSceneIndex); // £adujemy nastêpny poziom
        }
        else
        {
            Debug.Log("Nie ma nastêpnego poziomu!"); // Jeœli nie ma nastêpnego poziomu
        }
    }

    // Metoda wywo³ywana, gdy wrogowie zostan¹ pokonani
    public void LevelCompleted()
    {
        ShowLevelCompleted();
    }
}
