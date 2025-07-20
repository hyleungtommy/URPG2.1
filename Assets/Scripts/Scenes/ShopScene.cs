using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
public class ShopScene : MonoBehaviour
{
    [SerializeField] GameObject ShopBoxPrefab;
    [SerializeField] Transform ShopBoxContainer;
    private List<ShopBox> shopBoxes = new List<ShopBox>();
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemDescription;
    [SerializeField] Text ItemType;
    [SerializeField] Image ItemIcon;
    [SerializeField] Image ItemIconFrame;
    [SerializeField] Text TextBuyAmount;
    [SerializeField] Text TextBuyPrice;
    [SerializeField] Button BuyButton;
    [SerializeField] Text TextMoney;
    [SerializeField] GameObject BuyAmountPanel;
    private List<ItemTemplate> shopItems;
    private int buyAmount = 1;
    private ItemTemplate selectedItem;
    // Start is called before the first frame update
    void Start()
    {

        Render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Render()
    {
        TextMoney.text = Game.Money.ToString();
        // Clear existing boxes
        foreach (Transform child in ShopBoxContainer)
        {
            Destroy(child.gameObject);
        }
        shopBoxes.Clear();

        shopItems = new List<ItemTemplate>(DBManager.Instance.GetAllItems());
        // Create new boxes for each slot
        for (int i = 0; i < shopItems.Count; i++)
        {
            int j = i;
            GameObject boxObj = Instantiate(ShopBoxPrefab, ShopBoxContainer);
            ShopBox box = boxObj.GetComponent<ShopBox>();
            box.Setup(shopItems[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.OnShopBoxClicked(j));
            box.Render();
            shopBoxes.Add(box);
        }
    }

    private void OnShopBoxClicked(int slotId)
    {
        selectedItem = shopItems[slotId];
        ItemName.text = selectedItem.Name;
        ItemDescription.text = selectedItem.Description;
        FormatItemDetails(selectedItem);
        ItemIcon.sprite = selectedItem.Icon;
        ItemIconFrame.gameObject.SetActive(true);
        buyAmount = 1;
        UpdateBuyAmount();
    }

    private void FormatItemDetails(ItemTemplate itemTemplate)
    {
        ItemType.text = itemTemplate.GetType().Name + "\n" + Constant.itemRarityName[itemTemplate.Rarity];
    }

    void UpdateBuyAmount()
    {
        TextBuyAmount.text = buyAmount.ToString();
        int price = selectedItem.Price * buyAmount;
        TextBuyPrice.text = price.ToString();
        if (price > Game.Money)
        {
            BuyButton.interactable = false;
        }
        else
        {
            BuyButton.interactable = true;
        }
    }

    public void OnClickAdd1()
    {
        if (buyAmount < 99)
        {
            buyAmount++;
            UpdateBuyAmount();
        }
    }

    public void OnClickMinus1()
    {
        if (buyAmount > 1)
        {
            buyAmount--;
            UpdateBuyAmount();
        }
    }

    public void OnClickAdd10()
    {

        if (buyAmount < 90)
        {
            buyAmount += 10;
        }else {
            buyAmount = 99;
        }
        UpdateBuyAmount();
    }

    public void OnClickMinus10()
    {
        if (buyAmount > 10)
        {   
            buyAmount -= 10;
        }else {
            buyAmount = 1;
        }
        UpdateBuyAmount();
    }

    public void OnClickBuy()
    {
        int price = selectedItem.Price * buyAmount;
        if (price > Game.Money)
        {
            return;
        }
        Game.Money -= price;
        Game.Inventory.InsertItem(selectedItem.GetItem(), buyAmount);
        TextMoney.text = Game.Money.ToString();

    }

}
