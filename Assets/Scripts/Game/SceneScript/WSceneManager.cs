using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manage all scene.go scene load scene prefabs.
/// </summary>
public class WSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
    public void GoScene(string sceneName)
    {
        StartCoroutine(_goScene(sceneName));
    }
    /// <summary>
    /// 跳转到指定的场景，首先加载一个空场景来卸载掉之前的场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator _goScene(string sceneName)
    {
        SceneManager.LoadScene("EmptyScene");//把之前的场景先卸载掉
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
        GameObject sceneScripte = GameObject.Find("SceneScript");//放置场景脚本
        GameObject root = GameObject.Find("Root");  //用来存储预制体节点的Object
        ISceneListener listener = sceneScripte.GetComponent<ISceneListener>();
        IProcess process = sceneScripte.GetComponent<IProcess>();
        listener.OnSceneStartLoad();
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
}
