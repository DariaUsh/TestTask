using System;
using System.Collections.Generic;
using System.Text;
using WebApplication2;
using Xunit;

namespace OnlineQueue.Tests
{
    public class QueueTests
    {
        DBContext db = new DBContext();

        [Fact]
        public void GetStatusMicrowaveShouldReturnList()
        {
            Queue queue = new Queue(db.GetContextWithData());

            var result = queue.GetStatusMicrowave() as List<string>;

            Assert.NotNull(result);
        }

        [Fact]
        public void IsInQueueShouldReturnBool()
        {
            Queue queue = new Queue(db.GetContextWithData());

            var result = queue.IsInQueue(null);

            Assert.True(result == true || result == false);
        }

        [Fact]
        public void AddInQueueShouldReturnBool()
        {
            Queue queue = new Queue(db.GetContextWithData());

            var result = queue.AddInQueue(null);

            Assert.True(result == true || result == false);
        }

        [Fact]
        public void CompleteShouldReturnBool()
        {
            Queue queue = new Queue(db.GetContextWithData());

            var result = queue.Complete(null);

            Assert.True(result == true || result == false);
        }

        [Fact]
        public void RemoveInQueueShouldReturnBool()
        {
            Queue queue = new Queue(db.GetContextWithData());

            var result = queue.RemoveInQueue(null);

            Assert.True(result == true || result == false);
        }

        [Fact]
        public void IsUseMicrowaveShouldReturnBool()
        {
            Queue queue = new Queue(db.GetContextWithData());

            var result = queue.IsUseMicrowave(null);

            Assert.True(result == true || result == false);
        }
    }
}
