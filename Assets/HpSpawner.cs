using UnityEngine;

public class HpSpawner : MonoBehaviour
{
    public GameObject[] healPrefabs; // 3���� ü��ȸ�� ������ ���
    public Transform player;         // �÷��̾� Transform
    public int spawnCount = 5;       // �Ѹ� ����
    public float spawnRadius = 15f;   // �÷��̾� �ֺ� �ݰ�
    public float spawnInterval = 15f;

    void Start()
    {
        // ���� �ֱ�� SpawnHealItems ����
        InvokeRepeating(nameof(SpawnHealItems), 0f, spawnInterval);
    }

    public void SpawnHealItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // ���� ������Ʈ ����
            GameObject prefab = healPrefabs[Random.Range(0, healPrefabs.Length)];

            // ���� �ݰ� �� ���� ��ġ
            Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = player.position + new Vector3(randomPos.x, randomPos.y, 0f);

            // ����
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}
