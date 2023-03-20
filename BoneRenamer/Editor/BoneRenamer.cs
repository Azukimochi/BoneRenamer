using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Xml.Linq;
using AvaterInfo;
using Unity.Collections;
using System;

public class BoneRenamer : EditorWindow
{
    static XMLLoader xml;
    static string ResultMessage;
    static GameObject TargetObj;
    static int SelectIndex = 0;
    static bool SelectUseStartWith;

    [MenuItem("Tools/BoneRenamer")]
    public static void BoneRename()
    {
        GetWindow<BoneRenamer> ();
    }
    private void OnGUI()
    {
        xml = new XMLLoader();
        GUILayout.MaxHeight(5);
        GUILayout.Label("ボーン名を変更したいオブジェクトを指定");

        TargetObj = (GameObject)EditorGUILayout.ObjectField("GameObject", TargetObj, typeof(GameObject), true);

        GUILayout.Label("");
        string[] avaterNames = xml.GetAvaterNames();

        GUILayout.Label("変更先のアバター名");
        var index = avaterNames.Length > 0 ? EditorGUILayout.Popup(SelectIndex, avaterNames) : -1;

        if (index != SelectIndex)
        {
            SelectIndex = index;
        }
        SelectUseStartWith = GUILayout.Toggle(SelectUseStartWith, "先頭一致でリネーム");
        if (GUILayout.Button("適用"))
        {
            int matchCount = 0;
            if (TargetObj == null)
                ResultMessage = "Objectが設定されていません";
            else
                if (BoneRenameProcess(TargetObj, ref matchCount))
                {
                    ResultMessage = "正常に処理が終了しました ボーン置換数:" + matchCount;
                    Debug.Log("正常終了,ボーン置換数：" + matchCount);
                }
               
        }
        GUILayout.Label(ResultMessage);
    }
    private static bool BoneRenameProcess (GameObject obj, ref int matchCount)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        
        if (children.childCount == 0) {
            return false;
        }
        foreach (Transform ob in children)
        {
            string tName = ob.gameObject.name;
            foreach (XMLLoader.keys key in Enum.GetValues(typeof(XMLLoader.keys))) 
            {
                string[] bones = xml.GetBoneNameFromKey(key);

                //先頭一致非使用
                if(! SelectUseStartWith)
                {
                    for (int i = 0; i < bones.Length; i++)
                    {
                        //Debug.Log("検索:" +tName + "/ "+ bones[i]);
                        if (tName == bones[i])
                        {
                            Debug.Log("置換:" + tName + " -> " + bones[SelectIndex]);
                            ob.gameObject.name = bones[SelectIndex];
                            matchCount++;
                            break;
                        }
                    }
                }
                //先頭一致使用
                else
                {
                    string rep = "";
                    for (int i = 0; i < bones.Length; i++)
                    {
                        if (tName.Equals(bones[SelectIndex]))
                            continue;
                        //Debug.Log("検索:" +tName + "/ "+ bones[i]);
                        if (tName.StartsWith(bones[i]))
                        {
                            //Debug.Log("合致:" + tName + " / " + bones[i]);
                            if (rep.Length <= bones[i].Length)  
                                rep = bones[i];
                        }
                    }
                    if(!rep.Equals(""))
                    {
                        Debug.Log("置換：" + tName+ " : " + rep + " -> " + bones[SelectIndex]);
                        matchCount++;
                        ob.gameObject.name = tName.Replace(rep, bones[SelectIndex]);
                    }
                }
            }
            BoneRenameProcess(ob.gameObject, ref matchCount);
        }
        return true;
    }
}
