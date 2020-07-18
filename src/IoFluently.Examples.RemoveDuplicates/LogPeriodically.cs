using System;

namespace IoFluently.Examples.RemoveDuplicates
{
    public class LogPeriodically
    {
        public TimeSpan LogPeriod { get; }
        private DateTimeOffset? _lastLoggedTimestamp = DateTimeOffset.UtcNow;

        public LogPeriodically(TimeSpan logPeriod)
        {
            LogPeriod = logPeriod;
        }

        public bool HasBeenLongEnough()
        {
            var now = DateTimeOffset.UtcNow;
            
            if (_lastLoggedTimestamp == null)
            {
                _lastLoggedTimestamp = now;
                return true;
            }

            if ((now - _lastLoggedTimestamp.Value) >= LogPeriod)
            {
                _lastLoggedTimestamp = now;
                return true;
            }

            return false;
        }
    }
}