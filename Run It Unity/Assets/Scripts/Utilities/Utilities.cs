using System;
using System.Collections.Generic;

namespace RunIt.Utilities
{
    public static class Utilities
    {
        public static object GetObjectFromList<T>(List<T> list, Type type)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetType() == type)
                {
                    return list[i];
                }
            }
            return null;
        }

        public static string RandomName(int length)
        {
            var name = new char[length];
            var vowels = new char['a', 'e', 'i', 'o', 'u'];
            var consonants = new char['b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't',
                'v', 'w', 'x', 'y', 'z'];

            for (int i = 0; i < name.Length; i++)
            {
                
            }

            return "";
        }
    }
}