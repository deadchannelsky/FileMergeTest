using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileMergeTest
{
    class StringEqualityComparer : IEqualityComparer<string>
    {
        // Lower-case your strings for a case insensitive compare.
        public bool Equals(string s1, string s2)
        {
            if (s1.ToLower().Equals(s2.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IEqualityComparer<string> Members
        public int GetHashCode(string s)
        {
            return s.GetHashCode();
        }

        #endregion
    }
}
