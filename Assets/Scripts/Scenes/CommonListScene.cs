using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CommonListScene<T> : MonoBehaviour where T : ListBox{
    public GameObject boxPrefab;
    public Transform boxContainer;
    public InfoPanel infoPanel;
    protected List<T> boxList = new List<T>();
    protected int selectedSlotId = -1;
    protected int selectedTabId = 0;
    protected List<List<System.Object>> displayList = new List<List<System.Object>>();
    public virtual void OnBoxClicked(int slotId){
        infoPanel.SetUp(displayList[selectedTabId][slotId]);
        infoPanel.Render();
        infoPanel.Show();
        selectedSlotId = slotId;
    }
    public virtual void OnTabClicked(int tabId){
        selectedTabId = tabId;
        Render();
    }

    public void AddDisplayList(List<System.Object> displayList){
        this.displayList.Add(displayList);
    }

    public virtual void Render(){
        infoPanel.Hide();
        foreach(Transform child in boxContainer){
            Destroy(child.gameObject);
        }
        boxList.Clear();
        for(int i = 0; i < displayList[selectedTabId].Count; i++){
            int j = i;
            GameObject boxObj = Instantiate(boxPrefab, boxContainer);
            T box = boxObj.GetComponent<T>();
            box.SetUp(displayList[selectedTabId][i]);
            box.Render();
            box.GetComponent<Button>().onClick.AddListener(() => OnBoxClicked(j));
            boxList.Add(box);
        }
    }

    public void ClearDisplayList(){
        displayList.Clear();
    }
}