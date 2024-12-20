using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth = 100; // Maksymalne zdrowie Nexusa
    [SerializeField] private float damageInterval = 1f; // Czas miêdzy kolejnymi atakami wirusów
    [SerializeField] private int damagePerVirus = 10; // Iloœæ HP tracona przy jednym ataku wirusa

    private int currentHealth; // Aktualne zdrowie Nexusa
    private List<EnemyMovement> attackingViruses = new List<EnemyMovement>(); // Lista wirusów atakuj¹cych Nexus
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
        // SprawdŸ, czy Nexus ma jeszcze ¿ycie
        if (currentHealth <= 0)
        {
            DestroyNexus();
        }
    }

    // Metoda do rozpoczêcia ataku wirusa
    public void StartVirusAttack(EnemyMovement virus)
    {
        attackingViruses.Add(virus);
        // Rozpoczynamy coroutine, jeœli to pierwszy wirus
        if (attackingViruses.Count == 1 && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(HandleDamageOverTime());
        }
    }

    // Metoda do zakoñczenia ataku wirusa
    public void StopVirusAttack(EnemyMovement virus)
    {
        attackingViruses.Remove(virus);
        // Jeœli lista wirusów jest pusta, zatrzymujemy coroutine
        if (attackingViruses.Count == 0 && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // Coroutine do obs³ugi zadawania obra¿eñ przez wirusy
    private IEnumerator HandleDamageOverTime()
    {
        while (attackingViruses.Count > 0 && currentHealth > 0)
        {
            currentHealth -= damagePerVirus * attackingViruses.Count; // Obra¿enia od wszystkich wirusów
            Debug.Log($"Nexus HP: {currentHealth}");
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Metoda do zniszczenia Nexusa
    private void DestroyNexus()
    {
        Debug.Log("Nexus zosta³ zniszczony! Koniec gry.");

        // Wywo³ujemy ekran koñcowy
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }
}
