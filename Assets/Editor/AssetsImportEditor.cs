using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
public class AssetFileInfo
{
    public string fileName;
    public string filePath;
    public uint crc;
    public bool isDel = false;
    public bool isExport = false;
}
[Flags]
public enum ExportType
{
    Prefab = 0x000001,
    Mat = 0x000002,
    Png = 0x000004,
}
public class AssetsImportEditorWindow : EditorWindow
{
    private string currentTaget = string.Empty;
    private ExportType exportType = ExportType.Prefab;
    private List<AssetFileInfo[]> windowList = new List<AssetFileInfo[]>();
    private List<string> filesPath = new List<string>();
    /// <summary>
    /// 导入UI预制体
    /// </summary>
    [MenuItem("MyWindow/ExportUI")]
    static void ExportUi()
    {
        AssetsImportEditorWindow window = new AssetsImportEditorWindow();
        window.minSize = new Vector2(350, 260f);
        window.title = "导出UI预制体";
        window.currentTaget = "UI";
        window.exportType = ExportType.Prefab;
        window.ShowWindow();
        window.Show();
    }
    [MenuItem("MyWindow/ExportIcon")]
    static void ExportIcon()
    {
        AssetsImportEditorWindow window = new AssetsImportEditorWindow();
        window.minSize = new Vector2(350, 260f);
        window.title = "导出ICON图片";
        window.currentTaget = "Icon";
        window.exportType = ExportType.Png;
        window.ShowWindow();
        window.Show();
    }
    private void OnGUI()
    {
        if (windowList.Count == 0) return;
        var sameTextStyle = GetStyle(new Color(0f, 0.8f, 0));
        EditorGUILayout.BeginHorizontal(GetStyle(Color.black));
        GUILayout.Label("文件名", GetStyle(Color.black) , GUILayout.Height(26), GUILayout.Width(180));
        GUILayout.Label("prefab CRC", GetStyle(Color.black), GUILayout.Height(26), GUILayout.Width(120));
        GUILayout.Label("resources CRC", GetStyle(Color.black), GUILayout.Height(26), GUILayout.Width(120));
        GUILayout.Space(30);
        if (GUILayout.Button("全部导出"))
        {
            ExportAllFile();
            ShowWindow();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < windowList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            AssetFileInfo[] infos = windowList[i];
            uint crc0 = infos[0] == null ? 0 : infos[0].crc;
            uint crc1 = infos[1] == null ? 0 : infos[1].crc;
            string fileName = infos[0] == null ? infos[1].fileName : infos[0].fileName;
            GUILayout.Label(fileName, GetStyle(Color.black), GUILayout.Height(26), GUILayout.Width(180));
            GUILayout.Label("" + crc0, GetStyle(Color.black), GUILayout.Height(26), GUILayout.Width(120));
            GUILayout.Label("" + crc1, GetStyle(Color.black), GUILayout.Height(26), GUILayout.Width(120));
            if (crc0 == 0)  //外部没有这个文件
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.normal.textColor = new Color(1f, 0.5f, 0);
                if (infos[1].isDel == false)
                {
                    if (GUILayout.Button("删除", style, GUILayout.Width(120)))
                    {
                        DeletePre(infos[1].filePath);
                        infos[1].isDel = true;
                    }
                }
                else
                {
                    GUILayout.Button("已删除", style, GUILayout.Width(120));
                }
            }
            else if (crc0 == crc1) //外部有 内部也有且没有修改
            {
                GUILayout.Label("匹配", sameTextStyle, GUILayout.Width(120));
            }
            else
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.normal.textColor = new Color(1f, 0f, 0);
                if (infos[0].isExport == false)
                {
                    if (GUILayout.Button("导出", style, GUILayout.Width(120)))
                    {
                        var target = "Assets/Resources/" + currentTaget + "/";
                        if (ExportFile(infos[0].filePath, target))
                        {
                            infos[0].isExport = true;
                            AssetDatabase.Refresh();
                        }
                    }
                }
                else
                {
                    GUILayout.Button("已导出", style, GUILayout.Width(120));
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

    }
    void DeletePre(string path)
    {
        AssetDatabase.DeleteAsset(path);
    }
    /// <summary>
    /// 匹配两个列表，把相同的放在一起 , 没有相同的单独放一个
    /// </summary>
    List<AssetFileInfo[]> MatchFileInfo(List<AssetFileInfo> li1 , List<AssetFileInfo> li2)
    {
        var li2Copy = new List<AssetFileInfo>();
        for (int s = 0; s < li2.Count; s++)
        {
            li2Copy.Add(li2[s]);
        }
        int max = Mathf.Max(li1.Count, li2.Count);
        var returnData = new List<AssetFileInfo[]>();
        for (int i = 0; i < li1.Count; i++)
        {
            AssetFileInfo info1 = li1[i];
            AssetFileInfo[] item = new AssetFileInfo[2];
            returnData.Add(item);
            item[0] = info1;
            for (int j = 0; j < li2Copy.Count; j++)
            {
                AssetFileInfo info2 = li2Copy[j];
                if (info1.fileName == info2.fileName)
                {
                    item[1] = info2;
                    li2Copy.RemoveAt(j);
                    break;
                }
            }
        }
        for (int q = 0; q < li2Copy.Count; q++)
        {
            AssetFileInfo[] item = new AssetFileInfo[2];
            item[1] = item[q];
            returnData.Add(item);
        }
        return returnData;
    }
    GUIStyle GetStyle(Color color)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        return style;
    }
    public void ShowWindow()
    {
        string outsidePath = "Assets/Prefabs/" + this.currentTaget + "/";  //外部文件地址
        string insidePath = "Assets/Resources/" + this.currentTaget + "/"; //导进来放置的地址
        var li1 = GetAllFileInformation(outsidePath);
        var li2 = GetAllFileInformation(insidePath);
        windowList = MatchFileInfo(li1, li2);
    }
    List<AssetFileInfo> GetAllFileInformation(string rootPath)
    {
        var paths = GetDirectoryFilePath(rootPath);
        List<AssetFileInfo> fileInfos = new List<AssetFileInfo>();
        for (int i = 0; i < paths.Count; i++)
        {
            var path = paths[i];
            var fileName = Path.GetFileName(path);
            AssetFileInfo fileInfo = new AssetFileInfo();
            fileInfo.fileName = fileName;
            fileInfo.filePath = path;
            fileInfo.crc = GetFileCrc(path);
            fileInfos.Add(fileInfo);
        }
        return fileInfos;
    }
    /// <summary>
    /// 获取文件夹下面所有文件,除去meta
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    List<string> GetDirectoryFilePath(string path)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        if (!direction.Exists)
        {
            return new List<string>();
        }
        FileInfo[] files = direction.GetFiles();
        DirectoryInfo[] directions = direction.GetDirectories();
        List<string> paths = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            if (!FileNeedExport(files[i].Name))
            {
                continue;
            }
            paths.Add(path + files[i].Name);
        }
        for (int j = 0; j < directions.Length; j++)
        {
            var dirPath = path + "/" + directions[j].Name + "/";
            paths.AddRange(GetDirectoryFilePath(dirPath));
        }
        return paths;
    }
    bool FileNeedExport(string name)
    {
        if ((exportType & ExportType.Mat) != 0 && name.EndsWith(".mat"))
        {
            return true;
        }
        if ((exportType & ExportType.Prefab) != 0 && name.EndsWith(".prefab"))
        {
            return true;
        }
        if ((exportType & ExportType.Png) != 0 && name.EndsWith(".png"))
        {
            return true;
        }
        return false;
    }

    void ExportAllFile()
    {
        string outsidePath = "Assets/Prefabs/" + this.currentTaget + "/";  //外部文件地址
        string insidePath = "Assets/Resources/" + this.currentTaget + "/"; //导进来放置的地址
        var paths = GetDirectoryFilePath(outsidePath);
        ExportAllFile(paths, insidePath);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paths">目标文件</param>
    /// <param name="target">目标文件夹</param>
    void ExportAllFile(List<string> paths , string target)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (!ExportFile(paths[i], target))
            {
                break;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="res">资源文件</param>
    /// <param name="target">目标文件夹</param>
    /// <returns></returns>
    bool ExportFile(string res , string target)
    {
        DirectoryInfo info = new DirectoryInfo(target);
        if (!info.Exists)
        {
            EditorUtility.DisplayDialog("编辑器", "请创建" + target + "目录", "确定");
            return false;
        }
        var targetFile = target + Path.GetFileName(res);
        var assPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(res);
        DestroyAnimatorComponent(assPrefab); //删除多余组件
        DestroyMeshColliderComponent(assPrefab);
        AssetDatabase.CopyAsset(res, targetFile);
        AssetImporter import = AssetImporter.GetAtPath(targetFile);
        import.assetBundleName = currentTaget + "/" + Path.GetFileNameWithoutExtension(targetFile);
        return true;
    }
    void DestroyMeshColliderComponent(GameObject go)
    {
        if (go != null && go.GetComponent<MeshCollider>() != null)
        {
            GameObject.DestroyImmediate(go.GetComponent<MeshCollider>());
        }
    }
    void DestroyAnimatorComponent(GameObject go)
    {
        if (go != null && go.GetComponent<Animator>() != null)
        {
            GameObject.DestroyImmediate(go.GetComponent<Animator>());
        }
    }

    private uint GetFileCrc(string path)
    {
        System.Security.Cryptography.MD5 hash = System.Security.Cryptography.MD5.Create();
        FileStream file = new FileStream(path, FileMode.Open);
        byte[] hashbyte = hash.ComputeHash(file);
        file.Close();
        return ((uint)hashbyte[0] << 24) | ((uint)hashbyte[1] << 16) | ((uint)hashbyte[2] << 8) | (uint)hashbyte[3];
    }
}
