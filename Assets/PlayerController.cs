// 2025-08-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using Cysharp.Threading.Tasks;
using System;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityEvent death;
    public SkillCoolTimeViewer sv;
    [HideInInspector]
    public ShockWave skillShockWave;
    [HideInInspector]
    public PlayerDash skillDash;
    [HideInInspector]
    public MissileShootor skillMissile;
    [HideInInspector]
    public Barrier skillBarrier;

    public float barrierCooldown;
    public float missileShootorCooldown;
    public float shockWaveCooldown;
    public float dashColldown;

    bool canDash = true;
    bool canMissile = true;
    bool canBarrier = true;
    bool canShockWave = true;

    bool isShockWave = false;

    public float powerIndex;
    public float bodyDamage;
    public float speed = 5f; // 이동 속도
    private Rigidbody2D rb;
    private Hp hp;
    private Vector2 moveInput;
    private Vector2 dashInput;
    private float anglerDamping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<Hp>();
        anglerDamping = rb.angularDamping;

        skillDash = GetComponent<PlayerDash>();
        skillMissile = GetComponent<MissileShootor>();
        skillShockWave = GetComponent<ShockWave>();
        skillBarrier = GetComponent<Barrier>();

    }

    private void Update()
    {
        sv.Show(canShockWave, canDash, canBarrier, canMissile);

        if (hp.GetHp() == 0)
        {
            Death();
        }
    }

    void Death()
    {
        int score = Mathf.Max(
            PointCountor.GetPoint(),
            PointCountor.ReadPoint()
        );

        PointCountor.SavePoint(score);

        death.Invoke();
    }

    // InputSystem의 Move 액션 값을 처리하는 메서드
    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        if (moveInput != Vector2.zero)
        {
            dashInput = moveInput;
        }
    }

    public void OnShockWave(InputValue inputValue)
    {
        if (canBarrier)
        {
            canBarrier = false;

            skillBarrier.CreateBarrier(powerIndex);

            _ = WaitRunning(barrierCooldown / powerIndex, () =>
            {
                canBarrier = true;
            });
        }
    }

    public void OnAttack(InputValue inputValue)
    {
        if (canMissile)
        {
            canMissile = false;

            skillMissile.SpawnMissiles(powerIndex);
            AttackEffect();

            _ = WaitRunning(missileShootorCooldown / powerIndex, () =>
            {
                canMissile = true;
            });
        }
    }

    public void OnChargeAttack(InputValue inputValue)
    {
        if (isShockWave)
        {
            isShockWave = false;
            skillShockWave.ShockWaveCharged(powerIndex);

            ChargeAttackEnd();

            _ = WaitRunning(shockWaveCooldown / powerIndex, () =>
            {
                canShockWave = true;
            });
        }
    }

    public void OnCharging(InputValue inputValue)
    {
        if (canShockWave)
        {
            canShockWave = false;
            isShockWave = true;
            ChargeAttackStart();
        }
    }
    
    public void OnDash(InputValue inputValue)
    {
        if (canDash)
        { 
            canDash = false;
            skillDash.Dashor(rb, dashInput, powerIndex);

            _ = WaitRunning(dashColldown / powerIndex, () =>
            {
                canDash = true;
            });
        }
    }

    private void ChargeAttackStart()
    {
        skillShockWave.ChargeStart();

        rb.angularDamping = 0;
        rb.angularVelocity = -400;
    }

    private void ChargeAttackEnd()
    {
        skillShockWave.ChargeEffectCancel();
        rb.angularDamping = anglerDamping;
    }

    private void AttackEffect()
    {
        rb.angularVelocity = 500;
    }

    async UniTask WaitRunning(float coolDown, Action then)
    {
        int coolDownMs = SecondToMilliSecond(coolDown);
        await UniTask.Delay(coolDownMs, cancellationToken: this.GetCancellationTokenOnDestroy());
        then();
    }

    private int SecondToMilliSecond(float second)
    {
        return Mathf.RoundToInt(second * 1000);
    }
    private void FixedUpdate()
    {
        if (skillDash.IsDashing)
        {
            return;
        }

        // Rigidbody2D를 사용한 이동 처리
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Hp hp = collision.GetComponent<Hp>();
            if (hp != null)
            {
                hp.SubHp(bodyDamage);
            }
        }
    }
}