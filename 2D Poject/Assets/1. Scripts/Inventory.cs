using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    ItemButton[] buttons;

    private int selcetedItemIndex1 = -1;
    private int selcetedItemIndex2 = -1;

    void Awake()
    {
        // GetComponent's'InChildren
        buttons = gridLayoutGroup.GetComponentsInChildren<ItemButton>();

        ItemManager itemManager =  FindObjectOfType<ItemManager>();

        for (int i = 0; i < buttons.Length; i++)
        {
            var itemData = itemManager.itemDatas[Random.Range(0, itemManager.itemDatas.Count)];

            var idx = i;

            // OnClickItemButton(i) �� �ϸ� i �� �������ִ� �޸𸮸� �����ϱ⿡ (�ڹٽ�ũ��Ʈ Ŭ���� ����)
            buttons[i].GetComponent<Button>().onClick.AddListener(() => OnClickItemButton(idx));

            buttons[i].GetComponent<ItemButton>().ItemInfo = new ItemInfo(){ amount = 1, itemData = itemData };
        }
    }
    void OnClickItemButton(int index)
    {
        Debug.Log(index);

        // ó�� Ŭ����
        if (0 > selcetedItemIndex1)
        {
            // ó�� Ŭ���� �����۹�ư�� �ε����� ������
            selcetedItemIndex1 = index;
        }
        // �ι�° Ŭ����
        else if (0 > selcetedItemIndex2)
        {
            // �ι�°�� Ŭ���� �����۹�ư�� �ε����� ������
            selcetedItemIndex2 = index;

            var itemInfo1 = buttons[selcetedItemIndex1].ItemInfo;
            var itemInfo2 = buttons[selcetedItemIndex2].ItemInfo;

            // �Ӽ����� set �� �ִ� �Լ��� �̹��� ����
            buttons[selcetedItemIndex1].ItemInfo = itemInfo2;
            buttons[selcetedItemIndex2].ItemInfo = itemInfo1;

            selcetedItemIndex1 = -1;
            selcetedItemIndex2 = -1;
        }
    }
}
