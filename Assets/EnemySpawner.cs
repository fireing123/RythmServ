using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject normalPrefab;
    public GameObject tankPrefab;
    public GameObject chargerPrefab;
    public GameObject staticPrefab;

    public float spawnInterval = 2f;
    private float timer = 0f;
    private int wave = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        wave++;
        int count = Mathf.Min(5 + wave, 50); // 점점 많이 생성

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * 10f;
            EnemyType type = ChooseType(wave);

            GameObject prefab = GetPrefab(type);
            if (prefab == null) continue;

            GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        }

        // 특수 케이스: 일정 웨이브마다 Static 무더기 방패 생성
        if (wave % 5 == 0)
        {
            Vector2 center = (Vector2)player.position + Random.insideUnitCircle.normalized * 12f;
            for (int j = 0; j < 8; j++)
            {
                Vector2 offset = Quaternion.Euler(0, 0, j * 45) * Vector2.right * 2f;
                GameObject wall = Instantiate(staticPrefab, center + offset, Quaternion.identity);
            }
        }
    }

    EnemyType ChooseType(int wave)
    {
        float r = Random.value;
        if (r < 0.5f) return EnemyType.Normal;
        if (r < 0.7f) return EnemyType.Tank;
        if (r < 0.9f) return EnemyType.Charger;
        return EnemyType.Static;
    }

    GameObject GetPrefab(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal: return normalPrefab;
            case EnemyType.Tank: return tankPrefab;
            case EnemyType.Charger: return chargerPrefab;
            case EnemyType.Static: return staticPrefab;
        }
        return null;
    }
}