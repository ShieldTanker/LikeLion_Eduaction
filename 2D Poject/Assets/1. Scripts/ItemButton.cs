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
            // ���İ� ���ֱ�
            var color = itemImage.color;

            // ���İ� ����
            color.a = 0;
            itemImage.color = color;

            // ���� ����
            tmpText.text = "";
        }
        else
        {
            // ���İ� ���̱�
            var color = itemImage.color;

            // ���İ� ����
            color.a = 1.0f;
            itemImage.color = color;

            tmpText.text = "" + item.amount;
        }
    }
}
