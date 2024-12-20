using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Metoda uruchamiaj¹ca grê (za³adowanie sceny gry)
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // U¿yj nazwy sceny gry
    }

    // Metoda zamykaj¹ca aplikacjê
    public void QuitGame()
    {
        Application.Quit();
    }
}
