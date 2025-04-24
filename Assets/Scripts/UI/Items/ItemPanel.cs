using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPanel : MonoBehaviour
{
    [SerializeField] GameObject itemSelectionRowPrefab;
    [SerializeField] Transform itemSelectionRowContainer;
    private BattleItemInventory battleItemInventory;
    private List<ItemSelectionRow> itemSelectionRows = new List<ItemSelectionRow>();

    public void Open()
    {
        gameObject.SetActive(true);
        battleItemInventory = GameController.Instance.Inventory.GetBattleItemInventory();
        // Clear existing boxes
        foreach (Transform child in itemSelectionRowContainer)
        {
            Destroy(child.gameObject);
        }
        itemSelectionRows.Clear();

        for (int i = 0; i < battleItemInventory.items.Count; i++)
        {
            int j = i;
            GameObject box = Instantiate(itemSelectionRowPrefab, itemSelectionRowContainer);
            ItemSelectionRow itemSelectionRow = box.GetComponent<ItemSelectionRow>();
            itemSelectionRow.Setup(battleItemInventory.items[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.OnItemSelectionRowClicked(j));
            itemSelectionRow.Render();
            itemSelectionRows.Add(itemSelectionRow);
        }
    }
    

    public void OnItemSelectionRowClicked(int index)
    {
        Debug.Log("ItemSelectionRowClicked: " + index);
        BattleScene.Instance.OnSelectItem(battleItemInventory.items[index].Item);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    
}
