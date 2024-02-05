using System;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




namespace _3_Working_With_Some_Guys
{
    public partial class Form1 : Form
    {
        Guy joe;
        Guy bob;
        int bank = 100;
        public void UpdateForm()
        {
            joesCash.Text = joe.Name + " has $" + joe.Cash;
            bobsCash.Text = bob.Name + " has $" + bob.Cash;
            BankCash.Text = "The bank has $" + bank;
        }
        
        public Form1()
        {
            InitializeComponent();
            bob = new Guy()
            {
                Cash = 100,
                Name = "Bob"
            };
            


            joe = new Guy() { 
                Cash = 50,
                Name = "Joe"
            };
            

            UpdateForm();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(bank >= 10)
            {
                bank -= joe.ReceiveCash(10);
                UpdateForm();
            }
            else
            {
                MessageBox.Show("The bank is out of money");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bank += bob.GiveCash(5);
            UpdateForm();
        }

        

        

        private void joeGivesToBob_Click(object sender, EventArgs e)
        {
            bob.ReceiveCash(joe.GiveCash(10));
            UpdateForm();
        }

        private void bobGivesToJoe_Click(object sender, EventArgs e)
        {
            joe.ReceiveCash(bob.GiveCash(5));
            UpdateForm();
        }

        private void saveJoe_Click(object sender, EventArgs e)
        {
            using (Stream output = File.Create("Guy_File.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, joe);
            }
        }

        private void loadJoe_Click(object sender, EventArgs e)
        {
            if (!File.Exists("Guy_File.dat"))
            {
                MessageBox.Show("File doesn't exist", "Not found", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            using(Stream input = File.OpenRead("Guy_File.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                joe = (Guy)formatter.Deserialize(input);
            }
            this.UpdateForm();
        }
    }
}
