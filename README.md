# SimpleIdServer links

SimpleIdServer
https://github.com/simpleidserver/SimpleIdServer

CaseManagement - BPMN and HumanTask engine
https://github.com/simpleidserver/CaseManagement

# OAuth rfc


# Scim rfc's
https://www.rfc-editor.org/rfc/rfc7642.html

https://www.rfc-editor.org/rfc/rfc7643.html

https://www.rfc-editor.org/rfc/rfc7644.html

# Local files
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
