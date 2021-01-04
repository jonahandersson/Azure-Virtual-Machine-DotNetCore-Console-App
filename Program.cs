using System;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Azure_Virtual_Machine_DotNetCore_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Virtual VM Creation using .NET Core 3.1!");
            
            //Create the management client. This will be used for all the operations that we will perform in Azure.
            var credentials = SdkContext.AzureCredentialsFactory
                                        .FromFile("./azureauth.properties");

            var azure = Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();

            //First of all, we need to create a resource group where we will add all the resources needed for the virtual machine
            var groupName = "rg-dev-azurevm-demo-dotnetcore";
            var vmName = "azvmDemo";
            var location = Region.EuropeNorth;
            var vNetName = "azvmdemoVNET";
            var vNetAddress = "172.16.0.0/16";
            var subnetName = "azvmdemoSubnet";
            var subnetAddress = "172.16.0.0/24";
            var nicName = "azvmDemoNIC";
            var adminUser = "azureadminuser";
            var adminPassword = "Pa$$w0rd!2021";

            Console.WriteLine($"Creating resource group {groupName} ...");
            var resourceGroup = azure.ResourceGroups.Define(groupName)
                .WithRegion(location)
                .Create();

            //Every virtual machine needs to be connected to a virtual network.
            Console.WriteLine($"Creating virtual network {vNetName} ...");
            var network = azure.Networks.Define(vNetName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithAddressSpace(vNetAddress)
                .WithSubnet(subnetName, subnetAddress)
                .Create();

            //Any virtual machine need a network interface for connecting to the virtual network
            Console.WriteLine($"Creating network interface {nicName} ...");
            var nic = azure.NetworkInterfaces.Define(nicName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet(subnetName)
                .WithPrimaryPrivateIPAddressDynamic()
                .Create();

            //Create the virtual machine
            Console.WriteLine($"Creating virtual machine {vmName} ...");
            azure.VirtualMachines.Define(vmName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetworkInterface(nic)
                .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer","2019-Datacenter")
                .WithAdminUsername(adminUser)
                .WithAdminPassword(adminPassword)
                .WithComputerName(vmName)
                .WithSize(VirtualMachineSizeTypes.StandardB1s)
                .Create();
            {

            }           
             Console.WriteLine($"Created {vmName} ...");
        }
    }
}
