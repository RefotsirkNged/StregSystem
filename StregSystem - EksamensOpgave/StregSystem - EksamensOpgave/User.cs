using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StregSystem___EksamensOpgave
{
    class User : IComparable
    {
        private string _firstName;
        private string _lastName;
        private string _userName;
        private static int _userIDIncrementor;
        private string _emailAddress;
        private int _userID;
        private decimal _balance;


        public User(string firstName, string lastName, string userName, string emailAddress )
        {
            Regex rxUserName = new Regex(@"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-z0-9._]+(?<![_.])$");
            Regex rxEmailAddress = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            _firstName = firstName;
            _lastName = lastName;
            if (!rxUserName.IsMatch(userName))
            {
                throw new ArgumentException("Username is not valid!");
            }
            _userName = userName;
            if (!rxEmailAddress.IsMatch(emailAddress))
            {
                throw new ArgumentException("Email is not valid!");
            }
            _emailAddress = emailAddress;
            _balance = 0;
            User._userIDIncrementor++;
            _userID = _userIDIncrementor;
        }



        #region Get/set
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string EmailAdrress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        } 
        #endregion

        #region Overriding ToString, Equals and a version of icomparable

        public override string ToString()
        {
            return _firstName + _lastName + "(" + _emailAddress + ")";
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            User u = obj as User;
            if ((System.Object)u == null)
            {
                return false;
            }

            return (this._userID == u._userID);
        }

        public override int GetHashCode()
        {
            return this.UserName.GetHashCode() ^ this.FirstName.GetHashCode() ^ this.LastName.GetHashCode();
        }

        //MSDN ftw
        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }
            User u = obj as User;
            if (u != null)
            {
                return this.UserID.CompareTo(u.UserID);
            }
            else
                throw new ArgumentException("Object Inputted to compare, wasnt a correct User Object!");


        }
        #endregion


    }
}
