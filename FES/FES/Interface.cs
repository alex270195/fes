using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FES
{
    public partial class Interface : Form
    {
        internal string sub; //формула вещества
        internal double k;//коэффициент полуширины
        internal string radType;//тип излучения

        public Interface()
        {
            InitializeComponent();
            sub = "";
            k = 0;
            radType = "";
            comboBoxRadiationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        void setSub(string _sub) { this.sub = _sub; }
        string getSub() { return this.sub; }

        void setK(string _k)
        {

            if (_k.Contains("."))
                MessageBox.Show("Ошибка при вводе коэффициента!");
            else
            {
                double x = Convert.ToDouble(_k);
                if (x != 0)
                    this.k = x;
                else
                {
                    MessageBox.Show("Ошибка при вводе коэффициента!");
                    Environment.Exit(0);
                }
            }
        }
        double getK() { return this.k; }

        void setRadType(string _radType)
        {
            if (_radType.Equals("Алюминий"))
                this.radType = "Al";
            else if (_radType.Equals("Магний"))
                this.radType = "Mg";
            else { MessageBox.Show("Ошибка в выборе типа излучения!"); }
        }
        string getRadType() { return this.radType; }

        /* void setDistType(string _distType){
             if (_distType.Equals("Гаусса"))
                 this.distType = 1;
             else if (_distType.Equals("Лоренца"))
                 this.distType = 2;
             else { MessageBox.Show("Ошибка в выборе типа распределения!"); }

         }*/
        //  byte getDistType() { return this.distType; }



        //получаем вещества из формулы
        private string[] getSubNames(string _form)
        {
            char[] arrr = _form.ToCharArray();


            if (Char.IsUpper(arrr[0]))
            {
                int i = 0;
                string[] arraySub1 = new string[arrr.Length];
                while (i < arrr.Length)
                {
                    if (Char.IsLetter(arrr[i]))
                    {
                        if (Char.IsUpper(arrr[i]))
                        {
                            arraySub1[i] = arrr[i].ToString();
                            i++;
                        }
                        else
                            if (Char.IsLower(arrr[i]) && i > 0)
                        {
                            arraySub1[i - 1] = arraySub1[i - 1] + arrr[i].ToString();
                            i++;
                        }

                    }
                    else i++;
                }

                string suba = "";
                for (i = 0; i < arraySub1.Length; i++)
                    suba = suba + arraySub1[i] + ".";

                string[] arraySub = suba.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                return arraySub;
            }
            else
            {

                string[] arraySub = new string[1];
                arraySub[0] = "exit";
                MessageBox.Show("Неверно введена формула вещества!");


                /*ЗАКРЫВАЕМ ПРОГРАММУ */

                //  Application.Exit();

                return arraySub;
                //Interface.ActiveForm.Show();
            }

        }

        private bool isLatinLet(char s)
        {
            switch (s)
            {
                case 'A':
                    return true;
                case 'B':
                    return true;
                case 'C':
                    return true;
                case 'D':
                    return true;
                case 'E':
                    return true;
                case 'F':
                    return true;
                case 'G':
                    return true;
                case 'H':
                    return true;
                case 'I':
                    return true;
                case 'J':
                    return true;
                case 'K':
                    return true;
                case 'L':
                    return true;
                case 'M':
                    return true;
                case 'N':
                    return true;
                case 'O':
                    return true;
                case 'P':
                    return true;
                case 'Q':
                    return true;
                case 'R':
                    return true;
                case 'S':
                    return true;
                case 'T':
                    return true;
                case 'U':
                    return true;
                case 'V':
                    return true;
                case 'W':
                    return true;
                case 'X':
                    return true;
                case 'Y':
                    return true;
                case 'Z':
                    return true;
            }
            return false;
        }

        private bool checkRdTp(string c)
        {
            bool fl = true;
            bool let;
            char[] arrC = c.ToCharArray();
            if (!Char.IsNumber(arrC[0]))
                fl = false;
            else
            {
                if (arrC.Length > 1)
                {
                    for (int i = 1; i < arrC.Length; i++)
                    {
                        let = Char.IsNumber(arrC[i]);
                        if (let != true)
                        {
                            if (arrC[i] != ',')
                            {
                                fl = false;
                                return fl;
                            }
                            else
                            {
                                fl = true;
                                return fl;
                            }
                        }
                        else
                        {
                            fl = true;
                            return fl;
                        }
                    }
                }
                else fl = true;
            }
            return fl;

        }

        private void buttonCheckCrossSection_Click(object sender, EventArgs e)
        {
            tabCrs tcs = new tabCrs();
            tcs.Show();
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonCheckCrossSection, "Просмотреть все сечения фотоионизации");
            t.SetToolTip(buttonCompute, "Рассчитать интенсивность и построить график, используя введенные данные");
            t.SetToolTip(buttonExit, "Выйти из программы");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void buttonCompute_Click(object sender, EventArgs e)
        {


            /* string Formula;
             if (textBoxFormula.Text != String.Empty)
                 Formula = textBoxFormula.Text;
             else
             {
                 MessageBox.Show("Введите формулу вещества!");
                 Formula = Console.ReadLine();
             }*/


            string radiationType = "";

            if (comboBoxRadiationType.Text == String.Empty)
            {
                MessageBox.Show("Ошибка! Не выбран тип излучения. Работы программы будет завершена.");
                Process.GetCurrentProcess().Kill();
                System.Environment.FailFast("Палундра!");
            }
            else
            {
                if (comboBoxRadiationType.Text == "Алюминий")
                    radiationType = "Al";
                else
                    radiationType = "Mg";

            }

            string coeff;

            if (textBoxCoeff.Text != String.Empty)
            {
                coeff = textBoxCoeff.Text;
                if (checkRdTp(coeff) != true)
                {
                    MessageBox.Show("Ошибка! Неверно указан коэффициент. Работы программы будет завершена.");
                    Process.GetCurrentProcess().Kill();
                    //Environment.Exit(1);
                }

            }
            else
            {
                MessageBox.Show("Ошибка! Не указан коэффициент. Работы программы будет завершена.");
                Process.GetCurrentProcess().Kill();
                //  Environment.Exit(2);
                coeff = "";
            }
            double coef = Convert.ToDouble(coeff);

            if (coef > 100)
            {
                MessageBox.Show("Ошибка! Неверно указан коэффициент. Работы программы будет завершена.");
                Process.GetCurrentProcess().Kill();
            }
            else if (coef < 0.0001)
            {
                MessageBox.Show("Ошибка! Неверно указан коэффициент. Работы программы будет завершена.");
                Process.GetCurrentProcess().Kill();
            }

            else { 


            // string distributionType = comboBoxDistribType.Text;
            //  MessageBox.Show(Formula + " /*/ " + coeff + " /*/ " + radiationType + " /*/ " + distributionType);

            //    this.setSub(Formula);
            // this.setK(coeff);
            //   this.setDistType(distributionType);
            //    this.setRadType(radiationType);



            /*    string[] lmnttts = this.getSubNames(Formula);
                if (lmnttts.Contains("exit"))
                {
                    Application.Exit();
                }
                else*/
            // {
            Substance sub = new Substance();
            sub.calc(coef, radiationType);
            }
            //   string[] t = sub.getElem(Formula);

            //  }

        }


    }
}
