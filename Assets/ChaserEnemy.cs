using UnityEngine;

public class ChaserEnemy : Enemy
{
    public float moveSpeed;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (player == null) return;

        // 플레이어를 향해 천천히 이동
        Vector2 dir = (player.transform.position - transform.position).normalized;
        transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
    }
}
