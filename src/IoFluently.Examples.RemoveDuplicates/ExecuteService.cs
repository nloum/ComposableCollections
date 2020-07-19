namespace IoFluently.Examples.RemoveDuplicates
{
    public class ExecuteService : ICommandLineService<ExecutePlanVerb>
    {
        private readonly IIoService _ioService;
        private readonly IRemoveDuplicatesService _service;

        public ExecuteService(IRemoveDuplicatesService service, IIoService ioService)
        {
            _service = service;
            _ioService = ioService;
        }

        public void Run(ExecutePlanVerb verb)
        {
            var planFile = _ioService.ParseAbsolutePath(verb.PlanPath).AsDuplicateRemovalPlan();
            var plan = planFile.Read();

            _service.Execute(plan);
        }
    }
}