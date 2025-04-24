using UnityEngine;

public class DBManager:MonoBehaviour
{
    public static DBManager Instance;
    [SerializeField] ItemTemplate[] itemTemplates;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public ItemTemplate[] GetAllItems()
    {
        return itemTemplates;
    }

    public Item GetItem(int itemId)
    {
        return itemTemplates[itemId].GetItem();
    }
}
