using UnityEngine;

public class TimerTask
{
    private float interval;
    private float timer;

    public TimerTask(float interval)
    {
        this.interval = interval;
        timer = Time.time;
    }

    // �� ������ ȣ��
    public bool RunPeriodicTask()
    {
        if (timer + interval < Time.time)
        {
            timer = Time.time;
            return true;
        }
        return false;
    }
}
