using System;
using Microsoft.AspNetCore.Http;

namespace Datingapp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            response.Headers.Add("Acess-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            age = (dateOfBirth.AddYears(age) > DateTime.Today) ? (age--) : age;
            return age;
        }
    }
}