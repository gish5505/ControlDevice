using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDevice.Calculations
{
    public class FixedSizeQueue<T>
    {
        private Queue<T> _fixedSizeQueue;
        private Queue<DateTime> _dateTimeQueue;

        public FixedSizeQueue(int limit)
        {
            Limit = limit;
            _fixedSizeQueue = new Queue<T>(limit);
            _dateTimeQueue = new Queue<DateTime>(limit);

        }

        public int Limit { get; private set; }

        public Queue<T> Queue { get { return _fixedSizeQueue; } }

        public void Enqueue(T obj)
        {
            if (_fixedSizeQueue.Count < Limit)
            {
                _fixedSizeQueue.Enqueue(obj);
                _dateTimeQueue.Enqueue(DateTime.Now);
            }
            else
            {
                _fixedSizeQueue.Dequeue();
                _dateTimeQueue.Dequeue();
            }
        }

    }

    public class DoubleFixedSizeQueue : FixedSizeQueue<double>
    {
        public DoubleFixedSizeQueue(int limit)
            : base(limit)
        {
        }

    }

    public class DateTimeFixedSizeQueue : FixedSizeQueue<DateTime>
    {
        public DateTimeFixedSizeQueue(int limit)
            : base(limit)
        {

        }

    }
}
