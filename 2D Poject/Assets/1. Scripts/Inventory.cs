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

            // OnClickItemButton(i) 로 하면 i 가 가지고있는 메모리를 추적하기에 (자바스크립트 클로저 개념)
            buttons[i].GetComponent<Button>().onClick.AddListener(() => OnClickItemButton(idx));

            buttons[i].GetComponent<ItemButton>().ItemInfo = new ItemInfo(){ amount = 1, itemData = itemData };
        }
    }
    void OnClickItemButton(int index)
    {
        Debug.Log(index);

        // 처음 클릭시
        if (0 > selcetedItemIndex1)
        {
            // 처음 클릭한 아이템버튼의 인덱스를 가져옴
            selcetedItemIndex1 = index;
        }
        // 두번째 클릭시
        else if (0 > selcetedItemIndex2)
        {
            // 두번째로 클릭한 아이템버튼의 인덱스를 가져옴
            selcetedItemIndex2 = index;

            var itemInfo1 = buttons[selcetedItemIndex1].ItemInfo;
            var itemInfo2 = buttons[selcetedItemIndex2].ItemInfo;

            // 속성에서 set 에 있는 함수로 이미지 변경
            buttons[selcetedItemIndex1].ItemInfo = itemInfo2;
            buttons[selcetedItemIndex2].ItemInfo = itemInfo1;

            selcetedItemIndex1 = -1;
            selcetedItemIndex2 = -1;
        }
    }
}
