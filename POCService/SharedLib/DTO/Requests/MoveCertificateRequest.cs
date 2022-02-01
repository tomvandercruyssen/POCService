using SharedLib.Enums;
using System.Text.Json.Serialization;

namespace SharedLib.DTO.Requests
{
    public class MoveCertificateRequest
    {
        [JsonPropertyName("file")]
        public string File { get; set; }
        [JsonPropertyName("target")]
        public CertificateFolders Target { get; set; }
    }
}
