using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTOSQLite
{
    public class ServerCredentialsDTOS
    {
        [JsonPropertyName("serverCredentialsId")]
        public Guid ServerCredentialsId { get; set; } = new Guid();
        [JsonPropertyName("userName")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public ServerCredentialsDTOS()
        {

        }

        //public ServerCredentialsDTOS(ServerCredentialsS cred)
        //{
        //    ServerCredentialsId = cred.ServerCredentialsId;
        //    Username = cred.Username;
        //    Password = cred.Password;
        //}

        //public ServerCredentialsS FromDTO()
        //{
        //    return new ServerCredentialsS()
        //    {
        //        ServerCredentialsId = ServerCredentialsId,
        //        Username = Username,
        //        Password = Password
        //    };
        //}
    }
}
