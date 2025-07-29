using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExploreCampScene : MonoBehaviour
{
    [SerializeField] CraftSkillRow craftSkillRows;
    [SerializeField] GameObject itemBoxPrefab;
    [SerializeField] GameObject scrollViewContent;
    [SerializeField] ExploreCompleteDialog exploreCompleteDialog;
    [SerializeField] Text siteName;
    [SerializeField] Text siteType;
    [SerializeField] Text siteDescription;
    [SerializeField] Text siteTime;
    [SerializeField] Text sitePrice;
    [SerializeField] Text availableExploreTeam;
    [SerializeField] Image siteImage;
    [SerializeField] ObtainableResourceList siteReward;
    [SerializeField] Button exploreButton;
    [SerializeField] Button collectButton;
    int selectedSlotId;
    int selectedTabId;
    List<ExploreSite> miningSites;
    List<ExploreSite> forgingSites;
    List<ExploreSite> huntingSites;
    List<ExploreSite> displayList;
    List<ExploreSiteBox> boxList;

    public void Start()
    {
        Debug.Log(Game.ExploreSiteList.Count);
        miningSites = Game.ExploreSiteList.Where(site => site.type == ExploreSiteType.Mining).ToList();
        forgingSites = Game.ExploreSiteList.Where(site => site.type == ExploreSiteType.Forging).ToList();
        huntingSites = Game.ExploreSiteList.Where(site => site.type == ExploreSiteType.Hunting).ToList();
        ClearInfoPanel();
        OnClickTab(0);
    }

    public void ClearInfoPanel()
    {
        siteName.text = "";
        siteType.text = "";
        siteDescription.text = "";
        siteTime.text = "";
        sitePrice.text = "";
        siteReward.gameObject.SetActive(false);
    }

    public void RenderSkillRow()
    {
        if (selectedTabId == 0)
        {
            craftSkillRows.Render(Game.CraftSkillManager.Mining);
            displayList = miningSites;
            Debug.Log(displayList.Count);
        }
        else if (selectedTabId == 1)
        {
            craftSkillRows.Render(Game.CraftSkillManager.Forging);
            displayList = forgingSites;
        }
        else if (selectedTabId == 2)
        {
            craftSkillRows.Render(Game.CraftSkillManager.Hunting);
            displayList = huntingSites;
        }
    }

    public void RenderScrollContent()
    {
        StopAllCoroutines();
        boxList = new List<ExploreSiteBox>();
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < displayList.Count; i++)
        {
            int j = i;

            GameObject boxObj = Instantiate(itemBoxPrefab, scrollViewContent.transform);
            ExploreSiteBox box = boxObj.GetComponent<ExploreSiteBox>();
            box.GetComponent<ExploreSiteBox>().SetExploreSite(displayList[j]);
            boxList.Add(box.GetComponent<ExploreSiteBox>());
            box.GetComponent<Button>().onClick.AddListener(() => OnClickExploreSite(j));
            box.Render();
        }
        StartCoroutine("UpdateCurrentTaskCtrl");
    }

    public void OnClickTab(int tabId)
    {
        exploreCompleteDialog.Hide();
        selectedTabId = tabId;
        RenderSkillRow();
        RenderScrollContent();
        ClearInfoPanel();
    }

    public void OnClickExploreSite(int slotId)
    {
        selectedSlotId = slotId;
        siteName.text = displayList[slotId].siteName;
        siteType.text = displayList[slotId].type.ToString();
        siteDescription.text = displayList[slotId].description;
        siteTime.text = displayList[slotId].requireTime.ToString();
        sitePrice.text = displayList[slotId].price.ToString();
        siteReward.SetObtainableResourceList(displayList[slotId].obtainableItems.Select(item => new ItemWithQuantity(item.item.GetItem(), item.minAmount)).ToList());
        siteReward.gameObject.SetActive(true);
        siteImage.sprite = DBManager.Instance.GetExploreSiteSprite(displayList[slotId].type);
    }

    public void onClickExplore()
    {
        displayList[selectedSlotId].StartExploreTask();
        Game.CraftSkillManager.AvailableExploreTeam--;
        Game.Money -= displayList[selectedSlotId].price;
    }

    public void onClickCollect()
    {
        TaskCompleteMessage taskCompleteMsg = displayList[selectedSlotId].exploreTask.CompleteTask();
        exploreCompleteDialog.SetExploreCompleteDialog(taskCompleteMsg.RewardItemList);
        exploreCompleteDialog.Show();
        displayList[selectedSlotId].exploreTask = null;
        RenderSkillRow();
        RenderScrollContent();
    }


    IEnumerator UpdateCurrentTaskCtrl()
    {

        while (true)
        {
            foreach (ExploreSiteBox box in boxList)
            {
                box.Render();
            }
            availableExploreTeam.text = "Available Explore Team: " + Game.CraftSkillManager.AvailableExploreTeam + "/" + Constant.MaxExploreTeam;
            if (displayList != null && displayList.Count > selectedSlotId && displayList[selectedSlotId].exploreTask != null)
            {
                exploreButton.gameObject.SetActive(false);
                if(displayList[selectedSlotId].exploreTask.IsTaskCompleted()){
                    siteTime.text = "Done! Click to collect items";
                    collectButton.gameObject.SetActive(true);
                }else{
                    collectButton.gameObject.SetActive(false);
                    siteTime.text = displayList[selectedSlotId].exploreTask.GetRemainingTimeFormatted();
                }
            }
            else
            {
                collectButton.gameObject.SetActive(false);
                exploreButton.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
        }

    }

}