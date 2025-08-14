using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//EquipmentPane has 2 info panel so it makes it tricky to convert it to use CommonListScene, TODO: convert to use CommonListScene
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
    [SerializeField] Text errorText;

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
        equipments = Game.Inventory.GetEquipmentList();
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
        currentEquipmentGroup.Render(character.CharacterClass.EquipmentManager);
        characterName.text = character.Name;
        selectedEquipmentInfoPanel.Hide();
        currentEquipmentInfoPanel.Hide();
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
        equipButton.gameObject.SetActive(CanEquip());
        selectedEquipmentInfoPanel.Show();
    }

    private bool CanEquip()
    {
        if (selectedEquipment.Item == null)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Please select an equipment";
            return false;
        }
        if (selectedEquipment.Item is Equipment && (selectedEquipment.Item as Equipment).RequireLv > character.Lv)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Require Lv." + (selectedEquipment.Item as Equipment).RequireLv;
            return false;
        }
        errorText.gameObject.SetActive(false);
        return true;
    }

    public void OnClickEquip()
    {
        character.CharacterClass.EquipmentManager.Equip(selectedEquipment.Item as Equipment);
        selectedEquipment.Clear();
        Render();
    }

    public void OnClickUnequip()
    {
        character.CharacterClass.EquipmentManager.Unequip(selectedCurrentEquipment);
        Render();
    }

    public void OnClickCurrentEquipment(int index)
    {
        if (index == 0)
        {
            if (character.CharacterClass.EquipmentManager.MainHand == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.MainHand);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.MainHand;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 1)
        {
            if (character.CharacterClass.EquipmentManager.OffHand == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.OffHand);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.OffHand;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 2)
        {
            if (character.CharacterClass.EquipmentManager.Head == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.Head);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.Head;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 3)
        {
            if (character.CharacterClass.EquipmentManager.Body == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.Body);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.Body;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 4)
        {
            if (character.CharacterClass.EquipmentManager.Hands == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.Hands);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.Hands;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 5)
        {
            if (character.CharacterClass.EquipmentManager.Legs == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.Legs);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.Legs;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 6)
        {
            if (character.CharacterClass.EquipmentManager.Feet == null)
            {
                return;
            }
            currentEquipmentInfoPanel.Render(character.CharacterClass.EquipmentManager.Feet);
            selectedCurrentEquipment = character.CharacterClass.EquipmentManager.Feet;
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 7)
        {
            //currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Accessory1);
        }
        else if (index == 8)
        {
            //currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Accessory2);
        }
    }
}
