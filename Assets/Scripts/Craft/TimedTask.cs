using System;

public abstract class TimedTask{
    public DateTime startTime{set;get;}
    public int taskTime{set;get;}
    public TimedTask(int taskTime){
        startTime = DateTime.Now;
        this.taskTime = taskTime;
    }

    public string GetRemainingTimeFormatted(){
        DateTime now = DateTime.Now;
        TimeSpan remainingTime = startTime.AddSeconds(taskTime).Subtract(now);
        if(remainingTime.Ticks > 0)
            return new DateTime(remainingTime.Ticks).ToString("HH:mm:ss");
        else
            return "00:00:00";
    }

    public bool IsTaskCompleted(){
        DateTime now = DateTime.Now;
        TimeSpan remainingTime = startTime.AddSeconds(taskTime).Subtract(now);
        return remainingTime.Ticks <= 0;
    }

    public int GetRemainingTimeSecond(){
        DateTime now = DateTime.Now;
        TimeSpan remainingTime = startTime.AddSeconds(taskTime).Subtract(now);
        return (int)remainingTime.TotalSeconds;
    }
    public abstract TaskCompleteMessage CompleteTask();
}