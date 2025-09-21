using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;

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
    [SerializeField] ReinforceDataTemplate[] reinforceDataTemplates;
    [SerializeField] EnchantmentTemplate[] enchantmentTemplates;
    [SerializeField] EnchantmentDataTemplate[] enchantmentDataTemplates;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadTestItems();
            AddTestEquipment();
            Game.QuestManager.Initialize(questTemplates.Select(q => new Quest(q)).ToList());
            Game.ExploreSiteList = exploreSiteTemplates.Select(e => new ExploreSite(e)).ToList();
        }
    }

    public void LoadTestItems(){
        Game.Inventory.InsertItem(GetItem(26), 99);
        Game.Inventory.InsertItem(GetItem(27), 99);
        Game.Inventory.InsertItem(GetItem(36), 99);
        Game.Inventory.InsertItem(GetItem(37), 99);
        Game.Inventory.InsertItem(GetItem(55), 99);
        Game.Inventory.InsertItem(GetItem(56), 99);
        Game.Inventory.InsertItem(GetItem(70), 99);
        Game.Inventory.InsertItem(GetItem(71), 99);
        Game.Inventory.InsertItem(GetItem(76), 99);
        Game.Inventory.InsertItem(GetItem(77), 99);
    }

        public void AddTestEquipment(){

        Weapon testSword = new Weapon(GetWeapon(0));
        Game.Inventory.InsertItem(testSword, 1);
        Weapon testShield = new Weapon(GetWeapon(1));
        Game.Inventory.InsertItem(testShield, 1);
        Weapon testAxe = new Weapon(GetWeapon(2));
        Game.Inventory.InsertItem(testAxe, 1);
        Weapon testWand = new Weapon(GetWeapon(3));
        Game.Inventory.InsertItem(testWand, 1);
        Weapon testArtifact = new Weapon(GetWeapon(4));
        Game.Inventory.InsertItem(testArtifact, 1);
        Weapon testStaff = new Weapon(GetWeapon(5));
        Game.Inventory.InsertItem(testStaff, 1);
        Weapon testBow = new Weapon(GetWeapon(6));
        Game.Inventory.InsertItem(testBow, 1);
        Weapon testDagger = new Weapon(GetWeapon(7));
        Game.Inventory.InsertItem(testDagger, 1);
        Armor testHeavyHead = new Armor(GetArmor(0));
        Game.Inventory.InsertItem(testHeavyHead, 1);
        Armor testLightHead = new Armor(GetArmor(1));
        Game.Inventory.InsertItem(testLightHead, 1);
        Armor testMediumHead = new Armor(GetArmor(2));
        Game.Inventory.InsertItem(testMediumHead, 1);
        Armor testHeavyBody = new Armor(GetArmor(3));
        Game.Inventory.InsertItem(testHeavyBody, 1);
        Armor testLightBody = new Armor(GetArmor(4));
        Game.Inventory.InsertItem(testLightBody, 1);
        Armor testMediumBody = new Armor(GetArmor(5));
        Game.Inventory.InsertItem(testMediumBody, 1);
        Armor testHeavyLeggings = new Armor(GetArmor(6));
        Game.Inventory.InsertItem(testHeavyLeggings, 1);
        Armor testLightLeggings = new Armor(GetArmor(7));
        Game.Inventory.InsertItem(testLightLeggings, 1);
        Armor testMediumLeggings = new Armor(GetArmor(8));
        Game.Inventory.InsertItem(testMediumLeggings, 1);
        Armor testHeavyGloves = new Armor(GetArmor(9));
        Game.Inventory.InsertItem(testHeavyGloves, 1);
        Armor testLightGloves = new Armor(GetArmor(10));
        Game.Inventory.InsertItem(testLightGloves, 1);
        Armor testMediumGloves = new Armor(GetArmor(11));
        Game.Inventory.InsertItem(testMediumGloves, 1);
        Armor testHeavyBoots = new Armor(GetArmor(12));
        Game.Inventory.InsertItem(testHeavyBoots, 1);
        Armor testLightBoots = new Armor(GetArmor(13));
        Game.Inventory.InsertItem(testLightBoots, 1);
        Armor testMediumBoots = new Armor(GetArmor(14));
        Game.Inventory.InsertItem(testMediumBoots, 1);

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

    public List<ReinforceDataTemplate> GetAllReinforceDataTemplates(){
        return reinforceDataTemplates.ToList();
    }

    public ReinforceData GetReinforceData(ReinforceEquipmentType equipmentType, int equipmentLv){
        ReinforceDataTemplate reinforceDataTemplate = reinforceDataTemplates.FirstOrDefault(r => r.equipmentType == equipmentType && r.equipmentLv == equipmentLv);
        if(reinforceDataTemplate == null){
            return null;
        }
        else{
            return new ReinforceData(reinforceDataTemplate);
        }
    }

    public List<EnchantmentTemplate> GetAllEnchantmentTemplates(){
        return enchantmentTemplates.ToList();
    }

    public EnchantmentTemplate GetEnchantmentTemplate(int enchantmentId)
    {
        if(enchantmentId > enchantmentTemplates.Length || enchantmentId <= 0){
            return null;
        }
        return enchantmentTemplates[enchantmentId - 1];
    }

    public EnchantmentData GetEnchantmentData(int equipmentRequireLevel){
        EnchantmentDataTemplate enchantmentDataTemplate = enchantmentDataTemplates.FirstOrDefault(e => e.equipmentRequireLevel == equipmentRequireLevel);
        if(enchantmentDataTemplate == null){
            return null;
        }
        else{
            return new EnchantmentData(enchantmentDataTemplate);
        }
    }




}
