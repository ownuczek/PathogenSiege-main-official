using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    public GameManagement gameManagement; // Referencja do GameManagement

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    // Edytowalne w Inspektorze
    [SerializeField] private float baseEnemySpeed = 2f; // Domy�lna pr�dko�� wrog�w
    [SerializeField] private int totalWaves = 10; // Ca�kowita liczba fal

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawing = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    public void Update()
    {
        if (!isSpawing) return;

        timeSinceLastSpawn += Time.deltaTime;

        // Obliczanie pr�dko�ci wrog�w w tej fali
        float enemySpeed = baseEnemySpeed + (currentWave - 1) * 0.5f;
        Debug.Log($"Pr�dko�� wrog�w w tej fali: {enemySpeed}"); // Debugowanie pr�dko�ci

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy(enemySpeed);  // Przekazanie pr�dko�ci do wroga
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        // Sprawdzenie, czy koniec fali
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawing = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    public void EndWave()
    {
        isSpawing = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        if (currentWave > totalWaves) // Zako�czone wszystkie fale
        {
            if (gameManagement != null)
            {
                gameManagement.ShowLevelCompleted(); // Wywo�anie zako�czenia poziomu
            }
            else
            {
                Debug.LogError("GameManagement jest null w EnemySpawner! Przypisz obiekt w inspektorze.");
            }
        }
        else
        {
            StartCoroutine(StartWave()); // Przej�cie do kolejnej fali
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)); // Liczba wrog�w w danej fali
    }

    private void SpawnEnemy(float enemySpeed)
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        GameObject enemy = Instantiate(prefabToSpawn, LevelManager.main.StartPoint.position, Quaternion.identity);

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetSpeed(enemySpeed); // Przekazywanie pr�dko�ci do przeciwnika
        }
        else
        {
            Debug.LogError("Brak komponentu EnemyMovement na prefabrykacie wroga!");
        }
    }
}
