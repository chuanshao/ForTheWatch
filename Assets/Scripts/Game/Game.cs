using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// 整个游戏的管理器
/// </summary>
public class Game : MonoBehaviour
{
    public SceneScript script
    {
        get
        {
            return _script;
        }
    }
    public static Game Instance
    {
        get
        {
            if (_instance == null)
				_instance = Singleton.Instance.AddComponent<Game>();
            return _instance;
        }
    }
    private SceneScript _script;
    private static Game _instance;
    /// <summary>
    /// 跳转到一个场景
    /// </summary>
    /// <param name="sceneName"></param>
    IEnumerator _goScene(string sceneName)
    {
        SceneManager.LoadScene("EmptyScene");
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (op.isDone)
        {
            yield return null;
        }
        GameObject sceneScriptObject = GameObject.Find("sceneScript");
        InitSceneScript(sceneScriptObject);
        _script.sceneBase.OnSceneLoadStart();//场景资源开始加载
        ///下面可以进行场景的资源加载
        _script.sceneBase.OnSceneLoadProcess(0f);
        GameObject cacheRes = GameObject.Find("cacheRes"); //需要预先缓存的数据
        _script.sceneBase.OnSceneLoadComplete();
        yield return null;
    }
    public void GoScene(string sceneName)
    {
        StartCoroutine(_goScene(sceneName));
    }
    void InitSceneScript(GameObject sceneGo)
    {
        if (sceneGo != null)
        {
            _script = new SceneScript();
            _script.uiCanvas = sceneGo.GetComponent<UICanvans>();
            _script.sceneBase = sceneGo.GetComponent<SceneBase>();
        }
    }
}
public class SceneScript
{
    public SceneBase sceneBase;
    public UICanvans uiCanvas;
}
