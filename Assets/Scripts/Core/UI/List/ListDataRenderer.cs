using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum ListSelectType{
    Radio, //单选
    Multiselect,//多选
}
[ExecuteInEditMode]
public class ListDataRenderer : MonoBehaviour, IUEventListener
{
    public int ChildCount;
    public GameObject Go;
    public ListSelectType SelectType = ListSelectType.Radio;
    private List<GameObject> _items = new List<GameObject>();
    private WData _listData;
    private ISelect _currentSelect;
    // Use this for initialization
    void Start()
    {

    }
    public bool EventHandler(string eventType,
                      UEventData eventData,
                      GameObject eventGameObject)
    {
        if (eventType == "itemClick") {
            SelectItem(eventGameObject);
            
            return true;
        }
        return false;
    }
    public void SetListData(WData dataList) {
        if (_listData == dataList) return;
        _listData = dataList;
        InitList();
    }
    private void Awake()
    {
        
    }
    public void SelectItem(GameObject go) {
        ISelect selectItem = go.GetComponent<ISelect>();
        if (SelectType == ListSelectType.Radio)
        {
            selectItem.select = !selectItem.select;
            if (selectItem.select)
            {//当前选中状态
                if (_currentSelect != null && _currentSelect != selectItem) {//取消之前选中的
                    _currentSelect.select = false;
                }
                _currentSelect = selectItem;
            }
            else {
                _currentSelect = null;
            }
        }
        else {
            selectItem.select = !selectItem.select;
        }
    }
    void InitList() {
        DestoryAll();
        for (int i = 0; i < _listData.Length; i++) {
            GameObject go = GameObject.Instantiate(Go);
            go.transform.SetParent(transform, false);
            IData d = go.GetComponent<IData>();
            d.data = _listData[i];
            _items.Add(go);
        }
    }
    void DestoryAll() {
        if (_items == null || _items.Count == 0)
        {
            return;
        }
        for (int i = 0; i < _items.Count; i++) {
            DestoryItem(_items[i]);
        }
        _items.Clear();
    }
    void DestoryItem(GameObject go) {
        if (Application.isPlaying)
        {
            GameObject.Destroy(go);
        }
        else {
            GameObject.DestroyImmediate(go);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor && !Application.isPlaying) {
            var WD = new WData();
            for (int i = 0; i < ChildCount; i++)
            {
                WD.Add(new WData());
            }
            SetListData(WD);
        }
    }
}
