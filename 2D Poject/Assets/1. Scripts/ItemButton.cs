using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text tmpText;

    private ItemInfo itemInfo;

    public ItemInfo ItemInfo
    {
        get { return itemInfo; }
        set { itemInfo = value; SetItemData(itemInfo); }
    }

    public void SetItemData(ItemInfo item)
    {
        if (item != null)
            itemImage.sprite = item.itemData.icon;

        if (item == null )
        {
            // 알파값 없애기
            var color = itemImage.color;

            // 알파값 제거
            color.a = 0;
            itemImage.color = color;

            // 개수 없음
            tmpText.text = "";
        }
        else
        {
            // 알파값 보이기
            var color = itemImage.color;

            // 알파값 조정
            color.a = 1.0f;
            itemImage.color = color;

            tmpText.text = "" + item.amount;
        }
    }
}
