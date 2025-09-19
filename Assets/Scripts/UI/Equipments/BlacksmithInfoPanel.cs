using UnityEngine;
using UnityEngine.UI;

public class BlacksmithInfoPanel:InfoPanel{
    [SerializeField] Text equipmentNameText;
    [SerializeField] Text equipmentDescriptionText;
    [SerializeField] Text equipmentTypeText;
    [SerializeField] Image equipmentIconImage;
    [SerializeField] EquipmentPowerText equipmentPowerText;
    [SerializeField] Text equipmentPriceText;
    [SerializeField] Button equipmentBuyButton;
    [SerializeField] Text errorText;
    [SerializeField] BlacksmithScene blacksmithScene;

    public override void Render(){
        if(obj is WeaponTemplate){
            WeaponTemplate weaponTemplate = obj as WeaponTemplate;
            equipmentNameText.text = weaponTemplate.Name;
            equipmentDescriptionText.text = weaponTemplate.Description;
            equipmentTypeText.text = weaponTemplate.WeaponType.ToString();
            equipmentIconImage.sprite = weaponTemplate.Icon;
            equipmentPowerText.Render(weaponTemplate);
            equipmentPriceText.text = weaponTemplate.Price.ToString();
            if (Game.Money < weaponTemplate.Price){
                errorText.gameObject.SetActive(true);
                errorText.text = "Money not enough";
                equipmentBuyButton.gameObject.SetActive(false);
            }
            else{
                errorText.gameObject.SetActive(false);
                equipmentBuyButton.gameObject.SetActive(true);
            }
        }else if(obj is ArmorTemplate){
            ArmorTemplate armorTemplate = obj as ArmorTemplate;
            equipmentNameText.text = armorTemplate.Name;
            equipmentDescriptionText.text = armorTemplate.Description;
            equipmentTypeText.text = armorTemplate.ArmorType.ToString();
            equipmentIconImage.sprite = armorTemplate.Icon;
            equipmentPowerText.Render(armorTemplate);
            equipmentPriceText.text = armorTemplate.Price.ToString();
            if (Game.Money < armorTemplate.Price){
                errorText.gameObject.SetActive(true);
                errorText.text = "Money not enough";
                equipmentBuyButton.gameObject.SetActive(false);
            }
            else{
                errorText.gameObject.SetActive(false);
                equipmentBuyButton.gameObject.SetActive(true);
            }
        }
    }

    public void OnClickBuy(){
        if(obj is WeaponTemplate){
            WeaponTemplate weaponTemplate = obj as WeaponTemplate;
            Game.Money -= weaponTemplate.Price;
            Game.Inventory.InsertItem(new Weapon(weaponTemplate), 1);
        }
        else if(obj is ArmorTemplate){
            ArmorTemplate armorTemplate = obj as ArmorTemplate;
            Game.Money -= armorTemplate.Price;
            Game.Inventory.InsertItem(new Armor(armorTemplate), 1);
        }
        blacksmithScene.UpdateMoneyText();
    }
        
}