using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float currectHealth { get; private set;}
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

        float _current = currectHealth;

        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage,0,int.MaxValue);
        if (enemy_collision != null)
            currectHealth -= damage * enemy_collision.FindCollisions();
        else
            currectHealth -= damage;
        //health_bar.SetHealth(currectHealth);


        SetLifeBar(_current, currectHealth);


        if (currectHealth <= 0)
        {
            Die();
            return;
        }



    }

    public void TakeHealth(int health)
    {
        float _current = currectHealth;

        currectHealth += health;
        currectHealth = Mathf.Clamp(currectHealth,0, maxHealth);

        SetLifeBar(_current, currectHealth);

        //health_bar.SetHealth(currectHealth);
    }

    void Die() {
        this.gameObject.SetActive(false);
    }

    public int Attack() {
        return damage.GetValue();
    }


    void SetLifeBar(float _from , float _to) {


        iTween.ValueTo(gameObject, iTween.Hash(
         "from", _from,
         "to", _to,
         "time", .5f,
         "delay", .5f,
         "onupdatetarget", gameObject,
         "onupdate", "UpdateLifeBar",
         "easetype", iTween.EaseType.easeOutSine
         )
        );

    }


    void UpdateLifeBar(float newValue)
    {
        health_bar.SetHealth(newValue);
    }

}
