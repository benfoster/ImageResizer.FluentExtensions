
namespace ImageResizer.FluentExtensions
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Checks whether the specified <paramref name="number"/> is a valid percentage
        /// </summary>
        public static bool IsValidPercentage(this int number)
        {
            return (number >= 0 && number <= 100);
        }

        /// <summary>
        /// Checks whether a number is between or equal to <paramref name="lowerRange"/> and <paramref name="upperRange"/>
        /// </summary>
        public static bool IsBetween(this int number, int lowerRange, int upperRange)
        {
            return ((double)number).IsBetween(lowerRange, upperRange);
        }

        /// <summary>
        /// Checks whether a number is between or equal to <paramref name="lowerRange"/> and <paramref name="upperRange"/>
        /// </summary>
        public static bool IsBetween(this double number, double lowerRange, double upperRange)
        {           
            return (number >= lowerRange && number <= upperRange);
        }
    }
}
