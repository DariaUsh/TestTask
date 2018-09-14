using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class Queue
    {
        public queueDBContext db { get; set; }
        public int numberUser { get; set; }
        public int numberMicrowave { get; set; }
        public string relaxRum { get; set; }
        public int relaxRumId { get; set; }
        private string culture { get; set; } = CultureInfo.CurrentCulture.Name;

        public Queue(queueDBContext db)
        {
            this.db = db;
        }

        public List<string> GetStatusMicrowave()
        {
            var statusMicrowave = new List<string>();            
            var microwaves = db.Microwave.Include(c => c.User).ToList();
            var status = "";

            foreach (Microwave item in microwaves)
            {
                if (item.Busy)
                {
                    if (item.User == null)
                    {                       
                        statusMicrowave.Add(culture == "ru" ? status = "Свободна" : status = "Free");
                        item.Busy = false;
                        db.SaveChanges();
                    }
                    else
                    {
                        statusMicrowave.Add(item.User.Name);
                    }
                }
                else
                {
                    statusMicrowave.Add(culture == "ru" ? status = "Свободна" : status = "Free");
                }
            }
            return statusMicrowave;         
        }

        public bool IsInQueue(string userEmail)
        {
            var userFind = db.User.FirstOrDefault(u => u.Email == userEmail);
            if (userFind != null)
            {
                var user = db.Line.FirstOrDefault(q => q.UserId == userFind.Id);
                numberUser = user != null? user.Number : 0;
                return true;
            }
            return false;          
        }

        public bool AddInQueue(string userEmail)
        {
            var userFind = db.User.FirstOrDefault(u => u.Email == userEmail);
            if (userFind != null)
            {
                var isInQueue = IsInQueue(userEmail);
                var notBusy = db.Microwave.Include(r => r.RelaxRum).Where(m => m.Busy == false).ToList();
                if (notBusy.Count > 0)
                {
                    notBusy[0].Busy = true;
                    notBusy[0].UserId = userFind.Id;
                    db.SaveChanges();
                    numberUser = 0;
                    numberMicrowave = notBusy[0].Id;
                    relaxRumId = notBusy[0].RelaxRum.Id;
                    if (isInQueue)
                    {
                        RemoveInQueue(userEmail);
                    }
                    return false;
                }
                else
                {
                    if (isInQueue)
                    {
                        return true;
                    }
                    int number = db.Line.ToList().Count;
                    var Line = new Line()
                    {
                        Number = ++number,
                        UserId = userFind.Id
                    };
                    db.Line.Add(Line);
                    db.SaveChanges();
                    numberUser = number++;
                    return true;
                }
            }
             return false;          
        }

        public bool Complete(string userEmail)
        {
            var userFind = db.User.FirstOrDefault(u => u.Email == userEmail);
            if(userFind != null)
            {
                var microwave = db.Microwave.FirstOrDefault(m => m.UserId == userFind.Id);
                if (microwave != null)
                {
                    microwave.Busy = false;
                    microwave.UserId = null;

                    db.SaveChanges();
                    return true;
                }
            }
            return false;            
        }

        public bool RemoveInQueue(string userEmail)
        {
            var userFind = db.User.FirstOrDefault(u => u.Email == userEmail);
            if (userFind != null)
            {
                var LineList = db.Line.ToList();
                var userInLine = LineList.FirstOrDefault(u => u.UserId == userFind.Id);
                if (userInLine != null)
                {
                    db.Line.Remove(userInLine);
                    for (var i = userInLine.Number; i < LineList.Count; i++)
                    {
                        LineList[i].Number -= 1;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool IsUseMicrowave(string userEmail)
        {
            var userFind = db.User.FirstOrDefault(u => u.Email == userEmail);
            if(userFind != null)
            {
                var microwave = db.Microwave.FirstOrDefault(m => m.UserId == userFind.Id);
                if (microwave != null)
                {
                    numberMicrowave = microwave.Id;
                    return true;
                }
            }
            return false;
        }

    }
}
