using CommandLine;

namespace IoFluently.Examples.RemoveDuplicates
{
    [Verb("execute")]
    public class ExecutePlanVerb
    {
        public ExecutePlanVerb(string planPath)
        {
            PlanPath = planPath;
        }

        [Option('p', "plan", Required = true)]
        public string PlanPath { get; }
    }
}