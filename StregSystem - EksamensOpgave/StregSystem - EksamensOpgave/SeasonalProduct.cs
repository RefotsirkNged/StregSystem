using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class SeasonalProduct : Product
    {
        private DateTime _seasonStartDate;
        private DateTime _seasonEndDate;

        public SeasonalProduct(string productName, decimal productPrice, bool active, bool canBeBoughtOnCredit, DateTime seasonStartDate, DateTime seasonEndDate)
        {
            //der skal somehow checkes hvilke numre der er tilgængelige...måske skal det bare dumbes down til at de bliver assignet i en rækkefølge på runtime?
            //Måske skal productID ind som parameter, og hvis id'et så er taget skal der bedes om et nyt
            _productName = productName;
            _price = productPrice;
            _active = active;
            _canBeBoughtOncredit = canBeBoughtOnCredit;
            _seasonStartDate = seasonStartDate;
            _seasonEndDate = seasonEndDate;
        }

        #region Getter/setter
        public DateTime SeasonStartDate
        {
            get { return _seasonStartDate; }
            set { _seasonStartDate = value; }
        }

        public DateTime SeasonEndDate
        {
            get { return _seasonEndDate; }
            set { _seasonEndDate = value; }
        }
        #endregion
    }
}
