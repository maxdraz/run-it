using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunIt.Testing
{
    public class RandomNameGenerator : MonoBehaviour
    {
        public int nameLength = 3;
        [SerializeField] private string randomName;
        private char[] vowels;
        private void OnEnable()
        {
            var vowels = new char[]{'a', 'e', 'i', 'o', 'u'};
            var consonants = new char[]{'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l','m', 'n', 'p', 'q', 'r', 's', 't',
                'v', 'w', 'x', 'y', 'z'};

            string n = null;
            for (int i = 0; i < nameLength; i++)
            {
                char l;
                int ran;
                if (i == 0)
                {
                    ran = Random.Range(0, consonants.Length - 1);
                    l = consonants[ran];
                    n += l;
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

                n += l;
            }


            var nmmm = Utilities.Utilities.RandomName(3);
            print(nmmm);

        }
        public string RandomName(int length)
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