using System;

namespace V6ThuePost.ViettelV2Objects.Response
{
    public class LoginResponse
    {
        public string error { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public int iat { get; set; }
        public int type { get; set; }
        public Guid jti { get; set; }
    }
    //{
    //"access_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX25hbWUiOiIwMTAwMTA5MTA2LTcxNSIsInNjb3BlIjpbIm9wZW5pZCJdLCJleHAiOjE2MDA3NjgwNzAsInR5cGUiOjEsImlhdCI6MTYwMDc2Nzc3MCwiaW52b2ljZV9jbHVzdGVyIjoiY2x1c3RlcjEiLCJhdXRob3JpdGllcyI6WyJST0xFX1VTRVIiXSwianRpIjoiYzU2ZDBhYzQtOGY4Yi00OGQ1LThiYTUtZTExNmYxYjJlZTM5IiwiY2xpZW50X2lkIjoid2ViX2FwcCJ9.DWYMwltRLcXsXtL-kp8ojvHqgk6tXe_7XZjC2kWELrKIqMi4CdF1zWAES1cqjhbc-F-wDoIu4tOEtsA3QZ2Cv5PrTepwcAkiu5XrnjtQRxgFzQiCwcLOLSLSEuEZNprA4g0JQ33cnKU0TPt6lzqPOB5Ebu7jNgbCY87m6MmR8U-EWktkU-Qup6S47rTy8nDbNjtsVfPATAKR1NGfqsV4XG9zf1luqqbtq0g-k0VCjVo_DItozPyHT5FYPeiQpIMhKOk2gFRp8TX7jYZpaQzU7Y8YrUcY3cpIGpM061eyqo6Gtrdc8hdLITinbB3NKuwJl1QEAoAQa39E4ETL6l89dw",
    //"token_type": "bearer",
    //"refresh_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX25hbWUiOiIwMTAwMTA5MTA2LTcxNSIsInNjb3BlIjpbIm9wZW5pZCJdLCJhdGkiOiJjNTZkMGFjNC04ZjhiLTQ4ZDUtOGJhNS1lMTE2ZjFiMmVlMzkiLCJleHAiOjE2MDEzNzI1NzAsInR5cGUiOjEsImlhdCI6MTYwMDc2Nzc3MCwiaW52b2ljZV9jbHVzdGVyIjoiY2x1c3RlcjEiLCJhdXRob3JpdGllcyI6WyJST0xFX1VTRVIiXSwianRpIjoiMTUyZGEzZjYtNjM1OS00NWQ0LTg1MjItMTdlODM2ZDEzZWQyIiwiY2xpZW50X2lkIjoid2ViX2FwcCJ9.WtbH_tNSnKgHTnBJYdlfbUz-ztj626Urpu0ynar3qWVZOeN1N8nD9OJSOR-ZKsjWpiB7ZEyXxA7yDTKaZ_9ngkCdslooK0iXfuMji_CCzj3MB1FQo6Qwxczbp_MJTVC2HlbnlQ8Ws1STppl0033Hqlrzic6obfzC7cHp9aGkfEIrXc08eCQUXlmOOBoAjTl63ehDP-VqXZoe3hlrgJd2MYTZOdjitbCPttJVLi-R2HVNgHCWM5Z6tNPRjis-7apOLdswZUemjQrexl4nos6BIhMog6r7z2OvJZPEHwNzX66jeW3mIJOQ7XXQe2Qt6Zkiyw04KmtPXbWpStvSHCxcvA",
    //"expires_in": 298,
    //"scope": "openid",
    //"iat": 1600767770,
    //"invoice_cluster": "cluster1",
    //"type": 1,
    //"jti": "c56d0ac4-8f8b-48d5-8ba5-e116f1b2ee39"
    //}
}
