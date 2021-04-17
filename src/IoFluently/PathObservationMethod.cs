namespace IoFluently
{
    /// <summary>
    /// IoFluently supports different ways of observing paths on different platforms. This enum is used to tell IoFluently
    /// which method to use.
    /// </summary>
    public enum PathObservationMethod
    {
        /// <summary>
        /// Use the FileSystemWatcher that is built into .NET. This has bugs on some non-Windows platforms.
        /// </summary>
        FileSystemWatcher,
        
        /// <summary>
        /// Use fswatch with its default monitor.
        /// </summary>
        FsWatchDefault,

        /// <summary>
        /// Use fswatch with its the FS events monitor.
        /// </summary>
        FsWatchFsEventsMonitor,
        
        /// <summary>
        /// Use fswatch with its Kqueue monitor.
        /// </summary>
        FsWatchKQueueMonitor,
        
        /// <summary>
        /// Use fswatch with its poll monitor.
        /// </summary>
        FsWatchPollMonitor,
    }
}