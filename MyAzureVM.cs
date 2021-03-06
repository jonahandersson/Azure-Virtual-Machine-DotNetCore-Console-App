using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Azure_Virtual_Machine_DotNetCore_Console_App
{
   public static class MyAzureVM{     
    
      public static string groupName = "rg-dev-azurevm-demo-dotnetcore";       
      public static string vmName = "azVMDemoCore";   
      public static string vNetName = "azvmdemoVNET";
      public static string  vNetAddress = "172.16.0.0/16";
      public static string  subnetName = "azvmdemoSubnet";
      public static string subnetAddress = "172.16.0.0/24"; 
      public static string nicName = "azvmDemoNIC";
      public static string adminUser = "<Your Admin User Here>"; //TODO secure passwords with Azure Key Vault!
      public static string adminPassword = "<Your Secure Password for VM here>"; //TODO secure passwords with Azure Key Vault!
      public static string  myNetworkSecurityGroup = "myNSGforAzureVM";
      public static string  publicIPAddress= "azVMPublicIP";

   }
}