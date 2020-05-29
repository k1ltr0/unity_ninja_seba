using UnityEngine;
public enum TypeItem { Health, Energy}

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public TypeItem type;
    public int quantity = 0;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Inventory.instance.Use(this);
    }
}
