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
            account = new CreditAccount("2022302111073", 20000, 15000);
            atm = new ATM();
            atm.BigMoneyFetched += Atm_BigMoneyFetched;
            lblBalance.Text = $"当前余额为: {account.Balance}";
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            decimal amount;

            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("请输入正确的金额格式。");
                return;
            }

            try
            {
                atm.Withdraw(account, amount);
                lblBalance.Text = $"当前余额为: {account.Balance}";
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
                throw new BadCashException("取款时有坏钞出现，此ATM已崩溃!");
            }
        }
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("请输入正确的金额格式。");
                return;
            }

            account.Deposit(amount);
            lblBalance.Text = $"Balance: {account.Balance}";
        }

        private void Atm_BigMoneyFetched(object sender, BigMoneyArgs e)
        {
            MessageBox.Show($"检测到大额取款！\r【取款账户】: {e.AccountNumber}\r【取款金额】: {e.Amount} 元（人民币）");
        }
    }
}
