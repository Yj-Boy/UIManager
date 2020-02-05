using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }     
    }

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if(canvasTransform==null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, string> panelPathDict;//所有面板的Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板身上的base组件
    private Stack<BasePanel> panelStack;

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    /// <summary>
    /// 把某个页面入栈，即显示界面
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        //判断一下栈是否为空
        if(panelStack==null)
        {
            panelStack = new Stack<BasePanel>();
        }

        //判断一下栈里面是否有页面，有的话将页面暂停
        if(panelStack.Count>0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        //获得要显示面板的BasePanel
        BasePanel panel = GetPanel(panelType);
        //调用BasePanel的OnEnter函数
        panel.OnEnter();
        //显示的面板入栈
        panelStack.Push(panel);
    }
    /// <summary>
    /// 把某个页面出栈，即移除
    /// </summary>
    public void PopPanel()
    {
        if(panelStack==null)
        {
            panelStack = new Stack<BasePanel>();
        }

        if (panelStack.Count <= 0) return;

        //关闭栈顶页面显示
        BasePanel basePanel = panelStack.Pop();
        basePanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel = panelStack.Peek();
        topPanel.OnResume();
    }
    /// <summary>
    /// 根据面板类型，得到实例化面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        //如果面板对象字典为空，初始化
        if(panelDict==null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);

        //对Direction类进行了扩展，扩展出TryGet方法，简化TryGetValue
        BasePanel panel = panelDict.TryGet(panelType);

        //如果面板还没实例化出来找不到，那么就找这个面板的路径，然后根据prefab去实例化面板
        if (panel==null)
        {
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);

            string path = panelPathDict.TryGet(panelType);

            GameObject instPanel = GameObject.Instantiate(Resources.Load(path))as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    [SerializeField]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        //从Rosources中读取json文件
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        //将json文件反序列
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach(UIPanelInfo info in jsonObject.infoList)
        {
            panelPathDict.Add(info.panelType, info.path);
        }
    }
}
