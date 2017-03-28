using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
///从右向左滑动GUI
/// </summary>
public class NormalEnterAnimation : IUIAnimationInterface
{
    public void MoveAnimation(GuiBase gui, Vector2 startPos, Vector2 endPos)
    {
        gui.rectTransform.anchoredPosition = startPos;
        gui.rectTransform.DOAnchorPos(endPos, 0.2f);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
