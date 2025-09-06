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

    // 매 프레임 호출
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
