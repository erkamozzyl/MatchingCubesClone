using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : CollectableBaseModel
{
    public int colorId;
    public bool canCollect;
    public float heightOffset;

    public override void OnCollect()
    {
        if (canCollect)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y - heightOffset, 0);
            canCollect = false;
        }
    }

    public void ScaleAnimation(float offsetTime)
    {
        DOVirtual.DelayedCall(offsetTime, () =>
        {
            transform.DOScale(1.7f, .2f).onComplete += () =>
            {
                transform.DOScale(1.5f, .2f);
            };
        });

    }
}
