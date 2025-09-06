using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerDash : Dash
{
    public void Dashor(Rigidbody2D rb, Vector2 dashValue, float damageIndex)
    {
        _ = DashPlayer(rb, dashValue, damageIndex);
        StartCoroutine(DashEffect());
    }

    private async UniTask DashPlayer(Rigidbody2D rb, Vector2 dashValue, float damageIndex)
    {
        var (start, end) = await DashMove(rb, dashValue);

        DashAttack(start, end, power * damageIndex);
    }
}
