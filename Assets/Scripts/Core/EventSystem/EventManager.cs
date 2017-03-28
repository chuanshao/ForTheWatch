using UnityEngine;
using System;
using LitJson;
using System.Collections;
using System.Collections.Generic;
public class EventManager : IDisposable
{
    private Dictionary<string, List<Action<JsonData>>> _callBackMap = new Dictionary<string, List<Action<JsonData>>>();
    private Dictionary<uint, Action<JsonData>> _onceCallBackMap = new Dictionary<uint, Action<JsonData>>();
    public void AddEvent(string key , Action<JsonData> cb)
    {
        if (!_callBackMap.ContainsKey(key))
        {
            _callBackMap.Add(key, new List<Action<JsonData>>());
        }
        _callBackMap[key].Add(cb);
    }
    public void InvokeOnEvent(string key , JsonData jdata)
    {
        Invoke(_callBackMap, key, jdata, false);
    }
    public void InvokeOnCallBack(uint key, JsonData jdata)
    {
        if (this._onceCallBackMap.ContainsKey(key))
        {
            this._onceCallBackMap[key].Invoke(jdata);
        }
    }
    void Invoke(Dictionary<string, List<Action<JsonData>>> callbackMap , string key, JsonData jdata, bool once)
    {
        if (!callbackMap.ContainsKey(key)) return;

        List<Action<JsonData>> list = callbackMap[key];
        foreach (Action<JsonData> action in list)
        {
            action.Invoke(jdata);
            if (once)
            {
                list.Remove(action);
            }
        }
    }
    public void AddOnceEvent(uint key, Action<JsonData> cb)
    {
        if (key > 0 && cb != null)
        {
            this._onceCallBackMap.Add(key, cb);
        }
        
    }
    public void Dispose()
    {
        this._callBackMap.Clear();
        this._onceCallBackMap.Clear();
    }
}
