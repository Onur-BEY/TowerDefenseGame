using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float startSpeed = 10f;
    
    [HideInInspector] // Denetçide gizler çünkü publictir ve deðiþtirilmesini istemiyoruz.
    public float speed;
    
    public float startHealth=100;
    private float health;
    public int worth=50;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed=startSpeed;
        health=startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1 - pct);
    } 

    void Die()
    {
        isDead = true;
        PlayerStats.Money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect,5f);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
