using UnityEngine;

public class Hp : MonoBehaviour
{
    public float maxHp;
    public float currentHp;

    public float GetHp()
    {
        return currentHp;
    }

    public void SetHp(float hp)
    {
        currentHp = Mathf.Clamp(hp, 0, maxHp);
    }

    public void SubHp(float _hp)
    {
        SetHp(GetHp() - _hp);
    }
    public void AddHp(float _hp)
    {
        SetHp(GetHp() + _hp);
    }


    public float GetHpPercent()
    {
        return currentHp / maxHp;
    }
}
