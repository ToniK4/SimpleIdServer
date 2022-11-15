using Newtonsoft.Json;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    public class GetResponse
    {
        //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Meta
        {
            public string ResourceType { get; set; }
            public DateTime Created { get; set; }
            public DateTime LastModified { get; set; }
            public int Version { get; set; }
            public string Location { get; set; }
        }

        public class Name
        {
            public string FamilyName { get; set; }
            public string GivenName { get; set; }
        }

        public class Resource
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public Name Name { get; set; }
            public List<object> X509Certificates { get; set; }
            public List<object> Photos { get; set; }
            public List<object> Entitlements { get; set; }
            public List<object> PhoneNumbers { get; set; }
            public List<object> Groups { get; set; }
            public List<object> Emails { get; set; }
            public List<object> Roles { get; set; }
            public List<object> Addresses { get; set; }
            public List<object> Ims { get; set; }
            public Meta Meta { get; set; }
            public string ExternalId { get; set; }
            public List<string> Schemas { get; set; }
        }

        public class Root
        {
            public List<string> Schemas { get; set; }
            public int TotalResults { get; set; }
            public int ItemsPerPage { get; set; }
            public int StartIndex { get; set; }
            public List<Resource> Resources { get; set; }
        }


    }
}
