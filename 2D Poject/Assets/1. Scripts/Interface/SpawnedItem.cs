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
        /* 같은 코드
        if (OnDestroiedAction != null)
        {
            OnDestroiedAction.Invoke();
        }*/

        // OnDestroy() 가 호출될 때 OnDestroiedAction(Action 이라는 델리게이트,OnDestroiedAction 변수명)에 함수들이 있으면 순차적으로 실행
        OnDestroiedAction?.Invoke();
    }
}
