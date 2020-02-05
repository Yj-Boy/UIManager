using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaskPanel : BasePanel
{
    public override void OnEnter()
    {
        Vector3 temp = transform.position;
        temp.x = 1000;
        transform.position = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }

    public override void OnExit()
    {
        transform.DOLocalMoveX(1000, 0.5f);
    }
}
