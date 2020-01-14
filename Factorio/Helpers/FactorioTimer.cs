using System;
using System.Collections.Generic;

namespace FactorioMod.Factorio.Helpers
{
    public class FactorioTimer
    {
        private static FactorioTimer _instance;
        public static FactorioTimer Instance => _instance = _instance ?? new FactorioTimer();

        public static void Tick()
        {
            Instance.InstanceTick();
        }

        public void InstanceTick()
        {
            _time++;

            if (_alarms.ContainsKey(_time))
            {
                _alarms[_time].Invoke();
                _alarms.Remove(_time);
            }
        }

        public static Func<double> SubscribeAction(int timeInMilliseconds, Action action)
        {
            return Instance.InstanceSubscribeAlarm(timeInMilliseconds, action);
        }

        public ulong _time;
        private readonly Dictionary<ulong, Action> _alarms;

        public FactorioTimer()
        {
            _time = 0;
            _alarms = new Dictionary<ulong, Action>();
        }

        public Func<double> InstanceSubscribeAlarm(int timeInMilliseconds, Action action)
        {
            ulong startTime = _time;
            ulong eventTime = startTime + ToFrameCount(timeInMilliseconds);

            if (startTime == eventTime)
            {
                eventTime += 5;
            }

            if (_alarms.ContainsKey(eventTime))
            {
                _alarms[eventTime] += action;
            }
            else
            {
                _alarms.Add(eventTime, action);
            }

            return () => (double)(_time - startTime) / (eventTime - startTime);
        }

        public ulong ToFrameCount(int timeInMilliseconds) => (ulong)((timeInMilliseconds * 60) / 1000);
    }
}
