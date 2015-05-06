using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class Product
    {
        protected int _productID;
        protected string _productName;
        protected decimal _price;
        protected bool _active;
        protected bool _canBeBoughtOncredit;

        public Product()
        {
            //this should never be used
        }
        public Product(string productName, decimal productPrice, bool active, bool canBeBoughtOnCredit)
        {
            //der skal somehow checkes hvilke numre der er tilgængelige...måske skal det bare dumbes down til at de bliver assignet i en rækkefølge på runtime?
            //Måske skal productID ind som parameter, og hvis id'et så er taget skal der bedes om et nyt
            _productName = productName;
            _price = productPrice;
            _active = active;
            _canBeBoughtOncredit = canBeBoughtOnCredit;
        }

        #region Getter/setter

        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool CanBeBoughtOnCredit
        {
            get { return _canBeBoughtOncredit; }
            set { _canBeBoughtOncredit = value; }
        }

        #endregion





    }
}
