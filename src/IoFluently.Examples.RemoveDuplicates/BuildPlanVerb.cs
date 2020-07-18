using System.Collections.Generic;
using CommandLine;

namespace IoFluently.Examples.RemoveDuplicates
{
    [Verb("plan")]
    public class BuildPlanVerb
    {
        public BuildPlanVerb(IEnumerable<string> rootFolders, string planPath)
        {
            RootFolders = rootFolders;
            PlanPath = planPath;
        }

        [Value(0)]
        public IEnumerable<string> RootFolders { get; }
        
        [Option('p', "plan", Required = true)]
        public string PlanPath { get; }
    }
}