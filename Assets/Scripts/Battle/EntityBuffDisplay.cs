using UnityEngine;
using UnityEngine.UI;
public class EntityBuffDisplay : MonoBehaviour{
    public Image[] buffIcons;
    public void Render(Buff[] buffs){
        for (int i = 0; i < buffIcons.Length; i++){
            if (i < buffs.Length){
                buffIcons[i].gameObject.SetActive(true);
                buffIcons[i].sprite = buffs[i].icon;
            }else{
                buffIcons[i].gameObject.SetActive(false);
            }
        }
    }
}