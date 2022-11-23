using Pulumi;

static void RunPulumi()
{
    var (rg, svc) = apim.ApiManagement.ApiManagement.Create();
}

return await Deployment.RunAsync(() =>
{
    RunPulumi();
});