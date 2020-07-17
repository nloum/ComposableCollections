namespace IoFluently
{
    public enum PathObservationMethod
    {
        FileSystemWatcher,
        FsWatchDefault,
        FsWatchFsEventsMonitor,
        FsWatchKQueueMonitor,
        FsWatchPollMonitor,
    }
}