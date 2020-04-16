﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;



    public List<Item> items = new List<Item>();

    public Item _item;

    public CharacterStats player;
    public ParticleSystem _health_particle;


    private void Awake()
    {
        instance = this;
    }

    public delegate void OnIntemChanged();
    public OnIntemChanged onItemChangedCallback;  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Add(_item);
        }

        /*if (Input.GetKeyDown(KeyCode.U))
        {
            if (items.Count > 0)
            {
                Use(items[0]);
            }
        }*/
    }

    public void Add(Item item) {

        if (!item.isDefaultItem)
        {
            items.Add(item);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }


    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }


    public void Use(Item item) {

        player.TakeHealth(item.health);
        _health_particle.Play();
        //Remove(item);
    }
}
