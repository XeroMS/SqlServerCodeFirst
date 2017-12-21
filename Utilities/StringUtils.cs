using System;

namespace Utilities
{
    public static class StringUtils
    {

        #region Extension Methods

        /// <summary>
        /// Checks whether the string is Null Or Empty
        /// </summary>
        /// <param name="theInput"></param>
        /// <returns></returns>
        public static bool IsNullEmpty(this string theInput)
        {
            return string.IsNullOrEmpty(theInput);
        }

        /// <summary>
        /// Converts the string to Int32
        /// </summary>
        /// <param name="theInput"></param>
        /// <returns></returns>
        public static int ToInt32(this string theInput)
        {
            return !string.IsNullOrEmpty(theInput) ? Convert.ToInt32(theInput) : 0;
        }

        #endregion

    }
}
