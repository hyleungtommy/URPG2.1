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
    protected List<Object> displayList = new List<Object>();
    public virtual void OnBoxClicked(int slotId){
        infoPanel.SetUp(displayList[slotId]);
        infoPanel.Render();
        infoPanel.Show();
        selectedSlotId = slotId;
    }
    public virtual void OnTabClicked(int tabId){
        selectedTabId = tabId;
        Render();
    }

    public void Render(){
        infoPanel.Hide();
        foreach(Transform child in boxContainer){
            Destroy(child.gameObject);
        }
        boxList.Clear();
        for(int i = 0; i < displayList.Count; i++){
            int j = i;
            GameObject boxObj = Instantiate(boxPrefab, boxContainer);
            T box = boxObj.GetComponent<T>();
            box.SetUp(displayList[i]);
            box.Render();
            box.GetComponent<Button>().onClick.AddListener(() => OnBoxClicked(j));
            boxList.Add(box);
        }
    }
}