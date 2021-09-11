using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace LeetCodeSolutions
{
    class Program
    {
        public static int LengthOfLastWord(string s)
        {
            string removeWhiteSpace = Regex.Replace(s.Trim(' '), @"\s+", ",");
            string[] splitS = removeWhiteSpace.Split(',');

            if (splitS.Length < 0)
                return 0;

            return splitS[splitS.Length - 1].Length;
        }

        public static int RomanToInt(string s)
        {
            Dictionary<char, int> RomanMap = new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };

            int number = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i + 1 < s.Length && RomanMap[s[i]] < RomanMap[s[i + 1]])
                {
                    number -= RomanMap[s[i]];
                }
                else
                {
                    number += RomanMap[s[i]];
                }
            }
            return number;
        }

        public static int[] PlusOne(int[] digits)
        {
            if (digits[digits.Length - 1] == 9)
            {
                int cursor = digits.Length - 1;

                while (cursor > -1 && digits[cursor] == 9)
                {
                    digits.SetValue(0, cursor);
                    cursor--;
                }
                if (cursor == -1)
                {
                    Array.Resize(ref digits, digits.Length + 1);
                    digits.SetValue(1, cursor + 1);
                }
                else
                {
                    digits.SetValue(digits[cursor] + 1, cursor);
                }                
            }
            else
            {
                var result = digits[digits.Length - 1] + 1;
                digits.SetValue(result, digits.Length - 1);
            }

            return digits;
        }

        public static string AddBinary(string a, string b)
        {
            if (a == null || b == null) 
                return "";

            int first = a.Length - 1;
            int second = b.Length - 1;
            StringBuilder sb = new StringBuilder();
            int carry = 0;
            while (first >= 0 || second >= 0)
            {
                int sum = carry;
                if (first >= 0)
                {
                    sum += a[first] - '0';
                    first--;
                }
                if (second >= 0)
                {
                    sum += b[second] - '0';
                    second--;
                }
                carry = sum >> 1;
                sum = sum & 1;
                sb.Append(sum == 0 ? '0' : '1');
            }
            if (carry > 0)
                sb.Append('1');

            char[] chars = sb.ToString().ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        public static string LongestCommonPrefix(string[] strs)
        {
            int size = strs.Length - 1;
            string result="";

            if (strs.Length == 1)
            {
                return strs[0];
            }
            else
            {
                Array.Sort(strs);
                int tam = strs[0].Length;                

                for (int i = 0; i < tam; i++)
                {
                    // If the characters match, append the character to the result.
                    if (strs[0][i] == strs[size][i])
                    {
                        result += strs[0][i];
                    }
                    // Else stop the comparison.
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public static int HammingWeight(uint n)
        {
            string number = Convert.ToString(n,2);
            int countOnes = 0;
            int lastPosition = number.Length - 1;
            while (lastPosition >= 0)
            {
                if (number[lastPosition].Equals('1'))
                    countOnes++;

                lastPosition--;
            }

            return countOnes;
        }

        public static uint ReverseBits(uint n)
        {
            uint y = 0;
            for (int i = 0; i < 32; ++i)
            {
                y <<= 1;
                y |= (n & 1);
                n >>= 1;
            }

            return y;
        }

        public static List<string> FindRepeatedDnaSequences(string s)
        {
            var res = new HashSet<string>();
            const int Length = 10;
            var hashtable = new Dictionary<char, int> { { 'A', 1 }, { 'C', 2 }, { 'G', 3 }, { 'T', 4 } };
            var hashtableFor10LengthString = new Dictionary<long, int>();

            for (var i = 0; i < s.Length - 9; i++)
            {
                long rollinghash = 0;
                for (var j = 0; j < Length; j++)
                {
                    // get rolling hash, base 10
                    rollinghash = hashtable[s[i + j]] + rollinghash * 10;
                }

                if (hashtableFor10LengthString.ContainsKey(rollinghash))
                {
                    res.Add(s.Substring(i, 10));
                }
                else
                {
                    hashtableFor10LengthString.Add(rollinghash, i);
                }
            }

            return res.ToList();
        }

        public static int FindMaximumXOR(int[] nums)
        {
            int maxx = 0, mask = 0;

            HashSet<int> se = new HashSet<int>();

            for (int i = 30; i >= 0; i--)
            {
                mask |= (1 << i);

                for (int j = 0; j < nums.Length; ++j)
                {
                    se.Add(nums[j] & mask);
                }

                int newMaxx = maxx | (1 << i);

                foreach (int prefix in se)
                {
                    if (se.Contains(newMaxx ^ prefix))
                    {
                        maxx = newMaxx;
                        break;
                    }
                }
                se.Clear();
            }
            return maxx;
        }

        public static int Divide(int dividend, int divisor)
        {
            if (dividend == int.MinValue && divisor == -1)
            {
                return int.MaxValue;
            }
            bool minus = ((dividend > 0) ^ (divisor > 0));
            long num = Math.Abs(dividend);
            long div = Math.Abs(divisor);
            int ans = 0;
            while (num >= div)
            {
                long temp = div;
                long mul = 1;
                while (num >= (temp << 1))
                {
                    mul <<= 1;
                    temp <<= 1;
                }
                ans += Convert.ToInt32(mul);
                num -= temp;
            }
            return minus ? -ans : ans;
        }

        public static string ShortestCompletingWord(string licensePlate, string[] words)
        {
            string res = "";
            licensePlate = System.Text.RegularExpressions.Regex.Replace(licensePlate, @"\d", "");
            licensePlate = licensePlate.ToLower();
            licensePlate = licensePlate.Replace(" ", "");
            var dic = new Dictionary<char, int>();
            List<string> reslist = new List<string>();
            foreach (char letter in licensePlate.ToCharArray())
            {
                if (dic.ContainsKey(letter))
                {
                    dic[letter]++;
                }
                else
                {
                    dic.Add(letter,1);
                }
            }

            foreach(string wordToProcess in words)
            {
                bool canForm = true;
                var d = new Dictionary<char, int>();
                foreach (var chr in wordToProcess.ToCharArray())
                {
                    if (d.ContainsKey(chr))
                    {
                        d[chr]++;
                    }
                    else
                    {
                        d.Add(chr, 1);
                    }
                }
                foreach (var item in dic)
                {
                    if (!d.ContainsKey(item.Key))
                    {
                        //Does not meet the conditions
                        canForm = false;
                        break;
                    }
                    if (d[item.Key] >= item.Value)
                    {
                        canForm = true;
                    }
                    else
                    {
                        //Does not meet the conditions
                        canForm = false;
                        break;
                    }
                }
                if (canForm)
                {
                    reslist.Add(wordToProcess);
                }
            }
            if (reslist.Count == 0)
                return "";

            res = reslist[0];
            for (int i = 1; i < reslist.Count; i++)
            {
                if (reslist[i].Length < res.Length)
                    res = reslist[i];
            }
            
            return res;
        }

        public static string ReverseWords(string s)
        {
            s = s.TrimStart().TrimEnd();
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", ",");
            string[] words = s.Split(',');
            string result = "";

            int lastPost = words.Length - 1;
            while(lastPost >= 0)
            {
                result += words[lastPost] + " ";
                lastPost--;
            }

            return result.TrimEnd();
        }

        public static string FrequencySort(string s)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            foreach(char character in s)
            {
                if (frequency.ContainsKey(character))
                {
                    frequency[character]++;                    
                }
                else
                {
                    frequency.Add(character, 1);
                }
            }

            var sortedMap = frequency.OrderByDescending(v => v.Value);

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<char, int> pair in sortedMap)
            {
                int n = pair.Value;
                while (n-- > 0)
                    sb.Append(pair.Key);
            }

            return sb.ToString();
        }

        public static int[] FrequencySortArray(int[] nums)
        {
            Dictionary<int, int> frequency = new Dictionary<int, int>();

            foreach(int element in nums)
            {
                if (frequency.ContainsKey(element))
                {
                    frequency[element]++;
                }
                else
                {
                    frequency.Add(element, 1);
                }
            }

            var sortedDictionary = frequency.OrderBy(val => val.Value).ThenByDescending(v=> v.Key);

            List<int> result = new List<int>();

            foreach(KeyValuePair<int,int> pair in sortedDictionary)
            {
                for(int i = 0; i < pair.Value; i++)
                {
                    result.Add(pair.Key);
                }                
            }

            return result.ToArray();
        }

        public static int FirstUniqChar(string s)
        {
            Dictionary<char, int> characterFrecuency = new Dictionary<char, int>();

            foreach (char character in s)
            {
                if (characterFrecuency.ContainsKey(character))
                {
                    characterFrecuency[character]++;
                }
                else
                {
                    characterFrecuency.Add(character, 1);
                }
            }

            var getUniqueCharacter = characterFrecuency.Where(value => value.Value == 1).FirstOrDefault();
            
            if(getUniqueCharacter.Value != 1)
            {
                return -1;
            }

            return s.IndexOf(getUniqueCharacter.Key);
        }

        public static int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> topKFrequency = new Dictionary<int, int>();

            foreach(int element in nums)
            {
                if (topKFrequency.ContainsKey(element))
                {
                    topKFrequency[element]++;
                }
                else
                {
                    topKFrequency.Add(element, 1);
                }
            }

            var getElementsInFrecuency = topKFrequency.OrderByDescending(v => v.Value).Take(k);

            List<int> result = new List<int>();

            foreach(KeyValuePair<int,int> pair in getElementsInFrecuency)
            {
                result.Add(pair.Key);
            }

            return result.ToArray();
        }

        public static IList<string> TopKFrequent2(string[] words, int k)
        {
            List<string> result = new List<string>();
            Dictionary<string, int> frequencyWord = new Dictionary<string, int>();

            foreach (string element in words)
            {
                if (frequencyWord.ContainsKey(element))
                {
                    frequencyWord[element]++;
                }
                else
                {
                    frequencyWord.Add(element, 1);
                }
            }

            var sortFrequencyWords = frequencyWord.OrderByDescending(val => val.Value).Take(k);

            foreach(KeyValuePair<string,int> pair in sortFrequencyWords)
            {
                result.Add(pair.Key);
            }           

            return result;
        }

        public static int NumberOfSubstrings(string s)
        {
            int numberOfSubsequences = 0;
            string subString = "";
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 0; j <= s.Length - i; j++)
                {
                    subString = s.Substring(j, i);
                    if(subString.Contains('a') && subString.Contains('b') && subString.Contains('c'))
                    {
                        numberOfSubsequences++;
                    }
                }
            }

            return numberOfSubsequences;
        }

        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            int n = nums.Length;
            if (n <= 1) return false;
            if (t < 0) return false;

            // take care when t == 0
            long width = (long)t + 1;

            long GetBucketId(long val, long width)
            {
                return val < 0 ? (val + 1l) / width - 1l : val / width;
            }

            Dictionary<long, long> map = new Dictionary<long, long>();
            for (int i = 0; i < n; i++)
            {
                long bucketId = GetBucketId((long)nums[i], width);
                if (map.ContainsKey(bucketId))
                {
                    return true;
                }
                if (map.ContainsKey(bucketId - 1) && Math.Abs(map[bucketId - 1] - nums[i]) < width)
                {
                    return true;
                }
                if (map.ContainsKey(bucketId + 1) && Math.Abs(map[bucketId + 1] - nums[i]) < width)
                {
                    return true;
                }
                map[bucketId] = (long)nums[i];
                if (i - k >= 0)
                {
                    long oldBucketId = GetBucketId((long)nums[i - k], width);
                    map.Remove(oldBucketId);
                }
            }

            return false;
        }

        public static bool ContainsDuplicate(int[] nums)
        {
            Dictionary<int, int> duplicates = new Dictionary<int, int>();
            bool checkDuplicates = false;

            foreach(int element in nums)
            {
                if (duplicates.ContainsKey(element))
                {
                    duplicates[element]++;
                }
                else
                {
                    duplicates.Add(element, 1);
                }
            }

            var getDuplicateMoreThanTwo = duplicates.Where(val => val.Value >= 2).ToList();

            if(getDuplicateMoreThanTwo.Count >= 1)
            {
                checkDuplicates = true;
            }

            return checkDuplicates;
        }

        public static bool WordPattern(string pattern, string str)
        {
            var words = str.Split(' ');
            if(words.Length != pattern.Length)
            {
                return false;
            }
            var seenWords = new HashSet<string>();
            var letterToWordLookup = new Dictionary<char, string>();

            for(int i = 0; i < pattern.Length; i++)
            {
                char letter = pattern[i];
                string word;
                if(!letterToWordLookup.TryGetValue(letter,out word))
                {
                    word = words[i];
                    if (!seenWords.Add(word))
                    {
                        return false;
                    }
                    letterToWordLookup[letter] = word;
                }
                if(word != words[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsIsomorphic(string s, string t)
        {            
            var letter = new Dictionary<char, char>();
            bool result = true;

            if (s.Length != t.Length)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                char charS = s[i];
                char charT = t[i];
                char c;
                if (letter.TryGetValue(charS, out c))
                {
                    if (c != charT)
                    {
                        result = false;
                        break;
                    }
                }
                else if (letter.ContainsValue(charT))
                {
                    result = false;
                    break;
                }
                else
                {
                    letter.Add(charS, charT);
                }
            }

            return result;
        }

        public static int[] IntersectionTwoArraysWithoutRepetitions(int[] nums1, int[] nums2)
        {
            var result = nums1.Intersect(nums2);

            return result.ToArray();
        }

        public static int[] IntersectionTwoArraysWithRepetitions(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                return nums2.Intersect(nums1).ToArray();
            }
            Dictionary<int, int> nums1Count = new Dictionary<int, int>();
            List<int> res = new List<int>();
            foreach (var num in nums1)
            {
                if (!nums1Count.ContainsKey(num))
                {
                    nums1Count.Add(num, 1);
                }
                else
                {
                    nums1Count[num]++;
                }
            }

            foreach (var num in nums2)
            {
                if (nums1Count.ContainsKey(num) && nums1Count[num] > 0)
                {
                    res.Add(num);
                    nums1Count[num]--;
                }
            }

            return res.ToArray();
        }

        public static IList<string> CommonChars(string[] A)
        {
            Dictionary<char, int[]> map = new Dictionary<char, int[]>();

            for (int i = 0; i < A.Length; i++)
            {
                foreach (char j in A[i])
                {
                    if (!map.ContainsKey(j))
                    {
                        map.Add(j, new int[A.Length]);
                    }
                    map[j][i]++;
                }
            }
            List<string> result = new List<string>();
            foreach (var kvp in map)
            {
                int[] arr = kvp.Value;
                int count = arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] < count)
                    {
                        count = arr[i];
                    }
                }
                if (count < 1) continue;
                for (int i = 0; i < count; i++)
                {
                    result.Add(kvp.Key.ToString());
                }
            }
            
            return result;
        }

        public static int LengthOfLongestSubstring(string s)
        {
            int curSize = 0;
            int maxSize = 0;
            int end = 0;
            bool[] present = new bool[256];


            for (int start = 0; start < s.Length; start++)
            {
                end = start;
                while (end < s.Length)
                {
                    if (!present[s[end]] && end < s.Length)
                    {
                        curSize++;
                        present[s[end]] = true;
                        end++;
                    }
                    else
                        break;
                }
                if (curSize > maxSize)
                {
                    maxSize = curSize;
                }
                //reset current size and the set all letter to false
                curSize = 0;
                for (int i = 0; i < present.Length; i++)
                    present[i] = false;
            }

            return maxSize;
        }

        public static bool CheckIfPangram(string sentence)
        {
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Dictionary<char, bool> panagram = new Dictionary<char, bool>();
            bool result = false;
            foreach(char element in alphabet)
            {
                panagram.Add(element, false);
            }

            foreach (char element in sentence.ToCharArray())
            {
                if (panagram.ContainsKey(element))
                {
                    panagram[element] = true;
                }
            }

            var countLetters = panagram.Where(val => val.Value == true).ToList().Count;

            if(countLetters == 26)
            {
                return true;
            }

            return false;
        }

        public static string EntityParser(string text)
        {
            Dictionary<string, char> specialCharacters = new Dictionary<string, char>();

            specialCharacters.Add("&quot;", '\"');
            specialCharacters.Add("&apos;", '\'');
            specialCharacters.Add("&amp;", '&');
            specialCharacters.Add("&gt;", '>');
            specialCharacters.Add("&lt;", '<');
            specialCharacters.Add("&frasl;", '/');

            if(text == "&amp;gt;")
            {
                text = "&gt;";
            }
            else
            {
                foreach (KeyValuePair<string, char> pair in specialCharacters)
                {
                    string key = pair.Key;
                    if (text.Contains(key))
                    {
                        text = text.Replace(key, pair.Value.ToString());
                    }
                }
            }

            return text;
        }

        public static string ReverseVowels(string s)
        {
            int first = 0;
            int last = s.Length - 1;
            StringBuilder sb = new StringBuilder(s);

            while (first < last)
            {
                if (!(sb[first] == 'a' || sb[first] == 'e' || sb[first] == 'i' || sb[first] == 'o' || sb[first] == 'u' ||
                      sb[first] == 'A' || sb[first] == 'E' || sb[first] == 'I' || sb[first] == 'O' || sb[first] == 'U'))
                    first++;
                
                if (!(sb[last] == 'a' || sb[last] == 'e' || sb[last] == 'i' || sb[last] == 'o' || sb[last] == 'u' ||
                      sb[last] == 'A' || sb[last] == 'E' || sb[last] == 'I' || sb[last] == 'O' || sb[last] == 'U'))
                    last--;

                if ((sb[first] == 'a' || sb[first] == 'e' || sb[first] == 'i' || sb[first] == 'o' || sb[first] == 'u' ||
                     sb[first] == 'A' || sb[first] == 'E' || sb[first] == 'I' || sb[first] == 'O' || sb[first] == 'U') &&
                    (sb[last] == 'a' || sb[last] == 'e' || sb[last] == 'i' || sb[last] == 'o' || sb[last] == 'u' ||
                     sb[last] == 'A' || sb[last] == 'E' || sb[last] == 'I' || sb[last] == 'O' || sb[last] == 'U'))
                {                    
                    char temp = s[first];
                    sb[first] = sb[last];
                    sb[last] = temp;

                    first++;
                    last--;
                }
            }

            return sb.ToString();
        }

        public static void ReverseString(char[] s)
        {
            Array.Reverse(s);
        }

        public static string ValidIPAddress(string IP)
        {
            var ipv4 = new Regex("(([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
            
            var ipv6 = new Regex("((([0-9a-fA-F]){1,4})\\:){7}([0-9a-fA-F]){1,4}");
                        
            if (ipv4.Match(IP).Success)
                return "IPv4";            
            else if (ipv6.Match(IP).Success)
                return "IPv6";

            return "Neither";
        }

        public static int BasicCalculator(string s)
        {
            int sum = 0;
            int sign = 1;

            Stack<int> st = new Stack<int>();
            for(int i = 0; i < s.Length; i++)
            {
                char ch = s[i];

                if (Char.IsDigit(ch))
                {
                    int val = 0;
                    while(i < s.Length && Char.IsDigit(s[i]))
                    {
                        val = val * 10 + s[i] - '0';
                        i++;
                    }
                    i--;
                    val = val * sign;
                    sign = 1;
                    sum += val;
                }
                else if (ch.Equals('('))
                {
                    st.Push(sum);
                    st.Push(sign);
                    sum = 0;
                    sign = +1;
                }
                else if (ch.Equals(')'))
                {
                    sum *= st.Pop();
                    sum += st.Pop();
                }
                else if (ch.Equals('-'))
                {
                    sign *= -1;
                }
            }

            return sum;
        }

        public static int ContainerWithMostWater(int[] height)
        {
            int maxArea = 0;
            int i = 0;
            int j = height.Length - 1;

            while(i < j)
            {
                if(height[i] < height[j])
                {
                    maxArea = Math.Max(maxArea, (j - i) * height[i]);
                    i++;
                }
                else
                {
                    maxArea = Math.Max(maxArea, (j - i) * height[j]);
                    j--;
                }
            }

            return maxArea;
        }
    }
}
