using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Metoda uruchamiaj�ca gr� (za�adowanie sceny gry)
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // U�yj nazwy sceny gry
    }

    // Metoda zamykaj�ca aplikacj�
    public void QuitGame()
    {
        Application.Quit();
    }
}
