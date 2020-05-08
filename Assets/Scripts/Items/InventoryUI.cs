using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void Open()
    {
        itemsParent.gameObject.SetActive(!itemsParent.gameObject.activeSelf);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
