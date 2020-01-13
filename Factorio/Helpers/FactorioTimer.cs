using System;
using System.Collections.Generic;

namespace FactorioMod.Factorio.Helpers
{
    public class FactorioTimer
    {
        private static FactorioTimer _instance;
        public static FactorioTimer Instance => _instance ?? (_instance = new FactorioTimer());

        public static void Tick()
        {
            Instance.InstanceTick();
        }

        public static ulong SubscribeAlarm(int timeInMilliseconds, Action action)
        {
            return Instance.InstanceSubscribeAlarm(timeInMilliseconds, action);
        }

        public static void RegisterOnUpdate(Action action)
        {
            Instance.Update += action;
        }

        public static void UnregisterOnUpdate(Action action)
        {
            Instance.Update -= action;
        }

        public ulong _time;
        private readonly Dictionary<ulong, Action> _alarms;
        private Action Update;

        public FactorioTimer()
        {
            _time = 0;
            _alarms = new Dictionary<ulong, Action>();
        }

        public void InstanceTick()
        {
            _time++;
            Update?.Invoke();
            if (_alarms.ContainsKey(_time))
            {
                _alarms[_time].Invoke();
                _alarms.Remove(_time);
            }
        }

        public ulong InstanceSubscribeAlarm(int timeInMilliseconds, Action action)
        {
            ulong eventTime = _time + ToFrameCount(timeInMilliseconds);
            if (_alarms.ContainsKey(eventTime))
            {
                _alarms[eventTime] += action;
            }
            else
            {
                _alarms.Add(eventTime, action);
            }

            return eventTime;
        }

        public ulong ToFrameCount(int timeInMilliseconds) => (ulong)((timeInMilliseconds * 60) / 1000);
    }
}
