using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public CharacterStats stats;


    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {

        stats.TakeDamage(damage);
    }


    public void TakeHealth(int health)
    {
     
        //stats.TakeHealth(health);
    }
}
