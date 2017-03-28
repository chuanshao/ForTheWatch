using UnityEngine;
using System.Collections;
using DG.Tweening;
/// <summary>
/// 弹出GUI动画
/// </summary>
public class PopUpAnimation : IUIAnimationInterface
{
    public void MoveAnimation(GuiBase gui, Vector2 startPos, Vector2 endPos)
    {
        gui.rectTransform.localScale = new Vector3(0.1f, 0.1f, 1f);
        gui.rectTransform.DOScale(Vector3.one, 0.5f);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
