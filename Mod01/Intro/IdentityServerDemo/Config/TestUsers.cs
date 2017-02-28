using System.Collections.Generic;
using IdentityServer4.Test;

namespace IdentityServerDemo.Config
{
    public class TestUsers
    {
        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "123",
                Username = "bob",
                Password = "bob",
                //Claims = new List<Claim>
                //{
                //    new Claim(Constants.Resources.Financial, "true")
                //}
            },

            new TestUser
            {
                SubjectId = "321",
                Username = "alice",
                Password = "alice",
            }
        };
    }
}