using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBar : MonoBehaviour
{
    public abstract void Scale(float percentage);
    protected float ScaleWithinBounds(float start, float end, float percentage)
    {
        return Mathf.Lerp(start, end, percentage);
    }

    protected Vector3 MoveWithinBounds(Vector3 start, Vector3 end, float percentage)
    {
        return Vector3.Lerp(start, end, percentage);
    }
}
