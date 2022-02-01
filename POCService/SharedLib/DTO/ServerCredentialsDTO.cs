using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTO
{
    public class ServerCredentialsDTO
    {
        [JsonPropertyName("serverCredentialsId")]
        public string ServerCredentialsId { get; set; }// = new Guid();
        [JsonPropertyName("userName")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public ServerCredentialsDTO()
        {

        }

        public ServerCredentialsDTO(ServerCredentials cred)
        {
            ServerCredentialsId = cred.ServerCredentialsId;
            Username = cred.Username;
            Password = cred.Password;
        }

        public ServerCredentials FromDTO()
        {
            return new ServerCredentials()
            {
                ServerCredentialsId = ServerCredentialsId,
                Username = Username,
                Password = Password
            };
        }
    }
}
