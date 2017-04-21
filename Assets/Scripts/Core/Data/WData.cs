using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WData
{
    private List<WData> _listData = new List<WData>();
    private Dictionary<string, WData> _dic = new Dictionary<string, WData>();
    public virtual void ParseJson(LitJson.JsonData jdata) {

    }
    public object this[int i]{
        get {
            return this._listData[i];
        }
    }
    public object this[string key]
    {
        get {
            if (this._dic.ContainsKey(key))
            {
                return this._dic[key];
            }
            return null;
        }
    }
}
