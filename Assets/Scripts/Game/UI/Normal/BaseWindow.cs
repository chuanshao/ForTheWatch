using UnityEngine;
using System.Collections;

public class BaseWindow : GuiBase
{

    // Use this for initialization
    void Start()
    {

    }
    public override void OnEnter()
    {
        if(_uiAnimation == null) _uiAnimation = new PopUpAnimation();
        _uiAnimation.MoveAnimation(this, new Vector2(1920, 0), Vector2.zero);
    }
    public override void OnExit()
    {
        GameObject.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
