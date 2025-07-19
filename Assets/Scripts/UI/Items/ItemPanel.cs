using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPanel : MonoBehaviour
{
    [SerializeField] GameObject itemSelectionRowPrefab;
    [SerializeField] Transform itemSelectionRowContainer;
    private List<ItemWithQuantity> battleFunctionalItems;
    private List<ItemSelectionRow> itemSelectionRows = new List<ItemSelectionRow>();

    public void Open()
    {
        gameObject.SetActive(true);
        battleFunctionalItems = GameController.Instance.Inventory.GetBattleFunctionalItemList();
        // Clear existing boxes
        foreach (Transform child in itemSelectionRowContainer)
        {
            Destroy(child.gameObject);
        }
        itemSelectionRows.Clear();

        for (int i = 0; i < battleFunctionalItems.Count; i++)
        {
            int j = i;
            GameObject box = Instantiate(itemSelectionRowPrefab, itemSelectionRowContainer);
            ItemSelectionRow itemSelectionRow = box.GetComponent<ItemSelectionRow>();
            itemSelectionRow.Setup(battleFunctionalItems[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.OnItemSelectionRowClicked(j));
            itemSelectionRow.Render();
            itemSelectionRows.Add(itemSelectionRow);
        }
    }
    

    public void OnItemSelectionRowClicked(int index)
    {
        BattleScene.Instance.OnSelectItem(battleFunctionalItems[index].Item);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    
}
