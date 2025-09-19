using UnityEngine;
using UnityEngine.UI;

public class AlchemyListBox : ListBox{
    [SerializeField] Text itemNameText;
    [SerializeField] BasicItemBox itemBox;

    public override void Render(){
        CraftRecipe recipe = obj as CraftRecipe;
        if(recipe != null){
            itemNameText.text = recipe.resultItem.Name;
            itemBox.Render(recipe.resultItem);
        }
    }
}