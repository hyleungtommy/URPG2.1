using UnityEngine;

public class Crafter : MonoBehaviour, Interactable{
    [SerializeField] CrafterType crafterType;
    public void Interact(){
        if (crafterType == CrafterType.AlchemyPot){
            UIController.Instance.OpenUIScene("Alchemy");
        }
        else if (crafterType == CrafterType.Anvil){
            UIController.Instance.OpenUIScene("Smithing");
        }
        else if (crafterType == CrafterType.Reinforcer){
            // TODO: Open reinforcing UI
        }
        else if (crafterType == CrafterType.Enchanter){
            // TODO: Open enchanting UI
        }
        else if (crafterType == CrafterType.Oven){
            // TODO: Open Cooking UI
        }
    }
    
}

public enum CrafterType{
    AlchemyPot,
    Anvil,
    Reinforcer,
    Enchanter,
    Oven
}