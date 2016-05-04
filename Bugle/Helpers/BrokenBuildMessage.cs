using System;

namespace SevanConsulting.Bugle.Helpers
{
    public class BrokenBuildMessage
    {
        public string BuildDefinitionName { get; set; }
        public string BuildNumber { get; set; }
        public DateTime? BuildDate { get; set; }
    }
}
