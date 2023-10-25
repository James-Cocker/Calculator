using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public int Number1, Number2;
        string Num2;
        string Operation;
        int PreviousAns, Length;
        bool TakeInputForNumber2 = false;

        //String CalcInput;

        // Initialise the Calculator upon launch
        public Form1()
        {
            InitializeComponent();
        }

        // On Calculator load:
        private void Form1_Load(object sender, EventArgs e)
        {
            txtOutput.Enabled = false;
        }

        // ----------------------------------------- //
        //           Calculator Functions            //
        // ----------------------------------------- //

        // When the 'On' Button is clicked
        private void btnOn_Click(object sender, EventArgs e)
        {
            txtOutput.Enabled = true;
            txtOutput.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5E0B4");
            txtOutput.BorderStyle = BorderStyle.FixedSingle;
        }

        // When the 'Off' button is clicked
        private void btnOff_Click(object sender, EventArgs e)
        {
            // Close the form as they have no need for the calculator
            this.Close();
        }

        // Check if text is being added
        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            // Check if the 'On' button has been clicked by checking if the text box is active
            if (!txtOutput.Enabled)
            {
                // Remove any text entered if the calculator has not been turned on
                txtOutput.Text = "";
            }


            if (TakeInputForNumber2)
            {
                string Output = txtOutput.Text;
                char LastCharacter;
                Length = Output.Length;

                LastCharacter = Output[Length-1];
                Num2 += LastCharacter;
            }
            else
            {
                Num2 = "";
            }
        }

        // When the user clicks the left arrow
        private void btnLeft_Click(object sender, EventArgs e)
        {

        }

        // When the user clicks the 'Delete' key
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Length = (txtOutput.Text).Length;
            if (((txtOutput.Text).Length) > 0)
            {
                string CalcText = txtOutput.Text;
                string Output = CalcText.Remove(CalcText.Length - 1, 1);
                txtOutput.Text = Output;
            }
            // If the user deletes all the characters in the calculator, reset Number 1
            if (Length-1 <= 0)
            {
                Number1 = 0;
            }
            else
            {
                Number1 = PreviousAns;
            }
        }

        // When the user clicks the 'Clear' key
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            Number1 = 0;
            Number2 = 0;
            TakeInputForNumber2 = false;
        }

        // When the user clicks the 'Ans' key
        private void btnAns_Click(object sender, EventArgs e)
        {
            // ANS
            // PreviousAns
        }

        int GetNumber1()
        {
            if (txtOutput.Text != "")
            {
                Number1 = Int32.Parse(txtOutput.Text);
                TakeInputForNumber2 = true;
            }
            //else
            //{
            //   TakeInputForNumber2 = false;
            //}
            //TakeInputForNumber2 = true;
            return Number1;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "+";
            Operation = "+";
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "-";
            Operation = "-";
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "X";
            Operation = "X";
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "÷";
            Operation = "/";
        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "Sin(";
            Operation = "Sin";
        }

        private void btnCos_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "Cos(";
            Operation = "Cos";
        }

        private void btnRoot_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "(√";
            Operation = "√";
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "^(";
            Operation = "Sqr";
        }

        private void btnTan_Click_1(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            txtOutput.Text += "Tan(";
            Operation = "Tan";
        }

        // When the user tries to solve a calculation
        void btnEqual_Click(object sender, EventArgs e)
        {
            // EQUALS
            
            Number2 = Int32.Parse(Num2);

            switch (Operation)
            {
                case "+":
                    PreviousAns = Number1 + Number2; TakeInputForNumber2 = true; break;
                case "-":
                    PreviousAns = Number1 - Number2; TakeInputForNumber2 = true; break;
                case "*":
                    PreviousAns = Number1 * Number2; TakeInputForNumber2 = true; break;
                case "/":
                    PreviousAns = Number1 / Number2; TakeInputForNumber2 = true; break;
                default:
                    // No operation, so equate current number to answer
                    PreviousAns = Int32.Parse(txtOutput.Text); break;
            }


            //Console.WriteLine(Number1.ToString());
            //Console.WriteLine(Number2.ToString());
            //Console.WriteLine(PreviousAns.ToString());

            // Reset values after calculation, number 1 has already been reset
            Number2 = 0;
            Num2 = "";
            Operation = "";
            txtOutput.Text = PreviousAns.ToString();
            TakeInputForNumber2 = false;
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter key is down
                
            }
        }

        // Solving the calculation if the user has pressed '=' or pressed enter
        public void Solve()
        {
            
        }

        // ----------------------------------------- //
        //            Button Text input              //
        // ----------------------------------------- //

        private void btn0_Click_1(object sender, EventArgs e)
        {
            txtOutput.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "9";
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            txtOutput.Text += ".";
        }

        private void btnPie_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "π";
        }
    }
}
