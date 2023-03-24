namespace Galaga;
using System;
using System.Timers;
///<summary>This is used to make auto shot possible</summary>
public class TimedEvent {
    private static System.Timers.Timer aTimer;

    public bool timerOn {get; private set;} = false;

    private int interval;
    private bool eventActive = false;

    public TimedEvent(int interval) {
        this.interval = interval; //Event Interval
    }

    private void start() {
        aTimer = new System.Timers.Timer(interval);
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Start();
        timerOn = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e){
        eventActive = true;
        aTimer.Stop();
    }

    private void stop() {
        aTimer.Stop();
        aTimer.Dispose();
        timerOn = false;
        eventActive = false;
    }

    public void startStop() {
        if (timerOn) {
            stop();
        } else {
            start();
        }
    }

    public bool EventIsActive() {
        if (eventActive && timerOn) {
            eventActive = false;
            aTimer.Start();
            return true;
        }
        return false;
    }
}
