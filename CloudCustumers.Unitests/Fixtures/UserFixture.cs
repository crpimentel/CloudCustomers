using CloudCustomer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustumers.Unitests.Fixtures
{
    public static class UserFixture
    {
        public static List<User> GetTestUsers() => new() 
        {
            new User()
            {
                Name = "Cristian Pimentel",
                Email="CristianTabarePimentel@gmail.com",
                address = new Address()
                {
                    City = "Montevideo",
                    Street = "Av. 18 de Julio",
                    State = "Apto 101",
                    ZipCode = "11200"
                }
            },
            new User()
            {
                Name = "Cristal Pimentel",
                Email="CristalPimentel@gmail.com",
                address = new Address()
                {
                    City = "Montevideo2",
                    Street = "Av. 18 de Julio",
                    State = "Apto 101",
                    ZipCode = "11200"
                }
            },
            new User()
            {
                Name = "keily Pimentel",
                Email="KeilyPimentel@gmail.com",
                address = new Address()
                {
                    City = "Montevideo",
                    Street = "Av. 18 de Julio",
                    State = "Apto 101",
                    ZipCode = "11200"
                }
            },
            new User()
            {
                Name = "kenia Tineo",
                Email="keniaTineo@gmail.com",
                address = new Address()
                {
                    City = "Montevideo",
                    Street = "Av. 18 de Julio",
                    State = "Apto 101",
                    ZipCode = "11200"
                }
            }
        };
    }
}
