using UnityEngine;
using System.Collections;
using LitJson;
using System;


//扩展JsonData
public static class JsonDataExt
{
    public static bool IsInt(this JsonData jsonData)
    {
        if (jsonData.GetType() == typeof(int))
        {
            return true;
        }
        return false;
    }
    public static bool TryGetValue(this JsonData jsonData, string key, out object value)
    {
        if (jsonData.ContainsKey(key))
        {
            value = jsonData[key];
            return true;
        }
        else
        {
            value = null;
            return false;
        }
    }
    public static bool IsDouble(this JsonData jsonData)
    {
        if (jsonData.GetType() == typeof(double))
        {
            return true;
        }
        return false;
    }
    public static int ToInt(this JsonData jsonData)
	{
		if (jsonData.IsInt()) {
			return (int)jsonData;
		} else if(jsonData.IsDouble()) {
			return (int) (double) jsonData;
		} else {
			int val;
			if (System.Int32.TryParse(jsonData.ToString(), out val)) {
				return val;
			} else {
				return 0;
			}
		}
	}
    public static float ToFloat(this JsonData jsonData)
	{
		if (jsonData.IsInt()) {
			int val = (int)jsonData;
			return val * 1.0f;
		} else if (jsonData.IsDouble()) {
			double val = (double) jsonData;
			return (float) val;
		} else {
			double val;
			if (System.Double.TryParse(jsonData.ToString(), out val)) {
				return (float) val;
			} else {
				return 0;
			}
		}
	}

	public static int GetValue(this JsonData jsonData, string key, int defaultValue) {
		if (jsonData.ContainsKey (key)) {
			return jsonData [key].ToInt ();
		} else {
			return defaultValue;
		}
	}
    public static long GetValue(this JsonData jsonData, string key, long defaultValue)
    {
        if (jsonData.ContainsKey(key))
        {
            if (jsonData[key].IsInt())
            {
                return (long)jsonData[key].ToInt();
            }
            return (long)jsonData[key];
        }
        else {
            return defaultValue;
        }
    }     
    public static bool GetValue(this JsonData jsonData, string key, bool defaultValue)
    {
        if (jsonData.ContainsKey(key) && jsonData[key].IsBoolean)
        {
            return (bool)jsonData[key];
        }
        else
        {
            return defaultValue;
        }
    }
	public static float GetValue(this JsonData jsonData, string key, float defaultValue) {
		if (jsonData.ContainsKey (key)) {
			return jsonData [key].ToFloat ();
		} else {
			return defaultValue;
		}
	}

	public static string GetValue(this JsonData jsonData, string key, string defaultValue) {
		if (jsonData.ContainsKey (key) && jsonData[key] != null) {
			return jsonData [key].ToString ();
		} else {
			return defaultValue;
		}
	}


	//判断是否存在某个KEY	
	public static bool ContainsKey(this JsonData jsonData, string key) {
		bool result = false;
		if(jsonData == null)
			return result;
		if(!jsonData.IsObject)
		{
			return result;
		}
		IDictionary tdictionary = jsonData as IDictionary;
		if(tdictionary == null)
			return result;
		if (tdictionary.Contains (key)) {
			return true;
		} else {
			return result;
		}	
	}


}

