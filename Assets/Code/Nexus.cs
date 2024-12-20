using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth = 100; // Maksymalne zdrowie Nexusa
    [SerializeField] private float damageInterval = 1f; // Czas mi�dzy kolejnymi atakami wirus�w
    [SerializeField] private int damagePerVirus = 10; // Ilo�� HP tracona przy jednym ataku wirusa

    private int currentHealth; // Aktualne zdrowie Nexusa
    private List<EnemyMovement> attackingViruses = new List<EnemyMovement>(); // Lista wirus�w atakuj�cych Nexus
    private GameOverManager gameOverManager;
    private Coroutine damageCoroutine; // Do przechowywania referencji do coroutiny

    private void Start()
    {
        currentHealth = maxHealth;
        gameOverManager = FindObjectOfType<GameOverManager>(); // Szukamy obiektu GameOverManager

        // Sprawdzamy, czy GameOverManager istnieje w scenie
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager not found in the scene!");
        }
    }

    private void Update()
    {
        // Sprawd�, czy Nexus ma jeszcze �ycie
        if (currentHealth <= 0)
        {
            DestroyNexus();
        }
    }

    // Metoda do rozpocz�cia ataku wirusa
    public void StartVirusAttack(EnemyMovement virus)
    {
        attackingViruses.Add(virus);
        // Rozpoczynamy coroutine, je�li to pierwszy wirus
        if (attackingViruses.Count == 1 && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(HandleDamageOverTime());
        }
    }

    // Metoda do zako�czenia ataku wirusa
    public void StopVirusAttack(EnemyMovement virus)
    {
        attackingViruses.Remove(virus);
        // Je�li lista wirus�w jest pusta, zatrzymujemy coroutine
        if (attackingViruses.Count == 0 && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // Coroutine do obs�ugi zadawania obra�e� przez wirusy
    private IEnumerator HandleDamageOverTime()
    {
        while (attackingViruses.Count > 0 && currentHealth > 0)
        {
            currentHealth -= damagePerVirus * attackingViruses.Count; // Obra�enia od wszystkich wirus�w
            Debug.Log($"Nexus HP: {currentHealth}");
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Metoda do zniszczenia Nexusa
    private void DestroyNexus()
    {
        Debug.Log("Nexus zosta� zniszczony! Koniec gry.");

        // Wywo�ujemy ekran ko�cowy
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }
}
