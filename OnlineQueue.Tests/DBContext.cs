using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication2;

namespace OnlineQueue.Tests
{
    public class DBContext
    {
        public queueDBContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<queueDBContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new queueDBContext(options);

            context.RelaxRum.Add(new RelaxRum { Id = 1, Name = "Комната отдыха 1" });
            context.RelaxRum.Add(new RelaxRum { Id = 2, Name = "Комната отдыха 2" });

            context.Microwave.Add(new Microwave { Id = 1, Busy = false, RelaxRumId = 1, UserId = null });
            context.Microwave.Add(new Microwave { Id = 2, Busy = false, RelaxRumId = 1, UserId = null });
            context.Microwave.Add(new Microwave { Id = 3, Busy = false, RelaxRumId = 2, UserId = null });
            context.Microwave.Add(new Microwave { Id = 4, Busy = false, RelaxRumId = 2, UserId = null });

            context.User.Add(new User { Id = 1, Name = "Петрова Елена", Email = "Petrov4.Elen@yandex.ru", Password = "789" });

            context.SaveChanges();

            return context;
        }
    }
}
