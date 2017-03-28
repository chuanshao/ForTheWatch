using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 面板和窗口的基类
/// </summary>
public class GuiBase : MonoBehaviour {
    protected IUIAnimationInterface _uiAnimation;
    private RectTransform _rectTransform;
    public GUIType Type = GUIType.Panel;
    public RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }
    public enum GUIType
    {
        Window,
        Panel
    }
    public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
