using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Cube : CollectableBaseModel
{
    public int colorId;
    public bool canCollect;
    public float heightOffset;
    [SerializeField] private Renderer renderer;
    [SerializeField] private List<Material> materials;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        switch (colorId)
        {
            case 0:
                renderer.material = materials[0];
                break;
            case 1:
                renderer.material = materials[1];
                break;
            case 2:
                renderer.material = materials[2];
                break;
        }
    }

    public override void OnCollect()
    {
        if (canCollect)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y - heightOffset, 0);
            canCollect = false;
        }
    }

    public void OnMatch(Action onComplete)
    {
        transform.DOScale(1.7f, .2f).OnComplete(() =>
        {
            transform.DOScale(0f, .4f).OnComplete(() =>
            {
                onComplete?.Invoke();
                gameObject.SetActive(false);
            });
        });
    }

    public void CollectScaleAnimation(float offsetTime)
    {
        DOVirtual.DelayedCall(offsetTime, () =>
        {
            transform.DOScale(1.7f, .2f).OnComplete(() =>
            {
                transform.DOScale(1.5f, .2f);
            });
        });

    }
}
