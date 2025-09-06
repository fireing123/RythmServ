using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    public float speed = 10f;             // 기본 속도
    public float homingDelay = 1f;        // 직진 유지 시간
    public float rotateSpeed = 200f;      // 회전(조향) 속도
    public float damage = 10f;            // 공격력

    private Transform target;
    private float timer;
    private bool isHoming = false;
    private Vector2 initialDirection; // 발사 직진 방향


    public void Initaial(float degree, float damageIndex)
    {
        damage *= damageIndex;
        transform.rotation = Quaternion.Euler(0, 0, degree);
        timer = homingDelay;
        float angleRad = degree * Mathf.Deg2Rad; // 라디안으로 변환
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
        initialDirection = direction; // 초기 진행 방향
    }

    private void Update()
    {
        if (!isHoming)
        {
            // Lerp를 이용해 현재 위치에서 목표 방향으로 부드럽게 이동
            Vector2 targetPos = (Vector2)transform.position + initialDirection * speed * Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, targetPos, 1f); // t=1이면 사실상 그대로 이동

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                AcquireTarget();
                isHoming = true;
            }
        }
        else
        {
            // 유도 시작
            if (target != null)
            {
                // 방향 계산
                Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
                float rotateAmount = Vector3.Cross(direction, transform.right).z;

                // 회전
                transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

                // 앞으로 이동
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                AcquireTarget();
                // 목표 없으면 그냥 직진
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
            Destroy(gameObject); // 미사일 제거
        }
    }
}
