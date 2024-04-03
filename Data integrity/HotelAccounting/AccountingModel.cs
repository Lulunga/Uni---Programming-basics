using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;
        private bool checkIfNested;
        public double Price
        {
            get => price;
            set
            {
                if (value == price) return;
                if (value < 0) throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                RecalculateTotal();
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value == nightsCount) return;
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                RecalculateTotal();
            }
        }
        public double Discount
        {
            get => discount;
            set
            {
                if (value == discount) return;
                if (value > 100.00) throw new ArgumentException();
                discount = value;
                Notify(nameof(Discount));
                if (!checkIfNested) RecalculateTotal();
            }
        }

        public double Total
        {
            get => total;
            set
            {
                if (value == total) return;
                if (value < 0) throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                if (!checkIfNested) RecalculateDiscount();
            }
        }

        private void RecalculateTotal()
        {
            checkIfNested = true;
            Total = Price * NightsCount * (1.0 - Discount / 100.0);
            checkIfNested = false;
        }

        private void RecalculateDiscount()
        {
            checkIfNested = true;
            if (Price == 0) Discount = 100.0;
            else Discount = 100.0 * (1 - Total / (Price * NightsCount));
            checkIfNested = false;
        }
    }
}