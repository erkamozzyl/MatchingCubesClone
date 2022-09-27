using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Cube : ObjectModel
{
    public int id;
    public int colorId;
    public bool canCollect;
    public GameObject parentObject;
    public float heightOffset;
    [SerializeField] private Renderer renderer;
    [SerializeField] private List<Material> materials;
    public CubeItemDataModel GetData()
    {
        CubeItemDataModel dataModel = new CubeItemDataModel();
        dataModel.id = id;
        dataModel.position = parentObject.transform.position;
        dataModel.rotation = parentObject.transform.rotation;
        dataModel.colorID = colorId;
        return dataModel;
    }

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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

    public  void OnCollect()
    {
        if (canCollect)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y - heightOffset, 0);
            canCollect = false;
        }
    }
    public void AddVector3ToPosition(Vector3 position)
    {
        transform.localPosition += position;
    }
    public void ScaleAnimation(float value, Action onComplete)
    {
        transform.DOScale(1.7f, .2f).OnComplete(() =>
        {
            transform.DOScale(value, .4f).OnComplete(() =>
            {
                onComplete?.Invoke();
                gameObject.SetActive(false);
            });
        });
    }

    public void ScaleAnimation(float offsetTime, float value)
    {
        DOVirtual.DelayedCall(offsetTime, () =>
        {
            transform.DOScale(1.7f, .2f).OnComplete(() =>
            {
                transform.DOScale(value, .2f);
            });
        });

    }
}
