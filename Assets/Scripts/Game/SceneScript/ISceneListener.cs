using UnityEngine;
using System.Collections;

public interface ISceneListener
{
    void OnSceneStartLoad();        //开始加载场景中的各种预制体
    void OnSceneLoadComplete();     //场景中的预制体加载完成
}
