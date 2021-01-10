using System;
using System.Diagnostics;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Network.Fluent.Models;

namespace Azure_Virtual_Machine_DotNetCore_Console_App
{
    class Program
    {        static void Main(string[] args)
        {
            Console.WriteLine("Azure Virtual VM Creation using .NET Core 3.1!");
            
            //Create the management client. This will be used for all the operations that we will perform in Azure.
            var credentials = SdkContext.AzureCredentialsFactory
                                        .FromFile("./azureauth.properties");

            var azure = Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();

            //Temp variables for Azure VM creation. Refactor your private data better way.
            //Create a resource group in Azure Portal set name resource group name below. 
            
            var groupName = MyAzureVM.groupName;
            var vmName = MyAzureVM.vmName; 
            var location = Region.EuropeNorth;
            var vNetName = MyAzureVM.vNetName;
            var vNetAddress = MyAzureVM.vNetAddress; 
            var subnetName =  MyAzureVM.subnetName;
            var subnetAddress = MyAzureVM.subnetAddress;
            var nicName =  MyAzureVM.nicName;
            var adminUser = MyAzureVM.adminUser;
            var adminPassword =  MyAzureVM.adminPassword;
            var myNetworkSecurityGroup = MyAzureVM.myNetworkSecurityGroup;
            var publicIPAddress = MyAzureVM.publicIPAddress;
            
            //Just a stopwatch to check elapse time on VM creation
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Console.WriteLine($"Creating resource group {groupName} ...");
            var resourceGroup = azure.ResourceGroups.Define(groupName)
                .WithRegion(location)
                .Create();

            //Every virtual machine needs to be connected to a virtual network.
            Console.WriteLine($"Creating Virtual Network {vNetName} ...");
            var network = azure.Networks.Define(vNetName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithAddressSpace(vNetAddress)
                .WithSubnet(subnetName, subnetAddress)
                .Create();
             
                
            // Create Public IP address for Azure VM. Necessary to be able to access VM 
             Console.WriteLine($"Creating Public IP Address {publicIPAddress} ...");
             var vmPublicIPAddress = azure.PublicIPAddresses.Define(publicIPAddress)
                    .WithRegion(location)
                    .WithExistingResourceGroup(resourceGroup)
                    .Create();

             //Add network security group for securing access to Azure VM
            Console.WriteLine($"Creating Network Security Group {myNetworkSecurityGroup} ...");
            var networkSecurityGroup = azure.NetworkSecurityGroups.Define(myNetworkSecurityGroup)
                     .WithRegion(location)
                     .WithExistingResourceGroup(groupName)
                     .Create();
                     
             // Network Security Group - 
            Console.WriteLine($"Creating a Security Rule for allowing the remote access");
            networkSecurityGroup.Update()
                .DefineRule("Allow-RDP")
                    .AllowInbound()
                    .FromAnyAddress()
                    .FromAnyPort()
                    .ToAnyAddress()
                    .ToPort(3389)
                    .WithProtocol(SecurityRuleProtocol.Tcp)
                    .WithPriority(100)
                    .WithDescription("Allow-RDP")
                    .Attach()
                .Apply();
         

            //Network interface for VM and for connecting to the virtual network
            Console.WriteLine($"Creating Network Interface {nicName} ...");
            var nic = azure.NetworkInterfaces.Define(nicName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet(subnetName)
                .WithPrimaryPrivateIPAddressDynamic()
                .WithNewPrimaryPublicIPAddress(publicIPAddress)
                .WithExistingNetworkSecurityGroup(networkSecurityGroup)
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
                      
            Console.WriteLine($"Created {vmName} ...");
           
            sw.Stop();
            Console.WriteLine($"Created {sw.Elapsed}");

            //Delay
            Console.ReadLine();
        }
    }
}
