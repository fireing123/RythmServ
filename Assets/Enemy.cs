using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bodyDamage;
    private Hp hp;
    protected GameObject player;

    protected virtual void Start()
    {
        hp = GetComponent<Hp>();
        player = GameObject.FindWithTag("Player");
    }

    private void Death()
    {
        PointCountor.AddPoint(1);
        Destroy(transform.parent.gameObject);
    }

    protected virtual void Update()
    {
        if (hp.GetHp() == 0)
        {
            Death();
        }
    }

    public void Attack(GameObject go, float damage)
    {
        go.TryGetComponent(out Hp hp);
        hp.SetHp(hp.GetHp() - damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hp hp = collision.GetComponent<Hp>();
            if (hp != null)
            {
                hp.SubHp(bodyDamage);
            }
        }
    }
}
