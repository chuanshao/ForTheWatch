using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public abstract class SceneBase : MonoBehaviour
{
    public abstract void OnSceneLoadStart();
    public abstract void OnSceneLoadProcess(float process);
    public abstract void OnSceneLoadComplete();
}
