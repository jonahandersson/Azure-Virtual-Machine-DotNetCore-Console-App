# Azure-Virtual-Machine-DotNetCore-Console-App
- A simple .NET Core 3.1 console app in creating an Azure VM 
- An example of ARM template 

REQUIRED:
- Azure Subscription
- Azure Role Access that enables you to create VMs or add resources in a resource group in your Azure Subscription 
- Create Azure AD and Security Principal
- Nuget Package = Microft.Azure.Management.Fluent 
- An Azure Key Vault or a  .properties configuration file to read your Azure-related important credentials configuration related to your Azure subcription 
 
Prerequisites:
1. Create a resource group or add Azure VM to an existing Resource Group in the Azure Portal
2. Create Azure AD 
3. Register an application for your Azure Virtual Machine in Azure AD and add security service principal in your Azure Subscription
4. Take note of your Resource-Group name, desired Virtual Machine info that you want to create, Azure ADD app details like clientId, tenantId, subscriptionId, etc. 

RECOMMENDED DEVELOPER TOOLS:
- Visual Studio Code 
- Azure Fluent API 
- Azure VS Code Extensions 
- A Web Browser to Access the Azure Portal 
- A Windows Tool to Remote Access (RDP) Your Azure Virtual Machine 


