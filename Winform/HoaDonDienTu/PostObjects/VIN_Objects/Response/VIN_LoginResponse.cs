using System;

namespace V6ThuePost.VIN_Objects.Response
{
    public class VIN_LoginResponse
    {
        public VIN_LoginResult result { get; set; }
    }

    public class VIN_LoginResult
    {
//        {
//- access_token: Token 
//- encrypted_access_token: Mã client ID 
//- expire_in_seconds: Thời hạn sử dụng của token, ms
//- token_type: bear
//}
        public string error { get; set; }
        public string access_token { get; set; }
        public string encrypted_access_token { get; set; }
        public int expire_in_seconds { get; set; }
        public string token_type { get; set; }

        public string refresh_token { get; set; }
        public string scope { get; set; }
        public int iat { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public string path { get; set; }
        public string message { get; set; }
    }
}
