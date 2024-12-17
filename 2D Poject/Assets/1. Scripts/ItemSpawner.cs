using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    /// <summary>
    /// 씬에 생성시킬 오브젝트
    /// </summary>
    public GameObject itemPrefab;

    // 아이템 데이터들
    public ItemData[] itemData;

    public float minSpawnTime;
    public float maxSpawnTime;

    void Start()
    {
        SpawnItemCallback();
    }

    IEnumerator SpawnItem()
    {
        float nextRandomTime = Random.Range(minSpawnTime, maxSpawnTime);

        yield return new WaitForSeconds(nextRandomTime);

        SpawnItemCallback();
    }

    private void SpawnItemCallback()
    {
        // 현재위치에 씬에 생성시킬 오브젝트를 item 변수에 할당
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        SpawnedItem spawnedItem = item.GetComponent<SpawnedItem>();

        int randomIdx = Random.Range(0, itemData.Length);

        // 스폰할 아이템을 아이템 데이타로 설정
        spawnedItem.SetItemData(itemData[randomIdx]);

        // 익명함수 , 델리게이트 하나
        // item의 델리게이트에 익명으로 StartCoroutine(SpawnItem()); 를 실행시키는 함수를 넣어둠
        item.GetComponent<SpawnedItem>().OnDestroiedAction += () =>
        {
            Debug.Log("Item call");
            StartCoroutine(SpawnItem());
        };
    }
}