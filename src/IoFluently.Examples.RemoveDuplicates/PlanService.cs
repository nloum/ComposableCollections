using System.Linq;

namespace IoFluently.Examples.RemoveDuplicates
{
    public class PlanService : ICommandLineService<BuildPlanVerb>
    {
        private readonly IIoService _ioService;
        private readonly IRemoveDuplicatesService _service;

        public PlanService(IRemoveDuplicatesService service, IIoService ioService)
        {
            _service = service;
            _ioService = ioService;
        }

        public void Run(BuildPlanVerb verb)
        {
            var duplicates = _service.FindDuplicates(verb.RootFolders.Select(x => _ioService.ParseAbsolutePath(x)));

            var planFile = _ioService.ParseAbsolutePath(verb.PlanPath).AsDuplicateRemovalPlan();
            planFile.Write(duplicates);
        }
    }
}