using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Azure_Virtual_Machine_DotNetCore_Console_App
{
   public static class MyAzureVM{     
       //Create a resource group in Azure Portal set name resource group name below. RG where we will add all the resources needed for the virtual machine

      public static string groupName = "rg-dev-azurevm-demo-dotnetcore";       
      public static string vmName = "azVMDemoCore"; 
  
      public static string vNetName = "azvmdemoVNET";
      public static string  vNetAddress = "172.16.0.0/16";
      public static string  subnetName = "azvmdemoSubnet";
      public static string subnetAddress = "172.16.0.0/24"; 
      public static string nicName = "azvmDemoNIC";
      public static string adminUser = "azureadminuser";
      public static string adminPassword = "Pa$$w0rd!2021";
      public static string  myNetworkSecurityGroup = "myNSGforAzureVM";

   }
}