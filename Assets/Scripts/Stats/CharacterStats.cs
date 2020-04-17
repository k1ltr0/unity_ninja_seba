using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currectHealth { get; private set;}


    public Stat damage;
    public Stat defense;

    public HealthBar health_bar;


    private void Awake()
    {
        currectHealth = maxHealth;
        health_bar.SetMaxHealth(currectHealth);
    }



    public void TakeDamage(int damage) {

        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage,0,int.MaxValue);
        currectHealth -= damage;
        health_bar.SetHealth(currectHealth);

        //Debug.Log(transform.name +" take "+ damage +" damage");

    }

    public void TakeHealth(int health)
    {

        currectHealth += health;
        currectHealth = Mathf.Clamp(currectHealth,0, maxHealth);
        health_bar.SetHealth(currectHealth);
    }

    public int Attack() {

        return damage.GetValue();
    }

}
