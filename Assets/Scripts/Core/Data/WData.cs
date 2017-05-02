using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class WData
{
    private List<WData> _listData = new List<WData>();
    private Dictionary<string, object> _dic = new Dictionary<string, object>();
    public virtual void ParseJson(LitJson.JsonData jdata) {

    }
    public void Add(WData wd) {
        _listData.Add(wd);
    }
    public WData this[int i]{
        get {
            return this._listData[i];
        }
    }
    public int Length {
        get {
            return this._listData.Count;
        }
    }
    public object this[string key]
    {
        get {
            if (this._dic.ContainsKey(key))
            {
                return this._dic[key];
            }
            return GetReflectionProto(key);
        }
        set {
            if (this._dic.ContainsKey(key))
            {
                this._dic[key] = value;
            }
            else {
                SetProtoByReflection(key , value);
            }
        }
    }

    object GetReflectionProto(string key) {
        PropertyInfo pi = this.GetType().GetProperty(key);
        if (pi != null)
        {
            return pi.GetValue(this, null);
        }
        else {
            return null;
        }
    }
    void SetProtoByReflection(string key , object value) {
        PropertyInfo pi = this.GetType().GetProperty(key);
        if (pi != null)
        {
            try {
                pi.SetValue(this, value, null);
            }
            catch (TargetInvocationException e) {

            }
        }
    }
}
