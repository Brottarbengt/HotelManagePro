using HotelManagePro.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelManagePro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            App.Run();
        }
    }
}
