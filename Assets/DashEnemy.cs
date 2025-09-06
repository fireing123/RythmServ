using UnityEngine;

public class DashEnemy : Enemy
{
    public float dashPower;
    public float dashTime;

    public Dash dash;
    private TimerTask timerTask;

    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();

        timerTask = new TimerTask(dashTime);
    }

    protected override void Update()
    {
        base.Update();
        if (player == null) return;

        // �÷��̾ ���� õõ�� �̵�
        
        if (timerTask.RunPeriodicTask())
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            _ = dash.DashMove(rb, dir * dashPower);
        }
    }
}
