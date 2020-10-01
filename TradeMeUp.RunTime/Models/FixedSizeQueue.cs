using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Models
{
	public class FixedSizeQueue<T>
	{
		private Queue<T> queue;
		
		public int Size { get; private set; }
		public int Count => queue.Count;

		public FixedSizeQueue(int size)
		{
			queue = new Queue<T>();
			Size = size;
		}

		public void Enqueue(T item)
		{
			queue.Enqueue(item);

			if (queue.Count > Size)
			{
				queue.Dequeue();
			}
		}

		public T First() => queue.First();
		public T Last() => queue.Last();
		public List<T> ToList() => queue.ToList();
		public T[] ToArray() => queue.ToArray();
		public IEnumerator<T> GetEnumerator() => queue.GetEnumerator();
	}
}
