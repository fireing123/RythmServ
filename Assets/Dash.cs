using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public TrailRenderer tr;

    public float radius;
    public float power;

    public bool CanDash {  get; private set; } = true;
    public bool IsDashing {  get; private set; }

    private float dashingPower = 32f;
    private float dashingTime = 0.15f;

    public async UniTask<(Vector2, Vector2)> DashMove(Rigidbody2D rb, Vector2 dashValue)
    {
        CanDash = false;
        IsDashing = true;
        Vector2 startPoint = transform.position;
        rb.linearVelocity = dashValue.normalized * dashingPower;

        int dasingMs = SecondToMilliSecond(dashingTime);
        await UniTask.Delay(dasingMs, cancellationToken: this.GetCancellationTokenOnDestroy());
        IsDashing = false;
        Vector2 endPoint = transform.position;
        return (startPoint, endPoint);
    }

    protected void DashAttack(Vector2 start, Vector2 end, float damage)
    {
        ColliderCheck(start, end, radius, out var colliders);
        DashDamage(colliders, damage);
    }

    private int SecondToMilliSecond(float second)
    {
        return Mathf.RoundToInt(second * 1000);
    }

    private void DashDamage(Collider2D[] colliders, float damage) 
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") == false)
                continue;

            AttackEnemy(collider.gameObject, damage);
        }
    }

    private void ColliderCheck(Vector2 start, Vector2 end, float radius, out Collider2D[] colliders)
    {
        Vector2 mid = (start + end) * 0.5f;

        float length = Vector2.Distance(start, end);

        Vector2 size = new Vector2(length + 2f * radius, 2f * radius);

        float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;

        colliders = Physics2D.OverlapCapsuleAll(mid, size, CapsuleDirection2D.Horizontal, angle);
    }

    private void AttackEnemy(GameObject go, float damage)
    {
        var hp = go.GetComponent<Hp>();
        hp.SubHp(damage);
    }

    protected IEnumerator DashEffect()
    {
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3.right * dashingPower * dashingTime);

        DrawCapsule2DGizmo(start, end, radius, Color.red);
    }

    // Ä¸½¶ ±âÁî¸ð (3D ÁÂÇ¥ »ç¿ë, Z=0)
    private void DrawCapsule2DGizmo(Vector3 start, Vector3 end, float r, Color color)
    {
        Gizmos.color = color;
        Vector3 dir = (end - start).normalized;
        Vector3 n = new Vector3(-dir.y, dir.x, 0); // 2D¿ë Á÷±³ º¤ÅÍ

        Gizmos.DrawLine(start + n * r, end + n * r);
        Gizmos.DrawLine(start - n * r, end - n * r);
        Gizmos.DrawWireSphere(start, r);
        Gizmos.DrawWireSphere(end, r);
    }
}
