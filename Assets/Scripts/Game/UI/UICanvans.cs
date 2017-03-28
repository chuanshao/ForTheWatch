using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// Manager all instantiation Panel ， Window.
/// </summary>
public class UICanvans : MonoBehaviour {
    private Stack<GuiBase> _panels = new Stack<GuiBase>();
    private Canvas _canvas;
    private CanvasScaler _canvasScaler;
    private RectTransform _rectTransform;
    private GuiBase _currentGui;
    void Awake()
    {
        CreateCanvans();
        CreateEventSystem();
    }
    // Use this for initialization
    void Start ()
    {

	}
    /// <summary>
    /// 创建并设置canvas的参数
    /// </summary>
    void CreateCanvans()
    {
        var canvasGo = new GameObject("UICanvas");
        _rectTransform = canvasGo.AddComponent<RectTransform>();

        _canvas = canvasGo.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _canvasScaler = canvasGo.AddComponent<CanvasScaler>();
        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _canvasScaler.referenceResolution = new Vector2(1920, 1080);
        _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        _canvasScaler.matchWidthOrHeight = 1;

        canvasGo.AddComponent<GraphicRaycaster>();
    }
    void CreateEventSystem()
    {
        var eventSystem = GameObject.Find("EventSystem");
        if (eventSystem == null)
        {
            var eventSystemGo = new GameObject("EventSystem");
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();
        }
    }
    public void OpenGui(GuiBase gui)
    {
        gui.rectTransform.SetParent(_rectTransform , false);
        gui.OnEnter();
        _panels.Push(gui);
    }
    public void CloseCurrentGui()
    {
        if (_panels.Count > 0)
        {
            var currentGui = _panels.Pop();
            currentGui.rectTransform.SetParent(null);
            currentGui.OnExit();
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
