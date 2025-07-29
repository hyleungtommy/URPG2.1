using UnityEngine;
using UnityEngine.UI;

public class ShopInfoPanel : InfoPanel
{
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemDescription;
    [SerializeField] Text ItemType;
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text TextBuyAmount;
    [SerializeField] Text TextBuyPrice;
    [SerializeField] ShopScene shopScene;
    [SerializeField] Button BuyButton;
    [SerializeField] Text TextError;
    public override void Render()
    {
        ItemTemplate itemTemplate = obj as ItemTemplate;
        if (itemTemplate != null)
        {
            shopScene.BuyAmount = 1;
            ItemName.text = itemTemplate.Name;
            ItemDescription.text = itemTemplate.Description;
            ItemType.text = itemTemplate.GetItemType() + "\n" + Constant.itemRarityName[itemTemplate.Rarity];
            itemBox.Render(itemTemplate);
            UpdateBuyAmount();
        }
    }

    void UpdateBuyAmount()
    {
        ItemTemplate itemTemplate = obj as ItemTemplate;
        if (itemTemplate != null)
        {
            TextBuyAmount.text = shopScene.BuyAmount.ToString();
            int price = shopScene.BuyAmount * itemTemplate.Price;
            TextBuyPrice.text = price.ToString();
            if (price > Game.Money)
            {
                BuyButton.interactable = false;
                TextError.text = "Not enough money";
            }
            else
            {
                BuyButton.interactable = true;
                TextError.text = "";
            }
        }
    }

    public void OnClickAdd1()
    {
        if (shopScene.BuyAmount < 99)
        {
            shopScene.BuyAmount++;
            UpdateBuyAmount();
        }
    }

    public void OnClickMinus1()
    {
        if (shopScene.BuyAmount > 1)
        {
            shopScene.BuyAmount--;
            UpdateBuyAmount();
        }
    }

    public void OnClickAdd10()
    {

        if (shopScene.BuyAmount < 90)
        {
            shopScene.BuyAmount += 10;
        }
        else
        {
            shopScene.BuyAmount = 99;
        }
        UpdateBuyAmount();
    }

    public void OnClickMinus10()
    {
        if (shopScene.BuyAmount > 10)
        {
            shopScene.BuyAmount -= 10;
        }
        else
        {
            shopScene.BuyAmount = 1;
        }
        UpdateBuyAmount();
    }

    public void OnClickBuy()
    {
        ItemTemplate itemTemplate = obj as ItemTemplate;
        if (itemTemplate != null)
        {
            int price = itemTemplate.Price * shopScene.BuyAmount;
            if (price > Game.Money)
            {
                return;
            }
            Game.Money -= price;
            Game.Inventory.InsertItem(itemTemplate.GetItem(), shopScene.BuyAmount);
            shopScene.BuyAmount = 1;
            UpdateBuyAmount();
            shopScene.UpdateMoney();
        }
    }
}