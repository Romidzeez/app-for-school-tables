using ConsoleApp1;
using Microsoft.EntityFrameworkCore;

using (TrainingContext db = new TrainingContext())
{
    User Test = new User { Age = 121, LastName = "Tes1t", FirstName = "T1EST", };
    db.Users.Add(Test);
    //db.SaveChanges();


    // получаем объекты из бд и выводим на консоль
    try
    {
        db.SaveChanges();
        Console.WriteLine("User saved successfully.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
    var users = db.Users.ToList();
    Console.WriteLine("Список объектов:");
    foreach (User i in users)
    {
        Console.WriteLine($"{i.Id}.{i.Name} - {i.Age}");
    }
}