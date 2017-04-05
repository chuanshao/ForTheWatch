using UnityEngine;
using System.Collections;

public class PlayScene : SceneBase
{
	public override void OnSceneLoadComplete ()
	{
		Ass.Instance.GetPrefab ("PlayUi", delegate(GameObject obj) {
			
		});
	}
	public override void OnSceneLoadProcess (float process)
	{
		throw new System.NotImplementedException ();
	}
	public override void OnSceneLoadStart ()
	{
		throw new System.NotImplementedException ();
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

