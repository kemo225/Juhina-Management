using FluentScheduler;

namespace Juhyna_Api.Serviecs
{
    public static class SheduleRunner
    {
        public static void StartShecdule()
        {
            JobManager.Initialize(new Juhyna_BLL.FluentScedule.JopRegistry());
        }
    }
}
