using System;
using UnityEngine;

[Serializable]
public class UIPanelInfo: ISerializationCallbackReceiver//与JsonUtility对应
{
    [NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString;
    //{
    //    get
    //    {
    //        return panelType.ToString();
    //    }
    //    set
    //    {
    //        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), value);
    //        panelType=type;
    //    }
    //}
    public string path;

    //反序列化，从文本信息到对象
    public void OnAfterDeserialize()
    {
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType=type;
    }

    //序列化
    public void OnBeforeSerialize()
    {

    }
}
