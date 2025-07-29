using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExploreSiteBox : MonoBehaviour
{
    public Text siteName;
    public Text requireTime;
    public Image exploreSiteImage;
    public ObtainableResourceList obtainableResourceList;
    private ExploreSite exploreSite;

    public void SetExploreSite(ExploreSite exploreSite)
    {
        this.exploreSite = exploreSite;
    }

    public void Render(){
        siteName.text = exploreSite.siteName + "\n" + exploreSite.type.ToString() + " Lv." + exploreSite.requiredLevel;
        if (exploreSite.exploreTask != null)
        {
            if (exploreSite.exploreTask.GetRemainingTimeSecond() <= 0)
            {
                requireTime.text = "Done!";
            }
            else
            {
                requireTime.text = exploreSite.exploreTask.GetRemainingTimeFormatted();
            }
        }
        else
        {
            requireTime.text = new DateTime(new TimeSpan(0, 0, exploreSite.requireTime).Ticks).ToString("HH:mm:ss");
        }
        exploreSiteImage.sprite = DBManager.Instance.GetExploreSiteSprite(exploreSite.type);
        obtainableResourceList.SetObtainableResourceList(exploreSite.obtainableItems.Select(item => new ItemWithQuantity(item.item.GetItem(), item.minAmount)).ToList());
    }

}