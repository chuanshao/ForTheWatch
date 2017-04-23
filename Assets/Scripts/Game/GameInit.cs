using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using LitJson;
/// <summary>
/// The game start from this class , and this class will do something.
/// First compare version number ,if local version behind of server this class will open update funcution
/// else go next step.The next step is Loading login assets(prefabs , config) and open login scene;
/// </summary>
public class GameInit : MonoBehaviour
{ 
    // Use this for initialization
    void Start ()
    {
        var loom = Loom.Current;
        Game.Instance.GoScene("Login");

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
