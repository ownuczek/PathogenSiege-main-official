using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f; // Pr�dko�� poruszania si� wroga

    private Transform target;
    private int pathIndex = 0;
    private bool attackingNexus = false; // Czy wirus atakuje Nexusa?

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogError("Target jest null! �cie�ka nie zosta�a poprawnie ustawiona.");
            return;
        }


        if (attackingNexus) return; // Wirus nie porusza si�, gdy atakuje Nexus

        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                // Dotarcie do Nexusa
                StartAttackingNexus();
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        if (attackingNexus) return; // Zatrzymaj ruch, je�li wirus atakuje Nexus

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed; // Zastosowanie zmiennej moveSpeed
    }

    private void StartAttackingNexus()
    {
        attackingNexus = true;
        rb.velocity = Vector2.zero; // Zatrzymaj ruch

        // Znajd� Nexus i zacznij go atakowa�
        Nexus nexus = FindObjectOfType<Nexus>();
        if (nexus != null)
        {
            nexus.StartVirusAttack(this);
        }
        else
        {
            Debug.LogError("Nexus nie znaleziony!");
            Destroy(gameObject); // Usu� wirusa, je�li Nexus nie istnieje
        }
    }

    public void StopAttacking()
    {
        // Wywo�ywane przez Nexus, gdy wirus przestaje atakowa� (np. ginie)
        Destroy(gameObject);
    }

    // Dodajemy metod�, aby zmienia� pr�dko�� wroga z zewn�trz
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        Debug.Log($"Pr�dko�� ustawiona na: {moveSpeed}");
    }

}