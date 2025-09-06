using System.Collections;
using Unity.AppUI.UI;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public ParticleSystem ps;


    public ParticleSystem chargeEffect;
    public ParticleSystem lastChargeEffect;



    public float waveRadius;
    public float wavePower;
    public float attackPower;
    public float chargedPower;
    public float chargeTime;
    
    private float chargeTimeStart;
    private Coroutine coroutine;


    public void ShockWaveAttack(float damageIndex)
    {
        ShockWaver(waveRadius, wavePower, attackPower * damageIndex);
        ShockWaveEffect(waveRadius);
    }

    public void ShockWaveCharged(float damageIndex)
    {
        float time = Mathf.Clamp(Time.time - chargeTimeStart, 0, chargeTime);

        float radius = waveRadius * time;
        float power = wavePower * time;
        ShockWaver(radius, power, chargedPower * damageIndex);
        ShockWaveEffect(radius);
    }

    public void ChargeStart()
    {
        var corutine = StartCoroutine(ChargeEffectStart());
        coroutine = corutine;
    }

    private IEnumerator ChargeEffectStart()
    {
        chargeTimeStart = Time.time;
        chargeEffect.Play();
        yield return new WaitForSeconds(chargeTime - 0.5f);
        chargeEffect.Stop();
        lastChargeEffect.Play();
    }

    public void ChargeEffectCancel()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        chargeEffect.Stop();
    }

    private void ShockWaveEffect(float radius)
    {
        var sz = ps.sizeOverLifetime;
        sz.enabled = true;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.1f);
        curve.AddKey(0.75f, radius * 25);

        sz.size = new ParticleSystem.MinMaxCurve(1.5f, curve);

        ps.Play();
    }

    private void ShockWaver(float radius, float power, float damage)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") == false) 
                continue;

            Vector2 vector = collider.transform.position - transform.position;
            Vector2 force = CalculateForce(vector, radius, power);
            ObjectPush(collider.gameObject, force);
            AttackEnemy(collider.gameObject, damage);
        }
    }

    private Vector2 CalculateForce(Vector2 value, float radius, float power)
    {
        float distance = value.magnitude;
        Vector2 direct = value.normalized;

        float distanceFactor = 1 / distance * distance;

        float force = distanceFactor * power;

        return direct * force;
    }

    private void ObjectPush(GameObject go, Vector2 power)
    {
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null) 
            rb.AddForce(power);
    }

    private void AttackEnemy(GameObject go, float damage)
    {
        var hp = go.GetComponent<Hp>();
        hp.SubHp(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, waveRadius);
    }
}
