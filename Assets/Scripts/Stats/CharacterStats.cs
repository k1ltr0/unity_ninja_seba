using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currectHealth { get; private set;}
    public Stat damage;
    public Stat defense;
    public HealthBar health_bar;

    public EnemyCollision enemy_collision;

    void Start()
    {
        enemy_collision = this.GetComponent<EnemyCollision>();
    }

    private void Awake()
    {
        currectHealth = maxHealth;
        health_bar.SetMaxHealth(currectHealth);
    }

    public void TakeDamage(int damage) {
        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage,0,int.MaxValue);
        if (enemy_collision != null)
            currectHealth -= damage * enemy_collision.FindCollisions();
        else
            currectHealth -= damage;
        health_bar.SetHealth(currectHealth);

        if (currectHealth <= 0)
        {
            Die();
        }
    }

    public void TakeHealth(int health)
    {
        currectHealth += health;
        currectHealth = Mathf.Clamp(currectHealth,0, maxHealth);
        health_bar.SetHealth(currectHealth);
    }

    void Die() {
        this.gameObject.SetActive(false);
    }

    public int Attack() {
        return damage.GetValue();
    }

}
