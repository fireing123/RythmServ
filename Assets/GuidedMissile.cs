using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    public float speed = 10f;             // �⺻ �ӵ�
    public float homingDelay = 1f;        // ���� ���� �ð�
    public float rotateSpeed = 200f;      // ȸ��(����) �ӵ�
    public float damage = 10f;            // ���ݷ�

    private Transform target;
    private float timer;
    private bool isHoming = false;
    private Vector2 initialDirection; // �߻� ���� ����


    public void Initaial(float degree, float damageIndex)
    {
        damage *= damageIndex;
        transform.rotation = Quaternion.Euler(0, 0, degree);
        timer = homingDelay;
        float angleRad = degree * Mathf.Deg2Rad; // �������� ��ȯ
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
        initialDirection = direction; // �ʱ� ���� ����
    }

    private void Update()
    {
        if (!isHoming)
        {
            // Lerp�� �̿��� ���� ��ġ���� ��ǥ �������� �ε巴�� �̵�
            Vector2 targetPos = (Vector2)transform.position + initialDirection * speed * Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, targetPos, 1f); // t=1�̸� ��ǻ� �״�� �̵�

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                AcquireTarget();
                isHoming = true;
            }
        }
        else
        {
            // ���� ����
            if (target != null)
            {
                // ���� ���
                Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
                float rotateAmount = Vector3.Cross(direction, transform.right).z;

                // ȸ��
                transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

                // ������ �̵�
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                AcquireTarget();
                // ��ǥ ������ �׳� ����
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
    }

    private void AcquireTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            Hp hp = collision.GetComponent<Hp>();
            if (hp != null)
            {
                hp.SubHp(damage);
            }
            Destroy(gameObject); // �̻��� ����
        }
    }
}
