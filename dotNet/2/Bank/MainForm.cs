using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class MainForm : Form
    {
        private Bank bank;
        private Account account;
        private ATM atm;

        public MainForm()
        {
            InitializeComponent();
            bank = new Bank("MyBank");
            account = new CreditAccount("123456", 5000, 2000);
            atm = new ATM();
            atm.BigMoneyFetched += Atm_BigMoneyFetched;
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                atm.Withdraw(account, amount);
                lblBalance.Text = $"Balance: {account.Balance}";
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BadCashException ex)
            {
                MessageBox.Show(ex.Message);
            }

            // 模拟破损钞票
            Random random = new Random();
            if (random.NextDouble() < 0.3)
            {
                throw new BadCashException("Detected bad cash!");
            }
        }

        private void Atm_BigMoneyFetched(object sender, BigMoneyArgs e)
        {
            MessageBox.Show($"Alert: Big money fetched! Account: {e.AccountNumber}, Amount: {e.Amount}");
        }
    }
}
