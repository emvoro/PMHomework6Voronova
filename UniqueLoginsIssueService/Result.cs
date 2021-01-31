using System;

namespace UniqueLoginsIssueService
{
    public class Result
    {
        public int Successful { get; private set; }

        public int Failed { get; private set; }

        public Result(int successful, int failed)
        {
            Successful = successful;
            Failed = failed;
        }
    }
}
