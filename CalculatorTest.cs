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
        bool NoNum1 = false, Shift = false, ModeMenu = false, CalcOn = false, Error = true, GraphingMenu = false, LineMode = false;

        List<string> AllowedHex = new List<string>
        {
            "A","B","C","D","E","F","G","H","H","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","1","2","3","4","5","6","7","8","9","0"
        };

        string Operation = "", Binary, Hex;

        const double Degrad = Math.PI / 180;
        const double Rad = 180 / Math.PI;

        int Length, Num1Length, Num2Length, CurrentMode, Denary, a = 0, b = 0, c = 0;
        float Number1, Number2;
        double PreviousAns;

        // Initialize sound
        //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\Button.wav");
        //player.Play();


        // Initialise the Calculator upon launch
        public Form1()
        {
            InitializeComponent();
        }

        // On Calculator load:
        private void Form1_Load(object sender, EventArgs e)
        {
            // Disable all GUI on calculator upon load
            DisableUnwantedGUI();
        }

        // ----------------------------------------- //
        //           Calculator Functions            //
        // ----------------------------------------- //

        // When the 'On' Button is clicked
        private void btnOn_Click(object sender, EventArgs e)
        {
            // Set current mode to scientific calculator
            CurrentMode = 1;

            // Tell the rest of the code the calculator is on
            CalcOn = true;

            // Make the shift key visible and enable it
            lblShift.Visible = true;
            txtOutput.Enabled = true;

            // Enable and make output textbox visible
            txtOutput.Enabled = true;
            txtOutput.Visible = true;

            // Turn on a text box border and make its background colour sightly different
            txtOutput.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5E0B4");
            txtOutput.BorderStyle = BorderStyle.FixedSingle;

            // And Clear the calculator
            EnableOperations();
            Clear();
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
            // Check if the 'On' button has been clicked
            if (!CalcOn)
            {
                // Remove any text entered if the calculator has not been turned on
                txtOutput.Text = "";
            }
        }

        // When the user clicks the left arrow
        private void btnLeft_Click(object sender, EventArgs e)
        {

        }

        // When switching modes, or on calculator load, disable unwanted GUI items
        void DisableUnwantedGUI()
        {
            // Whether its on or off at the time, turn on main output, clear it and turn it off
            //if (txtOutput.Enabled == false)
            //{
            //    txtOutput.Enabled = true;
            //}
            //Clear();

            txtOutput.Visible = false;
            txtOutput.Enabled = false;

            lblShift.Enabled = false;
            pictureBox1.Visible = false;
            pictureBox1.Enabled = false;
            txtOutput.Enabled = false;

            lblHex.Visible = false; lblHex.Enabled = false;
            lblBinary.Visible = false; lblBinary.Enabled = false;
            lblDenary.Visible = false; lblDenary.Enabled = false;
            txtHex.Visible = false; txtHex.Enabled = false;
            txtBinary.Visible = false; txtBinary.Enabled = false;
            txtDenary.Visible = false; txtDenary.Enabled = false;

            txtHex.BorderStyle = BorderStyle.None;
            txtBinary.BorderStyle = BorderStyle.None;
            txtDenary.BorderStyle = BorderStyle.None;

            lblA.Visible = false;
            lblB.Visible = false;
            lblC.Visible = false;
            txtA.Enabled = false;
            txtB.Enabled = false;
            txtC.Enabled = false;
            lblRoots.Visible = false;

            txtA.BorderStyle = BorderStyle.None;
            txtB.BorderStyle = BorderStyle.None;
            txtC.BorderStyle = BorderStyle.None;

            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
            pictureBox1.BorderStyle = BorderStyle.None;
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
            // If the user deletes all the characters in the calculator, reset Number 1 and operation
            if (Length-1 <= 0)
            {
                Number1 = 0;
                Operation = "";
                EnableOperations();
            }
        }

        // When the user clicks the 'Clear' key
        private void btnClear_Click(object sender, EventArgs e)
        {
            EnableOperations();
            Clear();
        }

        void Clear()
        {
            // Enable all operations and reset variables
            EnableOperations();
            txtOutput.Text = "";
            Number1 = 0;
            Number2 = 0;
            Operation = "";
            NoNum1 = false;
            Shift = false;
        }

        void SwapShift()
        {
            Shift = !Shift;
            lblShift.Enabled = Shift;
        }

        // When the user clicks the 'Ans' key
        private void btnAns_Click(object sender, EventArgs e)
        {
            // ANS
            txtOutput.Text += PreviousAns.ToString();
        }

        void ToggleSwitchCalculator()
        {
            // Get rid of all GUI on screen
            DisableUnwantedGUI();

            // Toggle the main output and the text (for the menu) output
            lblText.Enabled = !ModeMenu;
            lblText.Visible = !ModeMenu;

            // If the user wants to change modes...
            if (!ModeMenu)
            {
                lblText.Text = "1) Scientific Calculator\n2) Programmer\n3) Graphing";
                lblText.BringToFront();
            }

            // Switch mode menu variable
            ModeMenu = !ModeMenu;
        }




        // CODE FOR SCIENTIFIC CALCULATOR ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        float GetNumber1()
        {
            if (txtOutput.Text != "")
            {
                // Try and catch getting number 1 as a float
                try
                {
                    Number1 = float.Parse(txtOutput.Text);
                }
                catch
                {
                    txtOutput.Text = "Please enter a correct value for number 1";
                }

            }
            else if (Operation == " Sin" || Operation == " Cos" || Operation == " Tan" || Operation == " Sin⁻¹" || Operation == " Cos⁻¹" || Operation == " Tan⁻¹" || Operation == " √")
            {
                // Set number1 to 1, as we will multiply this by the trig of the angle (number 2)
                Number1 = 1;
                NoNum1 = true;
            }
            return Number1;
        }

        // Getting number 2
        float GetNumber2()
        {
            // Get the output
            string Output = txtOutput.Text;
            // Get the total output length
            int TotalLength = Output.Length;
            
            // Calculate the length of number 2 by subracting the total by the length of number, and the length of the operation
            if (NoNum1)
            {
                Num1Length = 0;
                Num2Length = TotalLength - ((Operation).Length);
            }
            else
            {
                Num1Length = (Number1.ToString()).Length;
                Num2Length = TotalLength - (Num1Length + (Operation).Length);
            }

            Console.WriteLine(Output);
            Console.WriteLine(TotalLength);
            Console.WriteLine(Num1Length);
            Console.WriteLine(Num2Length);
            PrintVariables();

            // Separate out number 2 from the whole text output and parse it into a float
            String Num2 = Output.Substring(Num1Length + (Operation).Length, Num2Length);
            float Number2 = float.Parse(Num2);

            Console.WriteLine(Num2);

            return Number2;
        }

        void DisableOperations()
        {
            // Disable all operation buttons
            btnSubtract.Enabled = false;
            btnMultiply.Enabled = false;
            btnDivide.Enabled = false;
            btnPower.Enabled = false;
            btnRoot.Enabled = false;
            btnPlus.Enabled = false;
            btnSin.Enabled = false;
            btnTan.Enabled = false;
            btnCos.Enabled = false;
            btnMod.Enabled = false;
            btnDiv.Enabled = false;
            btnPie.Enabled = false;
        }

        void EnableOperations()
        {
            // Enable all operation buttons
            btnSubtract.Enabled = true;
            btnMultiply.Enabled = true;
            btnDivide.Enabled = true;
            btnPower.Enabled = true;
            btnRoot.Enabled = true;
            btnPlus.Enabled = true;
            btnSin.Enabled = true;
            btnTan.Enabled = true;
            btnCos.Enabled = true;
            btnMod.Enabled = true;
            btnDiv.Enabled = true;
            btnPie.Enabled = true;
        }

        

        // For all the operations, when their button is pressed grab number 1 then record the respective operation and add it to the display
        private void btnPlus_Click(object sender, EventArgs e)
        {
            Operation = " + ";
            DisableOperations();
            Number1 = GetNumber1();
            // After they have entered the first number, allow them to do another decimal number 
            //btnDot.Enabled = true;
            txtOutput.Text += " + ";
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            Operation = " - ";
            Number1 = GetNumber1();
            txtOutput.Text += " - ";
            DisableOperations();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            Operation = " X ";
            Number1 = GetNumber1();
            txtOutput.Text += " X ";
            DisableOperations();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            Operation = " / ";
            Number1 = GetNumber1();
            txtOutput.Text += " ÷ ";
            DisableOperations();
        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            if (!Shift)
            {
                Operation = " Sin";
                Number1 = GetNumber1();
                txtOutput.Text += " Sin";
            }
            else
            {
                Operation = " Sin⁻¹";
                Number1 = GetNumber1();
                txtOutput.Text += " Sin⁻¹";
                SwapShift();
            }
            DisableOperations();
        }

        private void btnCos_Click(object sender, EventArgs e)
        {
            if (!Shift)
            {
                Operation = " Cos";
                Number1 = GetNumber1();
                txtOutput.Text += " Cos";
            }
            else
            {
                Operation = " Cos⁻¹";
                Number1 = GetNumber1();
                txtOutput.Text += " Cos⁻¹";
                SwapShift();
            }
            DisableOperations();
        }

        private void btnTan_Click_1(object sender, EventArgs e)
        {
            if (!Shift)
            {
                Operation = " Tan";
                Number1 = GetNumber1();
                txtOutput.Text += " Tan";
            }
            else
            {
                Operation = " Tan⁻¹";
                Number1 = GetNumber1();
                txtOutput.Text += " Tan⁻¹";
                SwapShift();
            }
            DisableOperations();
        }

        private void btnRoot_Click(object sender, EventArgs e)
        {
            if (!Shift)
            {
                Operation = " √";
                Number1 = GetNumber1();
                txtOutput.Text += " √";
            }
            else
            {
                Operation = "^√";
                Number1 = GetNumber1();
                txtOutput.Text += "^√";
                SwapShift();
            }
            DisableOperations();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            Operation = "^";
            Number1 = GetNumber1();
            txtOutput.Text += "^";
            DisableOperations();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            Operation = " % ";
            Number1 = GetNumber1();
            txtOutput.Text += " % ";
            DisableOperations();
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            Operation = " DIV ";
            Number1 = GetNumber1();
            txtOutput.Text += " DIV ";
            DisableOperations();
        }

        private void btnPie_Click(object sender, EventArgs e)
        {
            Number1 = GetNumber1();
            if (txtOutput.Text == "")
            {
                txtOutput.Text += "3.14";
                Number1 = GetNumber1();
            }
            else
            {
                Operation = " X ";
                txtOutput.Text += " X 3.14";
            }
            DisableOperations();
        }

        // When the user presses the equals sign
        void btnEqual_Click(object sender, EventArgs e)
        {
            // EQUALS
            Solve();
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter key is down
                Solve();
            }
        }

        // Solving the calculation if the user has pressed '=' or pressed enter
        public void Solve()
        {
            // Get number 2 after = is pressed (using string manipulation)
            if (Operation != "")
            {
                Number2 = GetNumber2();
            }

            // Check the operation, then calculate the answer
            switch (Operation)
            {
                // For each answer, round to 3 s.f. using Math.Round(value * 1000) / 1000
                case " + ":
                    PreviousAns = Math.Round((Number1 + Number2) * 1000) / 1000; break;
                case " - ":
                    PreviousAns = Math.Round((Number1 - Number2) * 1000) / 1000; break;
                case " X ":
                    PreviousAns = Math.Round((Number1 * Number2) * 1000) / 1000; break;
                case " / ":
                    PreviousAns = Math.Round((Number1 / Number2) * 1000) / 1000; break;
                case " √":
                    PreviousAns = Math.Round((Convert.ToDouble(Number1) * Math.Sqrt(Convert.ToDouble(Number2))) * 1000) / 1000; break;
                case "^√":
                    PreviousAns = Math.Round(Math.Pow(Number2, (1 / Number1)) * 1000) / 1000; break;
                case "^":
                    PreviousAns = Math.Round((Math.Pow(Number1, Number2)) * 1000) / 1000; break;
                case " Tan":
                    PreviousAns = Number1 * Math.Round((Math.Tan(Degrad * Number2)) * 1000) / 1000; break;
                case " Sin":
                    PreviousAns = Number1 * Math.Round((Math.Sin(Degrad * Number2)) * 1000) / 1000; break;
                case " Cos":
                    PreviousAns = Number1 * Math.Round((Math.Cos(Degrad * Number2)) * 1000) / 1000; break;
                case " Tan⁻¹":
                    PreviousAns = Number1 * Math.Round(Rad * (Math.Atan(Number2)) * 1000) / 1000; break;
                // For inverse Sin and Cos, if the number is not in the range on -1 and 1, show them a message box telling them this, then clear the calculator screen for next calculation
                case " Sin⁻¹":
                    if (Number2 > 1 || Number2 < -1){MessageBox.Show("Please enter a number between -1 and 1", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); Clear();}
                    else{PreviousAns = Number1 * Math.Round(Rad * (Math.Asin(Number2)) * 1000) / 1000;} break;
                case " Cos⁻¹":
                    if (Number2 > 1 || Number2 < -1){MessageBox.Show("Please enter a number between -1 and 1", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); Clear();}
                    else{PreviousAns = Number1 * Math.Round(Rad * (Math.Acos(Number2)) * 1000) / 1000;}break;
                case " % ":
                    PreviousAns = Math.Round((Number1 % Number2) * 1000) / 1000; break;
                case " DIV ":
                    PreviousAns = Math.Round((Math.Floor(Number1 / Number2)) * 1000) / 1000; break;
                default:
                    // No operation, so equate current number to answer
                    PreviousAns = Math.Round(float.Parse(txtOutput.Text) * 1000) / 1000; break;
            }

            //PrintVariables();

            // Reset values after calculation
            Clear();

            // Display answer, but if it is infinite, set to zero
            txtOutput.Text = PreviousAns.ToString();
            if (txtOutput.Text == "∞")
            {
                txtOutput.Text = "0";
            }
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
            if (ModeMenu)
            {
                // Set current mode to scientific calculator, and open the routine in they're in the mode menu
                CurrentMode = 1;
                ToggleSwitchCalculator();
                EnableOperations();
                txtOutput.Enabled = true;
                txtOutput.Visible = true;
            }
            else if (GraphingMenu)
            {
                LineGradient();
                GraphingMenu = false;
            }
            else
            {
                txtOutput.Text += "1";
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (ModeMenu)
            {
                // Set current mode to Programmer, and open the routine in they're in the mode menu
                CurrentMode = 2;
                Programmer();
            }
            else if (GraphingMenu)
            {
                Lines();
                GraphingMenu = false;
            }
            else
            {
                txtOutput.Text += "2";
            } 
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (ModeMenu)
            {
                // Set current mode to graphing, and open the routine in they're in the mode menu
                CurrentMode = 3;
                lblText.Text = "1) Get line gradient\n2) Graph out lines\n3) Solve quadratic";
                GraphingMenu = true;
                ModeMenu = false;
            }
            else if (GraphingMenu)
            {
                Quadratic();
                GraphingMenu = false;
            }
            else
            {
                txtOutput.Text += "3";
            }
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

        private void button9_Click(object sender, EventArgs e)
        {
            txtOutput.Text += ")";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtOutput.Text += "(";
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            // If the calculator is turned on, let the user press shift
            if (txtOutput.Enabled == true)
            {
                Shift = !Shift;
                lblShift.Enabled = Shift;
            }
        }

        private void btnSwitchMode_Click(object sender, EventArgs e)
        {
            
            // If the mode menu is not active and calculator is on...
            if (!ModeMenu && CalcOn)
            {
                // Disable and hide calculator functions
                DisableOperations();
                ToggleSwitchCalculator();
            }
            else if(CalcOn)
            {
                // Otherwise enable functions again and switch of menu
                EnableOperations();
                ToggleSwitchCalculator();
            }

            if (!ModeMenu)
            {
                if (CurrentMode == 1)
                {
                    EnableOperations();
                    txtOutput.Enabled = true;
                    txtOutput.Visible = true;
                }
                else if (CurrentMode == 2)
                {
                    Programmer();
                }
                else if (CurrentMode == 3)
                {
                    GraphingMenu = true;
                    lblText.Text = "1) Get line gradient\n2) Graph out lines\n3) Solve quadratic";
                    ModeMenu = false;
                }
            }
            
        }



        // CODE FOR PROGRAMMER MODE /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void txtDenary_TextChanged(object sender, EventArgs e)
        {
            String DenaryString = txtDenary.Text;
            try
            {
                Denary = Int32.Parse(txtDenary.Text);
                if ((Denary > 255 || Denary < 0) && Denary.ToString().Length > 0)
                {
                    if (DenaryString.Length != 0 && Binary.Length != 0 && Hex.Length != 0)
                    {
                        MessageBox.Show("Please enter a correct denary value up to 8 bit (255)", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
                }
                else if (Denary >= 0 && Denary.ToString().Length > 0)
                {
                    // Convert into Hex
                    Hex = Denary.ToString("X");
                    // Put into Hex text box
                    txtHex.Text = Hex;
                    // Convert into binary
                    Binary = Convert.ToString(Denary, 2);
                    // Put into Binary text box
                    txtBinary.Text = Binary;
                }
                else if (DenaryString.Length == 0 || Binary.Length == 0 || Hex.Length == 0)
                {
                    txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
                }
                else
                {
                    MessageBox.Show("Too long!'", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                Console.WriteLine(Denary + "    " + Denary.ToString().Length);
                if (DenaryString.Length != 0 && Binary.Length != 0 && Hex.Length != 0)
                {
                    MessageBox.Show("Please enter a correct denary value up to 8 bit (255)", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Binary = (txtBinary.Text).ToString();

            // If the previous length is greater than the current length that means they have deleted text
            if (Binary.Length > 0)
            {
                char LastCharacter = Binary[(Binary.Length - 1)];
                if (LastCharacter == '1' || LastCharacter == '0')
                {
                    // As long as it is not above 8 bit, convert
                    if (Binary.Length <= 8)
                    {
                        // Convert it to an integer with base 2
                        Denary = Convert.ToInt32(Binary, 2);
                        // Put this into the denary text box
                        txtDenary.Text = Denary.ToString();
                        // Convert into Hex
                        Hex = Denary.ToString("X");
                        // Put into Hex text box
                        txtHex.Text = Hex;
                    }
                    else
                    {
                        MessageBox.Show("Too long! Binary values should be up to 8 digits long'", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBinary.Text = Binary.Remove(Binary.Length - 1, 1);
                    }
                }
                else
                {
                    if (Denary.ToString().Length != 0 && Binary.Length != 0 && Hex.Length != 0)
                    {
                        MessageBox.Show("Please enter a correct 8 bit binary value such as '01100101 including the starting zero(s)'", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
                    LastCharacter = ' ';
                }
            }
            else
            {
                txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Hex = txtHex.Text.ToString();
            if (Hex.Length <= 2)
            {
                try
                {
                    // Convert it to an integer with base 2
                    Denary = Convert.ToInt32(Hex, 16);
                    // Put this into the denary text box
                    txtDenary.Text = Denary.ToString();
                    // Convert into binary
                    Binary = Convert.ToString(Denary, 2);
                    // Put into Binary text box
                    txtBinary.Text = Binary;
                }
                catch
                {
                    if (Denary.ToString().Length != 0 && Binary.Length != 0 && Hex.Length != 0)
                    {
                        MessageBox.Show("Please enter a correct Hex number no bigger than 'FF', or 255 in denary", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
                }
            }
            else if (Denary.ToString().Length == 0 || Binary.Length == 0 || Hex.Length == 0)
            {
                txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a correct Hex number no bigger than 'FF', or 255 in denary", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDenary.Text = ""; txtBinary.Text = ""; txtHex.Text = "";
            }
        }

        void Programmer()
        {
            // Get rid of menu screen
            ModeMenu = !ModeMenu;
            ToggleSwitchCalculator();
            lblText.Visible = false;
            ModeMenu = false;

            // Make Text inputs visible and put borders on them
            txtHex.Enabled = true; txtHex.Visible = true; txtHex.BorderStyle = BorderStyle.FixedSingle;
            txtBinary.Enabled = true; txtBinary.Visible = true; txtBinary.BorderStyle = BorderStyle.FixedSingle;
            txtDenary.Enabled = true; txtDenary.Visible = true; txtDenary.BorderStyle = BorderStyle.FixedSingle;

            // Bring the labels with text "Hex, Denary and Binary to the front"
            lblHex.Visible = true; lblHex.BringToFront();
            lblDenary.Visible = true; lblDenary.BringToFront();
            lblBinary.Visible = true; lblBinary.BringToFront();
        }



        // CODE FOR GRAPHING MODE ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void LineGradient()
        {

        }

        void Lines()
        {
            LineMode = true;
            ModeMenu = !ModeMenu;
            ToggleSwitchCalculator();
            ModeMenu = false;
            lblText.Enabled = true;
            lblText.Visible = true;
            lblText.Text = "Please enter the equation of\na line in the form y = ax + b";
            txtA.Enabled = true; lblA.Visible = true; 
            txtB.Enabled = true; lblB.Visible = true; 
            //txtC.Enabled = true; lblC.Visible = true; 
        }
        void InitiliseBox()
        {
            DisableUnwantedGUI();
            txtOutput.Visible = false;
            txtBinary.Visible = false;

            txtA.Visible = false; lblA.Visible = false;
            txtB.Visible = false; lblB.Visible = false;
            //txtC.Visible = false; lblC.Visible = false; 
            pictureBox1.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            lblText.Enabled = false;
            lblText.Visible = false;
            //lblText.Text = "Click to initialise";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (LineMode)
            {
                //lblText.Visible = false;
                pictureBox1.BringToFront();
                // Create graphics variable for the picture box
                Graphics graphics;
                graphics = pictureBox1.CreateGraphics();
                Pen penColour = new Pen(Color.Black);
                // Draw graph axis
                graphics.DrawLine(penColour, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
                graphics.DrawLine(penColour, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);

                // Find the points as if it were on a normal graph
                float X1 = 0; // so the line will take up half the graph, from the middle
                float Y1 = b;
                float X2 = (pictureBox1.Width) / 4;
                float Y2 = (a * X2) + b;

                Console.WriteLine(X1 + " " + Y1 + " " + X2 + " " + Y2);

                // graphing the actual points, as (0,0) is actually top left in the picture box and the middle needs to be altered from 0 to the graphing point
                float GraphX1 = pictureBox1.Width / 2;
                float GraphY1 = (pictureBox1.Height / 2) - b;
                float GraphX2 = X2 + ((pictureBox1.Width) / 2); // goes 3/4 down the picture box
                float GraphY2 = -(Y2 + (pictureBox1.Height / 2));

                graphics.DrawLine(penColour, GraphX1, GraphY1, GraphX2, GraphY2);
                LineMode = false;
            }
            else
            {
                // Code for grabbing the point
            }
        }

        private void lblB_Click(object sender, EventArgs e)
        {

        }

        private void lblA_Click(object sender, EventArgs e)
        {

        }

        void Quadratic()
        {
            // simple enter a,b,c use equation
            lblText.Text = "aX² + bX + c\nPlease enter a, b and c";
            lblRoots.Visible = true;
            lblA.Visible = true; txtA.Enabled = true;
            lblB.Visible = true; txtB.Enabled = true;
            lblC.Visible = true; txtC.Enabled = true;
            txtA.BorderStyle = BorderStyle.FixedSingle;
            txtB.BorderStyle = BorderStyle.FixedSingle;
            txtC.BorderStyle = BorderStyle.FixedSingle;
            lblRoots.Text = "Roots:";
        }

        void SolveQuadratic()
        {
            if(txtA.Text.Length != 0 && txtB.Text.Length != 0 && txtC.Text.Length != 0)
            {
                Double Discriminant = ((Math.Pow(b, 2)) - (4*a*c));
                lblRoots.Text = "Roots: " + (Math.Round((((-b) - (Math.Sqrt(Discriminant)))/(2*a)) * 1000) / 1000) + "   " + (Math.Round((((-b) + (Math.Sqrt(Discriminant))) / (2 * a)) * 1000) / 1000);
            }
        }

        private void txtA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                a = Int32.Parse(txtA.Text);
                SolveQuadratic();
            }
            catch
            {
                if ((txtA.Text).Length != 0)
                {
                    MessageBox.Show("Please enter a correct integer value for 'a'", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (LineMode && (txtA.Text).Length != 0 && (txtB.Text).Length != 0)
            {
                InitiliseBox();
            }
        }

        private void txtB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                b = Int32.Parse(txtB.Text);
                SolveQuadratic();
            }
            catch
            {
                if ((txtB.Text).Length != 0)
                {
                    MessageBox.Show("Please enter a correct integer value for 'b'", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (LineMode && (txtA.Text).Length != 0 && (txtB.Text).Length != 0)
            {
                InitiliseBox();
            }
        }

        private void txtC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c = Int32.Parse(txtC.Text);
                SolveQuadratic();
            }
            catch
            {
                if ((txtC.Text).Length != 0)
                {
                    MessageBox.Show("Please enter a correct integer value for 'c'", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            //if (LineMode && (txtA.Text).Length != 0 && (txtB.Text).Length != 0 && (txtC.Text).Length != 0)
            //{
            //    InitiliseBox();
            //}
        }


        void PrintVariables()
        {
            Console.WriteLine(Num1Length);
            Console.WriteLine(Num2Length);
            Console.WriteLine(Number2);

            Console.WriteLine(Number1);
            Console.WriteLine(Number2);
            Console.WriteLine(Operation);
            Console.WriteLine(PreviousAns);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
        private void lblMode_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void lblShift_Click(object sender, EventArgs e) { }
        private void lblBinary_Click(object sender, EventArgs e) { }
        private void lblDenary_Click(object sender, EventArgs e) { }
        private void lblHex_Click(object sender, EventArgs e){ }
        private void btnMode_Click(object sender, EventArgs e) { }
    }
}
