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
    [SerializeField] private float enemySpeed = 2f;
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

    private void Update()
    {
        if (!isSpawing) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
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

    private void EndWave()
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

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0]; // Mo�esz to rozszerzy� o losowanie wrog�w

        // Tworzymy instancj� wroga
        GameObject enemy = Instantiate(prefabToSpawn, LevelManager.main.StartPoint.position, Quaternion.identity);

        // Zak�adaj�c, �e skrypt ruchu wroga ma zmienn� speed, przypisujemy j�
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetSpeed(enemySpeed);  // Ustawiamy pr�dko�� wroga
        }
    }
}
