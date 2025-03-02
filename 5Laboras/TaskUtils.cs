using System.Collections.Generic;
using System.Linq;

namespace _5Laboras
{
    /// <summary>
    /// Class for calculations
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Gets all the prenumerators 
        /// and returns the list of them
        /// </summary>
        /// <param name="conteiners"></param>
        /// <returns></returns>
        public static List<Prenumerator> 
            AllPrenum(List<Storage> conteiners)
        {
            var allprenum = conteiners
                .SelectMany(container => container.GetPrenumerators());

            return allprenum.ToList();
        }

        /// <summary>
        /// Gets the list of prenumerators which has been selected
        /// </summary>
        /// <param name="conteiners"></param>
        /// <param name="month"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public static List<Prenumerator> 
            SelectedPrenumerators(List<Storage> conteiners, 
            int month, int startYear, int endYear)
        {
            var selectedPrenumerators = conteiners
                .Where(c => c.Year >= startYear 
                && c.Year <= endYear)
                .SelectMany(conteiner => 
                conteiner.GetPrenumerators())
                .Where(prenumerator => month >= prenumerator.Start 
                && month <= prenumerator.Duration 
                + prenumerator.Start - 1)
                .OrderBy(pren => pren.Address)
                .ThenBy(pren => pren.Surname);

            return selectedPrenumerators.ToList();
        }

        /// <summary>
        /// Adds product count to the each product
        /// </summary>
        /// <param name="prenumerators"></param>
        /// <param name="products"></param>
        public static void ProductCount(List<Prenumerator> prenumerators, 
            List<Product> products)
        {
            prenumerators.ForEach(prenumerator =>
            {
                products.Where(product => product.Code == prenumerator.Code)
                        .ToList()
                        .ForEach(product => 
                        product.AddCount(prenumerator.Count));
            });
        }
    }
}