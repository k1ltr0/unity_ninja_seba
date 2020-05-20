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

<<<<<<< HEAD
    public virtual void Use()
    {
=======

    public virtual void Use() {


>>>>>>> Se agregan sprites y wea
        Inventory.instance.Use(this);
    }
}
