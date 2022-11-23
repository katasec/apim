namespace apim.resources;
using AzureNative = Pulumi.AzureNative;

public static class ContainerApp {
    public static void Create(AzureNative.Resources.ResourceGroup rg)
    {
        var vnet = new AzureNative.Network.VirtualNetwork("vnet-01-", new AzureNative.Network.VirtualNetworkArgs
        {
            ResourceGroupName = rg.Name,
            AddressSpace = new AzureNative.Network.Inputs.AddressSpaceArgs {AddressPrefixes = "10.0.0.0/16" }
        });

        var subNet1 = new AzureNative.Network.Subnet("snet-01", new AzureNative.Network.SubnetArgs
        {
            ResourceGroupName = rg.Name,
            VirtualNetworkName = vnet.Name,
            AddressPrefix ="10.0.0.0/23"
        });

        var subNet2 = new AzureNative.Network.Subnet("snet-02", new AzureNative.Network.SubnetArgs
        {
            ResourceGroupName= rg.Name,
            VirtualNetworkName = vnet.Name,
            AddressPrefix = "10.0.2.0/23"
        });


        var managedEnvironment = new AzureNative.App.ManagedEnvironment("dev-", new AzureNative.App.ManagedEnvironmentArgs
        {
            ResourceGroupName = rg.Name,
            VnetConfiguration = new AzureNative.App.Inputs.VnetConfigurationArgs
            {
                RuntimeSubnetId = subNet1.Id,
                InfrastructureSubnetId = subNet2.Id
            },
            ZoneRedundant = false
        }) ;

        var containerApp = new AzureNative.App.ContainerApp("containerApp", new()
        {
            Configuration = new AzureNative.App.Inputs.ConfigurationArgs
            {
                Ingress = new AzureNative.App.Inputs.IngressArgs
                {
                    External = true,
                    TargetPort = 80,
                    //Traffic = new[]
                    //{
                    //    new AzureNative.App.Inputs.TrafficWeightArgs
                    //    {
                    //        Label = "production",
                    //        RevisionName = "testcontainerApp0-ab1234",
                    //        Weight = 100,
                    //    },
                    //},
                },
            },
            ContainerAppName = "weatherforecast",
            ManagedEnvironmentId = managedEnvironment.Id,
            ResourceGroupName = rg.Name,
            Template = new AzureNative.App.Inputs.TemplateArgs
            {
                Containers = new[]
                {
                    new AzureNative.App.Inputs.ContainerArgs
                    {
                        Image = "writeameer/weatherforecast:0.0.2",
                        Name = "weatherforecast",
                    },
                },
            },
        });

    }
}