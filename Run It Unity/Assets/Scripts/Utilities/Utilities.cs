using System;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using Random = UnityEngine.Random;
using UnityEngine;
using Unity;

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
            string name = null;
            var vowels = new char[]{'a', 'e', 'i', 'o', 'u'};
            var consonants = new char[]{'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l','m', 'n', 'p', 'q', 'r', 's', 't',
                'v', 'w', 'x', 'y', 'z'};

            for (int i = 0; i < length; i++)
            {
                char l;
                int ran;
                if (i == 0)
                {
                    ran = Random.Range(0, consonants.Length - 1);
                    l = consonants[ran];
                    name += l;
                    continue;
                }

                if (i % 2 == 1)
                {
                    ran = Random.Range(0, vowels.Length - 1);
                    l = vowels[ran];
                }
                else
                {
                    ran = Random.Range(0, consonants.Length - 1);
                    l = consonants[ran];
                }

                name += l;
            }

            return name;
        }
    }
}