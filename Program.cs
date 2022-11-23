using Pulumi;

static void RunPulumi()
{
    var (rg, svc) = apim.resources.ApiManagement.Create();
    apim.resources.ContainerApp.Create(rg);
}

return await Deployment.RunAsync(() =>
{
    RunPulumi();
});