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
            // ���İ� ���ֱ�
            var color = itemImage.color;

            // ���İ� ����
            color.a = 0;
            itemImage.color = color;
        }
        else
        {
            // ���İ� ���̱�
            var color = itemImage.color;

            // ���İ� ����
            color.a = 1.0f;
            itemImage.color = color;
        }
    }
}
