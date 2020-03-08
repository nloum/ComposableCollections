namespace MoreIO
{
    public enum PathObservationMethod
    {
        Default,
        FileSystemWatcher,
        FsWatchDefault,
        FsWatchFsEventsMonitor,
        FsWatchKQueueMonitor,
        FsWatchPollMonitor,
    }
}