using System.Collections.Generic;

namespace _5Laboras
{
    /// <summary>
    /// Storage class
    /// </summary>
    public class Storage
    {
        private List<Prenumerator> prenumerators;
        public int Year { get; }

        public Storage(int year)
        {
            Year = year;
            prenumerators = new List<Prenumerator>();
        }

        /// <summary>
        /// Adds prenumerator to the list
        /// </summary>
        /// <param name="prenumerator"></param>
        public void AddPrenum(Prenumerator prenumerator)
        {
            prenumerators.Add(prenumerator);
        }

        /// <summary>
        /// Returns prenumeretors list
        /// </summary>
        /// <returns></returns>
        public List<Prenumerator> GetPrenumerators()
        {
            return prenumerators;
        }
    }
}