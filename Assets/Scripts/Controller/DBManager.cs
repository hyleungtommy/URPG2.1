using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DBManager:MonoBehaviour
{
    public static DBManager Instance;
    [SerializeField] ItemTemplate[] itemTemplates;
    [SerializeField] WeaponTemplate[] weaponTemplates;
    [SerializeField] ArmorTemplate[] armorTemplates;
    [SerializeField] BuffTemplate[] buffTemplates;
    [SerializeField] QuestTemplate[] questTemplates;
    [SerializeField] ExploreSiteTemplate[] exploreSiteTemplates;
    [SerializeField] Sprite[] exploreSiteSprites;
    [SerializeField] CraftRecipeTemplate[] craftRecipeTemplates;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadTestItems();
            Game.QuestManager.Initialize(questTemplates.Select(q => new Quest(q)).ToList());
            Game.ExploreSiteList = exploreSiteTemplates.Select(e => new ExploreSite(e)).ToList();
        }
    }

    public void LoadTestItems(){
        /*
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(10), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(11), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(12), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(13), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(14), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(15), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(16), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(17), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(18), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(19), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(20), 10);
        GameController.Instance.Inventory.InsertItem(DBManager.Instance.GetItem(21), 10);
        */
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

    public BuffTemplate GetBuff(int buffId)
    {
        return buffTemplates[buffId];
    }

    public Sprite GetExploreSiteSprite(ExploreSiteType exploreSiteType)
    {
        return exploreSiteSprites[(int)exploreSiteType];
    }

    public List<CraftRecipe> GetAllCraftRecipes(){
        return craftRecipeTemplates.Select(c => new CraftRecipe(c)).ToList();
    }
    
    

    
}
