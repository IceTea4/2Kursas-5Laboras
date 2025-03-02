using System;

namespace _5Laboras
{
    /// <summary>
    /// Constructor class
    /// </summary>
    public class Product
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Count { get; private set; }

        public Product(string line)
        {
            SetData(line);
            Count = 0;
        }

        /// <summary>
        /// Sets the given data
        /// </summary>
        /// <param name="line"></param>
        private void SetData(string line)
        {
            string[] values = line.Split(',');
            Code = values[0];
            Name = values[1];
            Price = decimal.Parse(values[2].Replace('.', ','));
        }

        /// <summary>
        /// Adds the product count
        /// </summary>
        /// <param name="count"></param>
        public void AddCount(int count)
        {
            Count += count;
        }

        /// <summary>
        /// Overriding ToString for printing
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format($"| {Code,-8} | {Name,-15} " +
                $"| {Price,6} | {Count,6} |");
        }
    }
}