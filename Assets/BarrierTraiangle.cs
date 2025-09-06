using UnityEngine;

public class BarrierTraiangle : MonoBehaviour
{
    public int damage;
    public Barrier barrier;

    public void Init(Barrier br)
    {
        barrier = br;
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
            
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        barrier.barrierCnt--;
    }
}
