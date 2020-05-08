using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public Item _item, _item_b;
    public PlayerController player;

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

        if (Input.GetKeyDown(KeyCode.O))
        {
            Add(_item_b);
        }
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
        Remove(item);
    }
}
