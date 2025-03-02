using System;

namespace _5Laboras
{
    /// <summary>
    /// Constructor class
    /// </summary>
    public class Prenumerator
    {
        public string Surname { get; private set; }
        public string Address { get; private set; }
        public int Start { get; set; }
        public int Duration { get; private set; }
        public string Code { get; private set; }
        public int Count { get; private set; }

        public Prenumerator(string line)
        {
            SetData(line);
        }

        /// <summary>
        /// Sets the given data
        /// </summary>
        /// <param name="line"></param>
        private void SetData(string line)
        {
            string[] values = line.Split(',');
            Surname = values[0];
            Address = values[1];
            Start = int.Parse(values[2]);
            Duration = int.Parse(values[3]);
            Code = values[4];
            Count = int.Parse(values[5]);
        }

        /// <summary>
        /// Overriding ToString for printing
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format($"| {Surname,-15} | " +
                $"{Address,-15} | {Start,7} | " +
                $"{Duration,6} | {Code,-8} | {Count,7} |");
        }
    }
}