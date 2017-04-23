using UnityEngine;
using System.Collections;

public class PlayScene : SceneBase
{
	public override void OnSceneLoadComplete ()
	{
		Ass.Instance.GetPrefab ("ui/CreateOrAddWindow", delegate(GameObject go) {
            GameObject iGo = GameObject.Instantiate(go);
            Game.script.uiCanvas.OpenGui(iGo.GetComponent<GuiBase>());
        });
	}
	public override void OnSceneLoadProcess (float process)
	{

	}
	public override void OnSceneLoadStart ()
	{

	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

