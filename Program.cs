using Pulumi;

static void RunPulumi()
{
    var (rg, svc) = apim.resources.ApiManagement.Create();
}

return await Deployment.RunAsync(() =>
{
    RunPulumi();
});