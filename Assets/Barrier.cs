using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    public GameObject traiangle;
    public int barrierCount;
    public float barrierRadius;
    private ShockWave shockwave;
    [HideInInspector]
    public int barrierCnt = -1;

    private float damageIndex;

    private void Start()
    {
        shockwave = GetComponent<ShockWave>();
    }

    private void Update()
    {
        if (barrierCnt == 0)
        {
            Death();
            barrierCnt = -1;
        }
    }

    public void CreateBarrier(float _damageIndex)
    {
        damageIndex = _damageIndex;
        int count = Mathf.RoundToInt(barrierCount * damageIndex);
        Create(count, barrierRadius);
    }

    private void Create(int count, float radius)
    {
        barrierCnt = count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Quaternion rot = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            GameObject tri = Instantiate(traiangle, pos, rot, transform);

            tri.TryGetComponent(out BarrierTraiangle ang);
            ang.Init(this);
        }
    }

    private void Death()
    {
        shockwave.ShockWaveAttack(damageIndex);
    }
}
