using UnityEngine;

public class MissileShootor : MonoBehaviour
{
    public GameObject missilePrefab;
    public int missileCount;
    public void SpawnMissiles(float damageIndex)
    {
        SpawnMissile(Mathf.RoundToInt(missileCount * damageIndex), damageIndex);
    }

    private void SpawnMissile(int count, float damageIndex)
    {
        float degree = 360 / count;
        float angle = 0;
        for (int i = 0; i < count; i++)
        {
            angle += degree;
            var go = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            go.TryGetComponent(out GuidedMissile guidedMissile);
            guidedMissile.Initaial(angle, damageIndex);
        }
    }
}
