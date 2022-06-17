using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercice5
{
    public class BinaryS
    {
        public static int BinarySearch(float[] tableau, float valeur)
        {
            // On trie au cas où
            Array.Sort(tableau);

            int min = 0;
            int max = tableau.Length - 1 ;

            while (min != max)
            {

                int milieu = (min + max) / 2;

                if (tableau[milieu] == valeur)
                {
                    (min, max) = (milieu, milieu);
                }
                else if (tableau[milieu] < valeur)
                {
                    min = milieu;
                }
                else
                {
                    max = milieu;
                }
            }
            return min;
        }
    }


    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        static public bool IsSorted<T>(IEnumerable<T> ts)
        {
            return ts
                .Zip(ts.Skip(1), (a, b) => new { a, b })
                .All(x => ((IComparable<T>)(x.a)).CompareTo(x.b) <= 0);
        }

        /// <summary>
        /// Résout a*x+b=0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>un float si on a une solution, null s'il n'y a pas de solution, NaN s'il y a une infinité de solutions</returns>
        static public float? EquationPremierDegre(float a, float b)
        {
            if (a == 0 && b != 0) return null;
            if (a == 0 && b == 0) return float.NaN;

            return -b / a;
        }

        class ConstantPolynomialException : Exception { }

        static public float EquationPremierDegreThrows(float a, float b)
        {
            if (a == 0) throw new ConstantPolynomialException();

            return -b / a;
        }

        static public float[] EquationSecondDegre(float a, float b, float c)
        {
            float delta = b * b - 4 * a * c;

            if(a == 0)
            {
                float? v = EquationPremierDegre(b, c);

                if(! v.HasValue)
                {
                    return new float[] { };
                }
                else if (float.IsNaN(v.Value))
                {
                    return null;//throw new ConstantPolynomialException();
                }
                else
                {
                    return new float[] { v.Value };
                }
                /*
                return new float[] { EquationPremierDegreThrows(b, c) };
                */
            }

            if (delta < 0)
            {
                return new float[0];
            }
            else if(delta == 0)
            {
                return new float[] { -b/(2*a) };
            }
            else
            {
                return new float[]
                {
                    (float)((-b + Math.Sqrt(delta))/(2*a)),
                    (float)((-b - Math.Sqrt(delta))/(2*a))
                };
            }
        }

        private static (int, int) BinarySearchImpl(float[] tableau, float valeur, int min, int max)
        {
            int milieu = (min + max) / 2;

            if (tableau[milieu] == valeur)
            {
                return (milieu, milieu);
            }
            else if(tableau[milieu] < valeur)
            {
                return (milieu, max);
            } else
            {
                return (min, milieu);
            }
        }

        public static int BinarySearch(float[] tableau, float valeur)
        {
            // On trie au cas où
            Array.Sort(tableau);

            (int, int) bornes = (0, tableau.Length-1);

            while(bornes.Item1 != bornes.Item2)
            {
                bornes = BinarySearchImpl(tableau, valeur, bornes.Item1, bornes.Item2);
            }
            return bornes.Item1;
        }

        static void Main(string[] args)
        {
            float? v = EquationPremierDegre(0, 1);
            if (v.HasValue)
            {
                Console.WriteLine(v);
            }

        }

    }
}
