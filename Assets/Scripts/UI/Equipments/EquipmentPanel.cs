using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] EquipmentInfoPanel selectedEquipmentInfoPanel;
    [SerializeField] EquipmentInfoPanel currentEquipmentInfoPanel;
    [SerializeField] GameObject basicItemBoxPrefab;
    [SerializeField] Transform basicItemBoxParent;
    [SerializeField] CurrentEquipmentGroup currentEquipmentGroup;
    [SerializeField] Button equipButton;
    [SerializeField] Button unequipButton;
    [SerializeField] Text characterName;

    private List<BasicItemBox> basicItemBoxes = new List<BasicItemBox>();
    private List<StorageSlot> equipments = new List<StorageSlot>();
    private BattleCharacter character;
    private StorageSlot selectedEquipment;
    private Equipment selectedCurrentEquipment;
    public void Setup(BattleCharacter character)
    {
        this.character = character;
    }

    void Render()
    {
        equipments = GameController.Instance.Inventory.GetEquipmentList();
        foreach (Transform child in basicItemBoxParent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < equipments.Count; i++)
        {
            int j = i;
            GameObject boxObj = Instantiate(basicItemBoxPrefab, basicItemBoxParent);
            BasicItemBox box = boxObj.GetComponent<BasicItemBox>();
            box.GetComponent<Button>().onClick.AddListener(() => this.OnEquipmentBoxClicked(j));
            box.Render(equipments[i].Item as Equipment);
            basicItemBoxes.Add(box);
        }
        currentEquipmentGroup.Render(character.Class.EquipmentManager);
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
        characterName.text = character.Name;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Render();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnEquipmentBoxClicked(int index)
    {
        selectedEquipment = equipments[index];
        selectedEquipmentInfoPanel.Render(selectedEquipment.Item as Equipment);
        equipButton.gameObject.SetActive(canEquip());
    }

    private bool canEquip(){
        return selectedEquipment.Item != null && selectedEquipment.Item is Equipment && (selectedEquipment.Item as Equipment).RequireLv <= character.Lv;
    }

    public void OnClickEquip()
    {
        character.Class.EquipmentManager.Equip(selectedEquipment.Item as Equipment);
        selectedEquipment.Clear();
        selectedEquipmentInfoPanel.Clear();
        equipButton.gameObject.SetActive(false);
        Render();
    }

    public void OnClickUnequip(){
        character.Class.EquipmentManager.Unequip(selectedCurrentEquipment);
        currentEquipmentInfoPanel.Clear();
        unequipButton.gameObject.SetActive(false);
        Render();
    }

    public void OnClickCurrentEquipment(int index){
        if(index == 0){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.MainHand);
            selectedCurrentEquipment = character.Class.EquipmentManager.MainHand;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.MainHand != null);
        }
        else if(index == 1){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.OffHand);
            selectedCurrentEquipment = character.Class.EquipmentManager.OffHand;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.OffHand != null);
        }
        else if(index == 2){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Head);
            selectedCurrentEquipment = character.Class.EquipmentManager.Head;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.Head != null);
        }
        else if(index == 3){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Body);
            selectedCurrentEquipment = character.Class.EquipmentManager.Body;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.Body != null);
        }
        else if(index == 4){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Hands);
            selectedCurrentEquipment = character.Class.EquipmentManager.Hands;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.Hands != null);
        }
        else if(index == 5){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Legs);
            selectedCurrentEquipment = character.Class.EquipmentManager.Legs;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.Legs != null);
        }
        else if(index == 6){
            currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Feet);
            selectedCurrentEquipment = character.Class.EquipmentManager.Feet;
            unequipButton.gameObject.SetActive(character.Class.EquipmentManager.Feet != null);
        }
        else if(index == 7){
            //currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Accessory1);
        }
        else if(index == 8){
            //currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Accessory2);
        }
    }
}
