using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageBar : UIBar
{
    private RectTransform rectTrans;
    private Vector3 startPos;
    private Vector3 endPos;
    [SerializeField] private Vector3 endPosOffset;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();

        startPos = rectTrans.localPosition;
        endPos = startPos + endPosOffset;
    }


    public override void Scale(float percentage)
    {
        rectTrans.localPosition = MoveWithinBounds(startPos,endPos, percentage);
    }
}
