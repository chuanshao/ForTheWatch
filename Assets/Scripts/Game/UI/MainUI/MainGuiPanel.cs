using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGuiPanel : GuiBase {
    private void Awake()
    {
        _uiAnimation = new PopUpAnimation();
    }
    public override void OnEnter()
    {
        _uiAnimation.MoveAnimation(this, new Vector2(1920, 0), Vector2.zero);
    }
    public override void OnExit()
    {
        GameObject.Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
