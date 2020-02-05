using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnapsackPanel : BasePanel
{
    public override void OnEnter()
    {
        CanvasGroupValue.alpha = 0f;
        CanvasGroupValue.DOFade(1, 0.5f);
    }

    public override void OnExit()
    {
        CanvasGroupValue.DOFade(0, 0.5f);
    }
    public void OnItemButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }
}
