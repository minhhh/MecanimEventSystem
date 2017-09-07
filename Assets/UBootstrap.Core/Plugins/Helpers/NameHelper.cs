using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace UBootstrap
{
    static public partial class NameHelper
    {
        public static string CamelCase2Underscore (this string str)
        {
            string result = Regex.Replace (str, "(?<=.)([A-Z])", "_$0", RegexOptions.Singleline);
            return result.ToLower ();
        }

        public static string UnderscoreToCamel (this string str)
        {
            string result = "";
            str.Split ('_').ToList ().ForEach (substr =>
                {
                    result += substr.First ().ToString ().ToUpper () + substr.Substring (1);
                });
            return result.First ().ToString ().ToLower () + result.Substring (1);
        }
    }

}
