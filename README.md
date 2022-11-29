# SimpleIdServer links

SimpleIdServer
https://github.com/simpleidserver/SimpleIdServer

CaseManagement - BPMN and HumanTask engine
https://github.com/simpleidserver/CaseManagement

# OAuth rfc
https://www.rfc-editor.org/rfc/rfc6749

https://www.rfc-editor.org/rfc/rfc6750

# Scim rfc's
https://www.rfc-editor.org/rfc/rfc7642.html

https://www.rfc-editor.org/rfc/rfc7643.html

https://www.rfc-editor.org/rfc/rfc7644.html

# MassTransit
The MassTransit migration to dotnet 6 requires some code changes to work.
Action of type IServiceCollectionBusConfigurator needs to be replaced with Action of type IBusRegistrationConfigurator in the ServiceCollectionExtensions class.

# Running the project and Tests
Running both the IdentityProvider and Scim project should allow you to run all of the tests.

# Startup problems
If there are SQL query problems when starting either the SCIM or Identity Provider project, remaking the database usually helps.
