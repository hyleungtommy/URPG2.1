using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithScene : MonoBehaviour
{
    [SerializeField] GameObject blacksmithShopBoxPrefab;
    [SerializeField] Transform blacksmithScrollContainer;
    [SerializeField] Text infoPanelEquipmentName;
    [SerializeField] Text infoPanelEquipmentDescription;
    [SerializeField] Text infoPanelEquipmentType;
    [SerializeField] Image infoPanelEquipmentIcon;
    [SerializeField] EquipmentPowerText infoPanelEquipmentPower;
    [SerializeField] Text infoPanelEquipmentPrice;
    [SerializeField] Button infoPanelEquipmentBuyButton;
    [SerializeField] Text TextMoney;
    [SerializeField] Text TextError;
    List<BlacksmithShopBox> blacksmithShopBoxes = new List<BlacksmithShopBox>();
    List<WeaponTemplate> weapons = new List<WeaponTemplate>();
    List<ArmorTemplate> armors = new List<ArmorTemplate>();
    int selectedTab = 0;
    int selectedIndex = 0;

    void Start(){
        Render();
    }

    void Render(){
        TextMoney.text = Game.Money.ToString();
        foreach (Transform child in blacksmithScrollContainer)
        {
            Destroy(child.gameObject);
        }
        blacksmithShopBoxes.Clear();
        
        weapons = new List<WeaponTemplate>(DBManager.Instance.GetAllWeapons());
        armors = new List<ArmorTemplate>(DBManager.Instance.GetAllArmors());
        TextError.gameObject.SetActive(false);
        infoPanelEquipmentBuyButton.gameObject.SetActive(true);
        if(selectedTab == 0){
            foreach (WeaponTemplate weapon in weapons)
            {
                int i = weapons.IndexOf(weapon);
                GameObject boxObj = Instantiate(blacksmithShopBoxPrefab, blacksmithScrollContainer);
                BlacksmithShopBox blacksmithShopBox = boxObj.GetComponent<BlacksmithShopBox>();
                blacksmithShopBox.Render(weapon);
                blacksmithShopBox.GetComponent<Button>().onClick.AddListener(() => OnBlacksmithShopBoxClicked(i));
            }
        }
        else if(selectedTab == 1){
            foreach (ArmorTemplate armor in armors)
            {
                int i = armors.IndexOf(armor);
                GameObject boxObj = Instantiate(blacksmithShopBoxPrefab, blacksmithScrollContainer);
                BlacksmithShopBox blacksmithShopBox = boxObj.GetComponent<BlacksmithShopBox>();
                blacksmithShopBox.Render(armor);
                blacksmithShopBox.GetComponent<Button>().onClick.AddListener(() => OnBlacksmithShopBoxClicked(i));
            }
        }

    }

    public void OnBlacksmithShopBoxClicked(int index){
        if(selectedTab == 0){
            infoPanelEquipmentName.text = weapons[index].WeaponName;
            infoPanelEquipmentDescription.text = weapons[index].Description;
            infoPanelEquipmentType.text = weapons[index].WeaponType.ToString();
            infoPanelEquipmentIcon.sprite = weapons[index].Icon;
            infoPanelEquipmentPower.Render(weapons[index]);
            infoPanelEquipmentPrice.text = weapons[index].Price.ToString();
            if (Game.Money < weapons[index].Price){
                TextError.gameObject.SetActive(true);
                TextError.text = "Money not enough";
                infoPanelEquipmentBuyButton.gameObject.SetActive(false);
            }
            else{
                TextError.gameObject.SetActive(false);
                TextError.text = "";
                infoPanelEquipmentBuyButton.gameObject.SetActive(true);
            }
            selectedIndex = index;
        }
        else if(selectedTab == 1){
            infoPanelEquipmentName.text = armors[index].ArmorName;
            infoPanelEquipmentDescription.text = armors[index].Description;
            infoPanelEquipmentType.text = armors[index].ArmorType.ToString();
            infoPanelEquipmentIcon.sprite = armors[index].Icon;
            infoPanelEquipmentPower.Render(armors[index]);
            infoPanelEquipmentPrice.text = armors[index].Price.ToString();
            if (Game.Money < armors[index].Price){
                TextError.gameObject.SetActive(true);
                TextError.text = "Money not enough";
                infoPanelEquipmentBuyButton.gameObject.SetActive(false);
            }
            else{
                TextError.gameObject.SetActive(false);
                TextError.text = "";
                infoPanelEquipmentBuyButton.gameObject.SetActive(true);
            }
            selectedIndex = index;
        }
    }

    public void OnTabButtonClicked(int index){
        selectedTab = index;
        Render();
    }

    public void OnBuyButtonClicked(){
        if(selectedTab == 0){
            Game.Money -= weapons[selectedIndex].Price;
            Game.Inventory.InsertItem(new Weapon(weapons[selectedIndex]), 1);
        }
        else if(selectedTab == 1){
            Game.Money -= armors[selectedIndex].Price;
            Game.Inventory.InsertItem(new Armor(armors[selectedIndex]), 1);
        }
        TextMoney.text = Game.Money.ToString();
    }
    
    

    

}
