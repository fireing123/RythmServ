using UnityEngine;

public class ChaserEnemy : Enemy
{
    public float moveSpeed;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (player == null) return;

        // �÷��̾ ���� õõ�� �̵�
        Vector2 dir = (player.transform.position - transform.position).normalized;
        transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
    }
}
