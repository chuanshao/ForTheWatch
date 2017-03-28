using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public class Ass : MonoBehaviour
{
    private static Ass _instance;
    private Dictionary<string, string> _localAssets;//本地资源包内的资源
    private Dictionary<string, string> _dirAssets;//更新文件夹内的资源列表
    private Dictionary<string, AssetBundle> _assetBundleList = new Dictionary<string, AssetBundle>();
    public static Ass Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Ass").AddComponent<Ass>();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 加载配置表
    /// </summary>
    public void LoadConfigs()
    {
        
    }
    public T GetConfig<T>(string configName)
    {
        return default(T);
    }
    public void GetPrefab(string path , Action<GameObject> callback)
    {
        StartCoroutine(_getPrefab(path, delegate (GameObject go)
        {
            callback(go);
        }));
    }

    IEnumerator _getPrefab(string path , Action<GameObject> callback)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            ResourceRequest request = Resources.LoadAsync(path);
            yield return request;
            callback((GameObject)request.asset);
        }
        else
        {
            bool isInUpdateFolder = JudgeThisAssetsInUpdateFolder(path);
            StartCoroutine(_getAssetBundle(path , isInUpdateFolder , delegate(AssetBundle bundle) 
            {
                callback((GameObject)bundle.mainAsset);
            }));
            yield return null;
        }
    }
    /// <summary>
    /// 随游戏一同发布的AssetBundle（一般位于StreamingAssets文件夹中）：
    /// 在打AssetBundle包时，使用LZ4压缩格式进行打包（开启BuildAssetBundleOptions.ChunkBasedCompression即可）。
    /// 在运行时需要加载AssetBundle对象时，使用LoadFromFile方法进行加载。
    /// 这样做的好处是：即可以将AssetBundle文件压缩，又可以兼顾加载速度，且节约内存。
    /// 作为更新包，需要从服务端下载的AssetBundle：
    /// 在打AssetBundle包时，使用默认的LZMA格式压缩。
    /// 使用WWW.LoadFromCacheOrDownload方法下载并缓存AssetBundle包文件。
    /// 这样做的好处是：获得了最大的压缩率，在下载过程中可以减少数据传输量。同时，在本地磁盘创建缓存之后，又可以兼顾之后的加载速度，且节约内存。
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isLoalData">是否在安装包内</param>
    /// <returns></returns>
    IEnumerator _getAssetBundle(string path , bool isLoalData , Action<AssetBundle> cb)
    {
        AssetBundle bundle;
        if (_assetBundleList.ContainsKey(path))
        {
            bundle = _assetBundleList[path];
            cb(bundle);
        }
        else
        {
            if (isLoalData) //安装包内的文件
            {
                AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path); //
                yield return request;
                bundle = request.assetBundle;
            }
            else
            {
                WWW www = WWW.LoadFromCacheOrDownload(path, 0);
                bundle = www.assetBundle;
                yield return www;
            }
            cb(bundle);
            _assetBundleList.Add(path, bundle);
        }
        yield return null;
    }

    public void GetPrefabs(List<string> path , Action<Dictionary<string , GameObject> , float> callback )
    {
        var index = 0;
        var processNow = 0f;
        Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>(); 
        for (int i = 0; i < path.Count; i++)
        {
            var pathItem = path[i];
            GetPrefab(pathItem , delegate (GameObject go)
            {
                index++;
                processNow = index / path.Count;
                gameObjects.Add(pathItem , go);
                callback(gameObjects, processNow);
            });
        }
    }

    /// <summary>
    /// 判断当前文件更新目录是否有
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    bool JudgeThisAssetsInUpdateFolder(string path)
    {
        return false;
    }
}
