using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    public static class MathHelper
    {
        /// <summary>
        /// Chuyển String về Int
        /// </summary>
        public static int IntParseFast(string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char letter = value[i];
                result = 10 * result + (letter - 48);
            }

            return result;
        }

        /// <summary>
        /// Chuyển Char về Int
        /// </summary>
        public static int IntParseFast(char value)
        {
            int result = 0;
            result = 10 * result + (value - 48);
            return result;
        }

        /// <summary>
        /// Trả về giá trị random trong List
        /// </summary>
        public static T RandomValueInList<T>(List<T> lst)
        {
            if (lst.Count > 0) return lst[Random.Range(0, lst.Count)];
            return default;
        }

        /// <summary>
        /// Trộn mảng
        /// </summary>
        public static void Shuffle<T>(this List<T> idxs)
        {
            for (int i = 0; i < idxs.Count - 1; i++)
            {
                int random = Random.Range(i, idxs.Count);
                idxs.Swap(i, random);
            }
        }

        private static void Swap<T>(this List<T> idxs, int a, int b)
        {
            T temp = idxs[a];
            idxs[a] = idxs[b];
            idxs[b] = temp;
        }

        /// <summary>
        /// Int to Score
        /// </summary>
        public static string ScoreShow(ulong Score)
        {
            float Scor = Score;
            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;

            for (i = 0; i < ScoreNames.Length; i++)
                if (Scor < 900)
                    break;
                else Scor = Mathf.Floor(Scor / 100f) / 10f;

            if (Scor == Mathf.Floor(Scor))
                result = Scor.ToString() + ScoreNames[i];
            else result = Scor.ToString("F1") + ScoreNames[i];
            return result;
        }

        /// <summary>
        /// Int to Score
        /// </summary>
        public static string ScoreShow(long Score)
        {
            float Scor = Score;
            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;

            for (i = 0; i < ScoreNames.Length; i++)
                if (Scor < 900)
                    break;
                else Scor = Mathf.Floor(Scor / 100f) / 10f;

            if (Scor == Mathf.Floor(Scor))
                result = Scor.ToString() + ScoreNames[i];
            else result = Scor.ToString("F1") + ScoreNames[i];
            return result;
        }
    }
}
