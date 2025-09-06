using UnityEngine;

public class HpSpawner : MonoBehaviour
{
    public GameObject[] healPrefabs; // 3개의 체력회복 프리팹 등록
    public Transform player;         // 플레이어 Transform
    public int spawnCount = 5;       // 뿌릴 개수
    public float spawnRadius = 15f;   // 플레이어 주변 반경
    public float spawnInterval = 15f;

    void Start()
    {
        // 일정 주기로 SpawnHealItems 실행
        InvokeRepeating(nameof(SpawnHealItems), 0f, spawnInterval);
    }

    public void SpawnHealItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 랜덤 오브젝트 선택
            GameObject prefab = healPrefabs[Random.Range(0, healPrefabs.Length)];

            // 원형 반경 내 랜덤 위치
            Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = player.position + new Vector3(randomPos.x, randomPos.y, 0f);

            // 생성
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}
