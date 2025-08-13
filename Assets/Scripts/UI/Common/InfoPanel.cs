using UnityEngine;

public abstract class InfoPanel : MonoBehaviour
{
    protected System.Object obj;
    public void SetUp(System.Object obj){
        this.obj = obj;
    }
    public abstract void Render();
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}