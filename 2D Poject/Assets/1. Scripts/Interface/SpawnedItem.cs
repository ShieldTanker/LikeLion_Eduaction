using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public Action OnDestroiedAction;
    public ItemData itemData;

    public void SetItemData(ItemData itemData)
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        this.itemData = itemData;
    }

    private void OnDestroy()
    {
        /* ���� �ڵ�
        if (OnDestroiedAction != null)
        {
            OnDestroiedAction.Invoke();
        }*/

        // OnDestroy() �� ȣ��� �� OnDestroiedAction(Action �̶�� ��������Ʈ,OnDestroiedAction ������)�� �Լ����� ������ ���������� ����
        OnDestroiedAction?.Invoke();
    }
}
