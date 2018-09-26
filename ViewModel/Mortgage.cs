using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MortgageCalculator.Desktop.ViewModel
{
    public class Mortgage : INotifyPropertyChanged
    {
        private int totalAmount;
        public int TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
                OnPropertyChanged("Loan");
                RaiseRecalculatePayment();
            }
        }

        private float interestRate;
        public float InterestRate
        {
            get { return interestRate; }
            set
            {
                interestRate = value;
                OnPropertyChanged("InterestRate");
                OnPropertyChanged("MonthlyInterest");
                RaiseRecalculatePayment();
            }

        }

        private int downPayment;
        public int DownPayment
        {
            get { return downPayment; }
            set
            {
                downPayment = value;
                OnPropertyChanged("DownPayment");
                OnPropertyChanged("Loan");
                RaiseRecalculatePayment();
            }
        }

        private int term;
        public int Term
        {
            get { return term; }
            set
            {
                term = value;
                OnPropertyChanged("Term");
                OnPropertyChanged("MonthsOfPayments");
                RaiseRecalculatePayment();
            }
        }

        public decimal Loan => (TotalAmount - DownPayment);
        public float MonthlyInterest => InterestRate / 100 / 12;
        public int MonthsOfPayments => Term * 12;

        private decimal monthlyPayment;
        public decimal MonthlyPayment
        {
            get
            {
                if (monthlyPayment == 0 && Loan > 0 && MonthsOfPayments > 0)
                {
                    if (MonthlyInterest == 0)
                    {
                        monthlyPayment = Loan / MonthsOfPayments;
                    }
                    else
                    {
                        // P = L[c(1 + c)^n] / [(1 + c)^n - 1]
                        monthlyPayment = Math.Round(
                            ((Loan * (decimal)(MonthlyInterest * Math.Pow(1 + MonthlyInterest, MonthsOfPayments)))
                            / (decimal)(Math.Pow(1 + MonthlyInterest, MonthsOfPayments) - 1)), 2
                              );
                    }
                }
                return monthlyPayment;
            }
        }

        public decimal TotalPayment => MonthlyPayment * MonthsOfPayments;
        public decimal TotalInterest => TotalPayment - Loan;
        private void RaiseRecalculatePayment()
        {
            OnPropertyChanged("MonthlyPayment");
            OnPropertyChanged("TotalPayment");
            OnPropertyChanged("TotalInterest");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            monthlyPayment = 0;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
