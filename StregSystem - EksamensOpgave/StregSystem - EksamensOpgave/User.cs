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
        private Guid _userID;
        private string _firstName;
        private string _lastName;
        private string _userName;
        
        private string _emailAddress;

        private decimal _balance;

        //pga af at et brugernavn nødvendigvis skal være unikt, kan et userid konstrueres ud fra brugernavnet. så måske skal userid ikke være GUID?
        //Sørg for at firstname/lastname aldrig er null? i constructoren? er det nok?

        public User(string firstName, string lastName, string userName, string emailAddress )
        {
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;
            _emailAddress = emailAddress;
            _balance = 0;
            _userID = Guid.NewGuid();
        }

        #region Get/set
        public Guid UserID
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
