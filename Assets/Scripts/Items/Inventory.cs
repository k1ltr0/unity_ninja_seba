using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>();

    public Item _item;

    public CharacterStats player;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Add(_item);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (items.Count > 0)
            {
                Use(items[0]);
            }
        }
    }

    public void Add(Item item) {

        if (!item.isDefaultItem)
        {
            items.Add(item);
        }
    }


    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void Use(Item item) {
        player.TakeHealth(item.health);
        Remove(item);
    }
}
