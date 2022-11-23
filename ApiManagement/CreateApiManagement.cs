using System;
using AzureNative = Pulumi.AzureNative;

namespace apim.ApiManagement;

public static class ApiManagement
{
    public static (AzureNative.Resources.ResourceGroup, AzureNative.ApiManagement.ApiManagementService) Create()
    {
        var resourceGroup = new AzureNative.Resources.ResourceGroup("mygroup");
        var apiManagementService = new AzureNative.ApiManagement.ApiManagementService("rg-wu2-apim", new()
        {
            PublisherEmail = "ameer.deen@katasec.com",
            PublisherName = "Ameer Deen",
            ResourceGroupName = resourceGroup.Name,
            Sku = new AzureNative.ApiManagement.Inputs.ApiManagementServiceSkuPropertiesArgs
            {
                Capacity = 0,
                Name = "Consumption",
            },
            VirtualNetworkType = "None",
        });

        return (resourceGroup, apiManagementService);
    }
}
