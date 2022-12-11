using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        //Name: Dallas Cosman
        //Date: December 06
        //Description: Uses random numbers to create a authorization code, and
        //once logged in by entering the correct code the user can select one
        //of two options. First, swap the ram location of two strings or join
        //the strings together. Second, Generates a list of numbers and finds
        //the sum, mean, and amount of odd numbers.

        //class variables init.
        const string PROGRAMMER = "Dallas Cosman";
        int loginAttempt;

        public Form1()
        {
            InitializeComponent();
        }

        //Hide groups on form load and generate a random auth code
        private void Form1_Load(object sender, EventArgs e)
        {
            Text += " " + PROGRAMMER;
            grpChoose.Visible = false;
            grpStats.Visible = false;
            grpText.Visible = false;
            txtCode.Focus();
            lblCode.Text = GetRandom(100000, 200000 + 1).ToString();
        }

        //Function to generate a random number from the min and max values sent to the function
        private int GetRandom(int min, int max)
        {
            Random rand = new Random();
            int generatedNum = rand.Next(min, max);
            return generatedNum;
        }

        //Reset the text group and change accept and cancel buttons
        private void ResetTextGrp()
        {
            txtString1.Text = "";
            txtString2.Text = "";
            lblResults.Text = "";
            chkSwap.Checked = false;
            txtString1.Focus();
            AcceptButton = btnJoin;
            CancelButton = btnReset;
        }

        //Reset the stat group and change accept and cancel buttons
        private void ResetStatsGrp()
        {
            lblSum.Text = "";
            lblMean.Text = "";
            lblOdd.Text = "";
            lstNumbers.Items.Clear();
            nudGenerateAmount.Value = nudGenerateAmount.Minimum;
            AcceptButton = btnGenerate;
            CancelButton = btnClear;
        }

        //Checks which radio button is selected and shows the according box and clears the info inside it
        private void SetupOption()
        {
            if (radStats.Checked)
            {
                ResetStatsGrp();
                grpStats.Visible = true;
                grpText.Visible = false;
                txtString1.Focus();
            }
            else
            {
                ResetTextGrp();
                grpText.Visible = true;
                grpStats.Visible = false;
                nudGenerateAmount.Focus();
            }
        }

        //Swaps the RAM location of two strings sent to the function
        private void Swap(ref string firstString, ref string secondString)
        {
            string holder = secondString;
            secondString = firstString;
            firstString = holder;
        }

        //Returns true if the two strings in the text boxes within the textgroup are not empty
        private bool CheckInput()
        {
            bool chkInput = false;
            if (txtString1.Text != "" && txtString2.Text != "")
                chkInput = true;
            return chkInput;
        }

        //Finds the sum of the list of randomly generated numbers.
        private int AddList
        {
            int total = 0, index = 0;
            lstNumbers.SelectedIndex = index;
            while (index < lstNumbers.Items.Count)
            {
                total += int.Parse(lstNumbers.SelectedItem.ToString());
                index++;
                if (index < lstNumbers.Items.Count)
                    lstNumbers.SelectedIndex = index;
            }
            return total;

        }

        //Counts the amount of odd numbers within the list of numbers
        private int CountOdd()
        {
            int count = 0, index = 0;
            lstNumbers.SelectedIndex = index;
            do
            {
                if (int.Parse(lstNumbers.SelectedItem.ToString()) / 2 != 0)
                    ++count;
                index++;
                if (index < lstNumbers.Items.Count)
                    lstNumbers.SelectedIndex = index;
            } while (index < lstNumbers.Items.Count);
            return count;
        }

        //When the login button is clicked show the radio buttons, disable the login group, and run the SetupOption function.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == lblCode.Text)
            {
                grpChoose.Visible = true;
                grpLogin.Enabled = false;
                SetupOption();
            }

            //if the users code entered does not match the auth code then increment the number of login attempts
            //if 3 login attempts are made then close the program.
            else
            {
                loginAttempt++;
                if (loginAttempt < 3)
                {
                    MessageBox.Show(loginAttempt + " incorrect code(s) entered\nTry again - only 3 attempts allowed", PROGRAMMER);
                    txtCode.Text = "";
                    txtCode.Focus();
                }

                else
                {
                    MessageBox.Show(loginAttempt + " attempts to login\nAccount locked - Closing program", PROGRAMMER);
                    Close();
                }
            }
        }

        //When the text radio button is selected run the SetupOption
        private void radText_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }

        //When the stats radio button is selected run the SetupOption
        private void radStats_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }

        //When the reset button is clicked reset the text group by calling ResetTextGrp function
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextGrp();
        }

        //When the clear button is clicked reset the stats group by calling ResetStatsGrp function
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetStatsGrp();
        }

        //When the Swap check box is clicked, use the Swap function to switch the
        //RAM locations and display a message describing what the event did
        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            string string1 = txtString1.Text, string2 = txtString2.Text;
            if (CheckInput() && chkSwap.Checked)
            {
                Swap(ref string1, ref string2);
                txtString1.Text = string1;
                txtString2.Text = string2;
                lblResults = "Strings have been swapped!";
            }
        }

        //When the join button is clicked, check that the inputs are not empty by using
        //CheckInput, then join the two strings if CheckInput is true.
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (CheckInput())
                lblResults.Text = "First string = " + txtString1.Text + "\nSecond string = " + txtString2.Text + "\nJoined = " + txtString1.Text + "-->" + txtString2.Text;
        }

        //Checks if the text boxes are empty, if they are not then join the two strings.
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (CheckInput())
                lblResults.Text = "First string = " + txtString1.Text + "\n Characters = " + txtString1.Text.Length + "\nSecond string = " + txtString2.Text + "\n Characters = " + txtString2.Text.Length;
        }

        //When the generate button is clicked, clear the previous set of numbers in the list
        //then, depending on the numberic value selected generate a list of random numbers matching
        //the selected numeric. Finally, calls functions to generate the sum, and amount of odd
        //numbers. And, also calculates the mean of the list of numbers.
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Random rand = new Random(733);
            lstNumbers.Items.Clear();
            for(int i=0; i<nudGenerateAmount.Value; i++)
            {
                lstNumbers.Items.Add(rand.Next(1000, 5000+1));
            }
            lblSum.Text = AddList().ToString("n0");
            lblMean.Text = (double.Parse(lblSum.Text) / lstNumbers.Items.Count).ToString("n2");
            lblOdd.Text = CountOdd().ToString();
        }
    }
}
