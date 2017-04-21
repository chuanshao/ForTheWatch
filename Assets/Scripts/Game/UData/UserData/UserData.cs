using UnityEngine;
using System.Collections;
using LitJson;

public class UserData : WData
{
    public string UserUid;
    public int Pos;
    public string PicUrl;
    public string Name;
    public override void ParseJson(JsonData jdata)
    {

    }
}
