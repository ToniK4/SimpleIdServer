# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

#SimpleIdServer links

SimpleIdServer
https://github.com/simpleidserver/SimpleIdServer

CaseManagement - BPMN and HumanTask engine
https://github.com/simpleidserver/CaseManagement

#OAuth rfc


#Scim rfc's
https://www.rfc-editor.org/rfc/rfc7642.html
https://www.rfc-editor.org/rfc/rfc7643.html
https://www.rfc-editor.org/rfc/rfc7644.html

#Local files
To debug, you can switch the nuget packages for the local files.
(A folder named SimpleIdServer with the full cloned repository https://github.com/simpleidserver/SimpleIdServer.git should be located in the same directory as BluePoles.IdentityProvider)
To do this Launch the BluePoles.IdentityProvider_LocalFiles solution and change SimpleIdServer.OpenID.SqlServer.Startup by commenting out these nuget packages: 

<PackageReference Include="SimpleIdServer.UI.Authenticate.Email" Version="2.0.17" />
<PackageReference Include="SimpleIdServer.UI.Authenticate.LoginPassword" Version="2.0.17" />
<PackageReference Include="SimpleIdServer.UI.Authenticate.Sms" Version="2.0.17" />
<PackageReference Include="SimpleIdServer.OpenID.EF" Version="2.0.17" />
<PackageReference Include="SimpleIdServer.Saml.Sp" Version="2.0.17" />

and replacing them with local project files:
		<ProjectReference Include="..\..\..\SimpleIdServer\src\Saml\SimpleIdServer.Saml.Sp\SimpleIdServer.Saml.Sp.csproj"/>
		<ProjectReference Include="..\..\..\SimpleIdServer\src\UI\SimpleIdServer.UI.Authenticate.Email\SimpleIdServer.UI.Authenticate.Email.csproj"/>
		<ProjectReference Include="..\..\..\SimpleIdServer\src\UI\SimpleIdServer.UI.Authenticate.LoginPassword\SimpleIdServer.UI.Authenticate.LoginPassword.csproj"/>
		<ProjectReference Include="..\..\..\SimpleIdServer\src\UI\SimpleIdServer.UI.Authenticate.Sms\SimpleIdServer.UI.Authenticate.Sms.csproj"/>
		<ProjectReference Include="..\..\..\SimpleIdServer\src\OpenID\SimpleIdServer.OpenID.EF\SimpleIdServer.OpenID.EF.csproj"/>
<ProjectReference Include="..\SimpleIdServer.OpenID.EF\SimpleIdServer.OpenID.EF.csproj"/>

If you're still getting a null exception (parameter 'type') delete the database and run the sqlserver project again.