using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image itemImage; 

    private ItemInfo itemInfo;
    public ItemInfo ItemInfo
    {
        get { return itemInfo; }
        set { itemInfo = value; SetItemImage(itemInfo.itemData.icon); }
    }

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;

        if (sprite == null )
        {
            // 알파값 없애기
            var color = itemImage.color;

            // 알파값 제거
            color.a = 0;
            itemImage.color = color;
        }
        else
        {
            // 알파값 보이기
            var color = itemImage.color;

            // 알파값 조정
            color.a = 1.0f;
            itemImage.color = color;
        }
    }
}
