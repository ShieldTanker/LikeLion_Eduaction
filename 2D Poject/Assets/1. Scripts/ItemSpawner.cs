using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    /// <summary>
    /// ���� ������ų ������Ʈ
    /// </summary>
    public GameObject itemPrefab;

    // ������ �����͵�
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
        // ������ġ�� ���� ������ų ������Ʈ�� item ������ �Ҵ�
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        SpawnedItem spawnedItem = item.GetComponent<SpawnedItem>();

        int randomIdx = Random.Range(0, itemData.Length);

        // ������ �������� ������ ����Ÿ�� ����
        spawnedItem.SetItemData(itemData[randomIdx]);

        // �͸��Լ� , ��������Ʈ �ϳ�
        // item�� ��������Ʈ�� �͸����� StartCoroutine(SpawnItem()); �� �����Ű�� �Լ��� �־��
        item.GetComponent<SpawnedItem>().OnDestroiedAction += () =>
        {
            Debug.Log("Item call");
            StartCoroutine(SpawnItem());
        };
    }
}