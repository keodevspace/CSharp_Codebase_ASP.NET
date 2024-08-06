// Program executa o banco de dados BankokeoDataContext

using Bankokeo.Data;
using Bankokeo.Models;
using Microsoft.EntityFrameworkCore;

namespace Bankokeo;

class Program
    {
    static void Main(string[] args)
        {
        using (var context = new BankokeoDataContext())
            {
            // CREATE
            var user1 = new User()
                {
                Name = "rocky balboa",
                Login = "rockylogin",
                Password = "roky123",
                Account = 1,
                Balance = "100"
                };

            var user2 = new User()
                {
                Name = "mickey manager",
                Login = "mickeylogin",
                Password = "mickey123",
                Account = 2,
                Balance = "100"
                };

            var user3 = new User()
                {
                Name = "adrian balboa",
                Login = "adrianlogin",
                Password = "adrian123",
                Account = 3,
                Balance = "100"
                };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.SaveChanges();
            Console.WriteLine("User criado e enviado para banco");

            // READ
            var users = context.Users.ToList();
            foreach (var user in users)
                {
                Console.WriteLine(user.Name);
                }
            Console.WriteLine($"Lista:{users}");

            // UPDATE
            var userToUpdate = context.Users.FirstOrDefault(x => x.Id == 1); // SELECT * FROM Users WHERE Id = 1
            if (userToUpdate != null)
                {
                userToUpdate.Name = "Rocky Balboa";
                context.Users.Update(userToUpdate);
                context.SaveChanges();
                Console.WriteLine("Update");
                }
            else
                {
                Console.WriteLine("User not found for update");
                }

            // DELETE
            var userToDelete = context.Users.FirstOrDefault(x => x.Id == 2); // SELECT * FROM Users WHERE Id = 2
            if (userToDelete != null)
                {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
                Console.WriteLine("Delete");
                }
            else
                {
                Console.WriteLine("User not found for delete");
                }

            // ToList() -> SELECT * FROM Users
            var userToList = context
                .Users
                .AsNoTracking() // Não rastrear
                .ToList();
            foreach (var user in userToList)
                {
                Console.WriteLine(user.Name);
                }
            Console.WriteLine($"Lista:{userToList}");
            }
        }
    }
