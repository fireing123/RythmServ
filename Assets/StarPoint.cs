using UnityEngine;

public enum StarType
{
    HP,
    HP_INCREASE,
    DAMAGE_INCREASE
}

public class StarPoint : MonoBehaviour
{
    public StarType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var hp = collision.GetComponent<Hp>();
            switch (type) {
                case StarType.HP:
                    hp.AddHp(25);
                    break;
                case StarType.HP_INCREASE:
                    hp.maxHp += 10;
                    hp.AddHp(10);
                    break;
                case StarType.DAMAGE_INCREASE:
                    var player = collision.GetComponent<PlayerController>();
                    player.powerIndex *= 1.02f;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
