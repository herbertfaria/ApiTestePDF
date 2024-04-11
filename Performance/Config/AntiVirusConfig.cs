using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace Performance;

public class AntiVirusConfig: ManualConfig
{
    public AntiVirusConfig()
    {
        AddJob(
               Job.MediumRun
                  .WithToolchain(InProcessNoEmitToolchain.Instance)
                  .WithStrategy(BenchmarkDotNet.Engines.RunStrategy.Throughput)
              )
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
    }
}
