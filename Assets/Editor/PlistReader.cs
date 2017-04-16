using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using LitJson;

public class PlistReader : Editor {

    [MenuItem("MyTool/ExportPngByPlist")]
    static void ExportPngByPlist()
    {
        string path = "Assets/Prefabs/card.png";
        string plistPath = "Prefabs/card.txt";
        Texture2D tex = ReadFileTex(path);
        string json = ReadFileString(Application.dataPath + "/" + plistPath);
        JsonData jdata = JsonMapper.ToObject(json);
        JsonData keyArr = jdata["plist"]["dict"]["dict"][0]["key"];
        JsonData dict = jdata["plist"]["dict"]["dict"][0]["dict"];
        for (int i = 0; i < dict.Count; i++)
        {
            var dictItem = dict[i];
            var stringJson = (string)dictItem["string"][0];
            var replaces = stringJson.Replace("{", "");
            replaces = replaces.Replace("}", "");
            string[] num = replaces.Split(',');
            var px = int.Parse(num[0]);
            var py = int.Parse(num[1]);
            var pw = int.Parse(num[2]);
            var ph = int.Parse(num[3]);
            exportPng(tex, (string)keyArr[i], px, py, pw, ph);
        }
    }
    static void exportPng(Texture2D image , string fileName , int x , int y , int w , int h)
    {
        Texture2D myimage = new Texture2D(w, h);
        for (int px = x; px < x + w; px++)
        {
            for (int py = y; py < y + h; py++)//Y轴像素
            {
                myimage.SetPixel(px - x, h - (py - y), image.GetPixel(px, 1024 - py));//05 06 19  22 26 29
            }
        }
       

        //转换纹理到EncodeToPNG兼容格式
        //if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
        //{
        //    Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
        //    newTexture.SetPixels(myimage.GetPixels(0), 0);
        //    myimage = newTexture;
        //}
        var pngData = myimage.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/" + "Prefabs" + "/" + fileName + ".PNG", pngData);
    }
	// Use this for initialization
	void Start () {
		
	}
    /// 从对应文件读取数据
    public static string ReadFileString(string filePath)
    {
        return File.ReadAllText(filePath);
    }
    public static Texture2D ReadFileTex(string filePath)
    {
        Texture2D myTexture = AssetDatabase.LoadAssetAtPath(filePath , typeof(Texture2D)) as Texture2D;
        Texture2D myimage = new Texture2D(97, 121);
        for (int x = 0; x < 97; x++)
        {
            int startX = x + 2;
            for (int y = 0; y < 121; y++)
            {
                int startY = 1022 - y;
                myimage.SetPixel(x, 121 - y, myTexture.GetPixel(startX, startY));
            }
        }
        var pngData = myimage.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + "Prefabs" + "/" + "test" + ".PNG", pngData);
        return myTexture;
    }
    // Update is called once per frame
    void Update () {
		
	}

    static XmlDocument LoadXml(string path)
    {
        //创建xml文档
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
        xml.Load(XmlReader.Create(path, set));
        //得到objects节点下的所有子节点

        return xml;
    }
}
