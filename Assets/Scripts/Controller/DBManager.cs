using UnityEngine;

public class DBManager:MonoBehaviour
{
    public static DBManager Instance;
    [SerializeField] ItemTemplate[] itemTemplates;
    [SerializeField] WeaponTemplate[] weaponTemplates;
    [SerializeField] ArmorTemplate[] armorTemplates;

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

    public WeaponTemplate[] GetAllWeapons()
    {
        return weaponTemplates;
    }

    public ArmorTemplate[] GetAllArmors()
    {
        return armorTemplates;
    }

    public ArmorTemplate GetArmor(int armorId)
    {
        return armorTemplates[armorId];
    }

    public WeaponTemplate GetWeapon(int weaponId)
    {
        return weaponTemplates[weaponId];
    }
    
    

    
}
