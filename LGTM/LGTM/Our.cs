using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LGTM
{
    public static class Our
    {
        public static HttpClient Client()
        {
            var client = new HttpClient();
            // De base url die voor alle calls hetzelfde is
            client.BaseAddress = new Uri("http://involved-htf-challenge.azurewebsites.net");
                 
            // De token die je gebruikt om je team te authenticeren, deze kan je via de swagger ophalen met je teamname + password
            var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMzgiLCJuYmYiOjE2Mzg0Mzc0ODMsImV4cCI6MTYzODUyMzg4MywiaWF0IjoxNjM4NDM3NDgzfQ.uOajE4IW8PVfkV3c5cr_tYQKYKSyyg1i34WC4a9OcNoXPf9kbfgBZfiL3k3Qssk1HrUjn9jJRnOpqQ-JcK3CKQ";
            // We stellen de token in zodat die wordt meegestuurd bij alle calls, anders krijgen we een 401 Unauthorized response op onze calls
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
        
    }
}