using UnityEngine;
using System.Collections;

public class NavigationItem : MonoBehaviour
{
    public GameObject SelectGo;
    public GameObject UnSelectGo;
    public NavigationBar NVBar;
    // Use this for initialization
    void Start()
    {

    }
    public void OnSelect() {
        SelectGo.SetActive(true);
        UnSelectGo.SetActive(false);
    }
    public void UnSelect() {
        SelectGo.SetActive(false);
        UnSelectGo.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
