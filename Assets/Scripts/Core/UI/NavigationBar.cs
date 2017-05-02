using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// 选择栏
/// </summary>
public class NavigationBar : MonoBehaviour , IUEventListener
{
    public List<NavigationItem> navigations;
    public NavigationItem CurrentSelect;
    public bool EventHandler(string eventType,
                      UEventData eventData,
                      GameObject eventGameObject)
    {
        if (eventType == "itemSelect") {
            var cs = eventGameObject.GetComponent<NavigationItem>();
            OnNavigationItemSelect(cs);
            return true;
        }
        return false;
    }
    public void OnNavigationItemSelect(NavigationItem item) {
        if (item == CurrentSelect) {
            return;
        }
        item.OnSelect();
        CurrentSelect.UnSelect();
        CurrentSelect = item;
    }
    public void Start()
    {
        
    }

    public void Awake()
    {
        CurrentSelect.OnSelect();
    }

    public void Update()
    {
        
    }
}
