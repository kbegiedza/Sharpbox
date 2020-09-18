using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace Sharpbox.Performance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var coreJob = Job.Default
                             .WithToolchain(CsProjCoreToolchain.NetCoreApp50);
            var config = DefaultConfig.Instance
                                      .AddJob(coreJob)
                                      .AddDiagnoser(MemoryDiagnoser.Default);

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                             .Run(args, config);
        }
    }
}
