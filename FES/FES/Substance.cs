using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace FES
{
    struct aOne
    {
        public int orbNum;  //номер орбитали
        public double orbEn;//орбитальная энергия

        public int numLine;  //номер функции
        public string elName;//хим элемент

        public string lvlNum; //номер элемента
        public string crName; //координата (для определения типа орбитали s, p, d, e)
        public double value; //значение нормированного вклада волновой функции во вклад уровня
    }
    class Substance
    {
        SqlConnection sqlConnect;


        public double[] getArr(double[][] _arr, int x)
        {
            int i = 0;
            double[] arr = new double[x];

            for (i = 0; i < x; i++)
                arr[i] = _arr[1][i];

            return arr;
        }

        private bool isDig(string _sym)
        {

            switch (_sym)
            {

                case "1":
                    return true;
                case "2":
                    return true;
                case "3":
                    return true;
                case "4":
                    return true;
                case "5":
                    return true;
                case "6":
                    return true;
                case "7":
                    return true;
                case "8":
                    return true;
                case "9":
                    return true;
                default:
                    return false;


            }
            //            return false;
        }

        public static bool isCorSym(string str)
        {
            //char[] arr=new char[3];
            // arr = str.ToCharArray();
            bool vr = false;
            if (str.Contains("X") || str.Contains("Y") || str.Contains("Z"))
                vr = true;
            return vr;
        }

        private bool isBkt(string _sym)
        {
            if (_sym == ")" || _sym == "(")
                return true;
            else return false;
        }

        //процедура выделения элементов из веществ
        public string[] getElem(string _formula)
        {

            string[] form;

            form = _formula.Split(' ');
            int i, j;
            for (i = 0; i < form.Length; i++)
            {
                if (isDig(form[i]))
                    form[i] = "";
                if (isDig(form[i]))
                    form[i] = "";
            }

            j = 0;
            i = 0;
            string[] _form = new string[form.Length];
            while ((i < form.Length) && (j < form.Length))
            {
                if (form[i] != null)
                {
                    _form[j] = form[i];
                    i++;
                }
                else
                    i++;
                j++;
            }


            return _form;
        }

        private string getType(string cor)
        {
            string _str = "";

            switch (cor)
            {
                case "S":
                    return "S";
                case "X":
                    return "P";
                case "Y":
                    return "P";
                case "Z":
                    return "P";
                case "XX":
                    return "D";
                case "YY":
                    return "D";
                case "ZZ":
                    return "D";
                case "XY":
                    return "D";
                case "XZ":
                    return "D";
                case "YX":
                    return "D";
                case "YZ":
                    return "D";
                case "ZX":
                    return "D";
                case "ZY":
                    return "D";
                case "XXX":
                    return "F";
                case "YYY":
                    return "F";
                case "ZZZ":
                    return "F";
                case "XXY":
                    return "F";
                case "XXZ":
                    return "F";
                case "YYX":
                    return "F";
                case "YYZ":
                    return "F";
                case "ZZX":
                    return "F";
                case "ZZY":
                    return "F";
                case "XYZ":
                    return "F";

            }

            return _str;
        }





        public void calc(double _coef, string _radType)
        {

            double cf = _coef *2.36/* Math.Sqrt(2 * Math.PI)*/;
            string firstLine, curLine;

            Stream myStream = null;
            StreamReader reader;

            int shellsCount = 0, basFuncCount = 0, electronsCount = 0, countLines;
            countLines = 0;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\Calc";
            openFileDialog1.Filter = "out files (*.out)|*.out";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            string subTitle = "";
            int subTitleLine = 0;

           // string pathOfAl = "crsAl2.txt";
            //string pathOfMg = "crsMg2.txt";


            //crssSec crsss = new crssSec();
          //  crsss.getCrsSec(pathOfAl);

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            reader = new StreamReader(myStream);

                            string file = openFileDialog1.FileName.ToString();

                            if (reader.EndOfStream)
                            {
                                MessageBox.Show("Ошибка! Файл пуст.");
                                Process.GetCurrentProcess().Kill();
                            }

                            //считываем функции из файлов, используя ключевые слова

                            while (!reader.EndOfStream)
                            {
                                firstLine = reader.ReadLine();

                                if (firstLine.Contains("RUN TITLE"))
                                {
                                    subTitleLine = countLines+2;
                                   // System.Windows.Forms.MessageBox.Show("RUN TITLE " + subTitleLine.ToString());
                                }

                                if (firstLine.Contains("TOTAL NUMBER OF SHELLS"))
                                {
                                    curLine = firstLine.Substring(firstLine.IndexOf("=") + 1);
                                    shellsCount = Convert.ToInt16(curLine);
                                    // System.Windows.Forms.MessageBox.Show("TOTAL NUMBER OF SHELLS " + shellsCount.ToString());
                                }

                                if (firstLine.Contains("TOTAL NUMBER OF BASIS FUNCTIONS") || firstLine.Contains("NUMBER OF CARTESIAN GAUSSIAN BASIS FUNCTIONS"))
                                {
                                    curLine = firstLine.Substring(firstLine.IndexOf("=") + 1);
                                    basFuncCount = Convert.ToInt16(curLine);
                                    //System.Windows.Forms.MessageBox.Show("TOTAL NUMBER OF BASIS FUNCTIONS " + basFuncCount.ToString());
                                }

                                if (firstLine.Contains("NUMBER OF ELECTRONS"))
                                {
                                    curLine = firstLine.Substring(firstLine.IndexOf("=") + 1);
                                    electronsCount = Convert.ToInt16(curLine);
                                    //  System.Windows.Forms.MessageBox.Show("NUMBER OF ELECTRONS " + electronsCount.ToString());
                                }

                                if (firstLine.Contains("EIGENVECTORS"))
                                {
                                    //  System.Windows.Forms.MessageBox.Show("EIGENVECTORS " + countLines.ToString());
                                    break;
                                }
                                countLines++;
                            }

                            string[] arStr = File.ReadAllLines(file);
                            subTitle = arStr[subTitleLine];
                           // string sbttl = "";
                           // int sbtlSpace = subTitle.IndexOf(" ");
                           // sbttl = subTitle.Substring(0, sbtlSpace);
                          //  System.Windows.Forms.MessageBox.Show("RUN TITLE " + subTitle);

                            List<string> arrFunc = new List<string>(); //попробуем туда закинуть наши функции 


                            int i = 0; //счетчик по столбцам
                            int x = 0, y = 0;
                            x = electronsCount % 5;
                            //делим на 5, т.к. в файлах по 5 столбцов
                            if (x != 0)
                                y = electronsCount / 5 + 1;
                            else
                                y = electronsCount / 5;

                            int toEndOfFunc = 0;
                            toEndOfFunc = y * (basFuncCount + 4);

                            int toEndOfElectrons = y * 5;

                            for (i = 0; i < toEndOfFunc; i++)
                                arrFunc.Add(arStr[countLines + 3 + i]);

                            string[] lineT;

                            int t = 0;
                            string lineR = "";
                            while (t < toEndOfFunc)
                            {
                                lineT = arrFunc[t].Split(' ');
                                /* каждый элемент списка функций 
                                 * - строку - 
                                 * парсим по пробелам
                                 *  и получаем массив с элементами строки*/

                                string lineTT = "";
                                int k = 0;
                                bool flag = false;
                                while (k < lineT.Length) //формируем одну огромную строку с элементами через запятую
                                {
                                    if (lineT[k] != "")
                                    {
                                        if (lineT[k].Length == 3)
                                        {
                                            flag = isCorSym(lineT[k]);
                                            if (flag == true) //здесь проверяем на ХХХ, YYY и т.п.
                                                lineTT = lineTT + "_" + "," + lineT[k] + ",";
                                            else lineTT = lineTT + lineT[k] + ",";
                                        }
                                        else
                                            lineTT = lineTT + lineT[k] + ",";
                                    }

                                    k++;
                                }

                                lineR = lineR + lineTT;
                                t++;
                            }
                            string[] st;
                            st = lineR.Split(','); //парсим строку на массив с элементами

                            for (i = 0; i < st.Length; i++) //заменяем в числах все точки на запятые для корректной конвертации
                            {
                                if (st[i].Contains("."))
                                    st[i] = st[i].Replace(".", ",");
                            }




                            string[] elm = new string[basFuncCount]; //для элементов
                            string[] coord = new string[basFuncCount]; //для координат
                            double[] orbEnergies = new double[toEndOfElectrons]; //для орбитальных энергий
                            string[] lvvvlNum = new string[basFuncCount];

                            double[][] myArray = new double[toEndOfElectrons][]; //создаем массив массивов в котором toEndOfElectrons столбцов
                            for (i = 0; i < toEndOfElectrons; i++)
                            {
                                myArray[i] = new double[basFuncCount]; // и basFuncCount строк
                            }

                            t = 0;


                            //разрезаем по столбцам

                            int g = 0, l = 0;

                            i = 19;
                            int elCounter = 16, coordCounter = 18, orbEnCounter = 5, lvvvlNumCounter = 17;
                            while (g < toEndOfElectrons)
                            {
                                l = 0;

                                while (l < basFuncCount)
                                {
                                    myArray[g][l] = Convert.ToDouble(st[i]);
                                    myArray[g + 1][l] = Convert.ToDouble(st[i + 1]);
                                    myArray[g + 2][l] = Convert.ToDouble(st[i + 2]);
                                    myArray[g + 3][l] = Convert.ToDouble(st[i + 3]);
                                    myArray[g + 4][l] = Convert.ToDouble(st[i + 4]);

                                    elm[l] = st[elCounter];
                                    coord[l] = st[coordCounter];
                                    lvvvlNum[l] = st[lvvvlNumCounter];

                                    elCounter = elCounter + 9;
                                    coordCounter = coordCounter + 9;
                                    lvvvlNumCounter = lvvvlNumCounter + 9;

                                    i = i + 9;

                                    l++;
                                }

                                //заполняем массив орбитальных энергий
                                orbEnergies[g] = Convert.ToDouble(st[orbEnCounter]);
                                orbEnergies[g + 1] = Convert.ToDouble(st[orbEnCounter + 1]);
                                orbEnergies[g + 2] = Convert.ToDouble(st[orbEnCounter + 2]);
                                orbEnergies[g + 3] = Convert.ToDouble(st[orbEnCounter + 3]);
                                orbEnergies[g + 4] = Convert.ToDouble(st[orbEnCounter + 4]);
                                orbEnCounter = orbEnCounter + basFuncCount * 9;

                                g = g + 5;
                                i = i + 15;
                                orbEnCounter = orbEnCounter + 15;
                                elCounter = elCounter + 15;
                                coordCounter = coordCounter + 15;
                                lvvvlNumCounter = lvvvlNumCounter + 15;
                            }

                            //создадим список структур

                            //здесь создаем список списков (для файла NiC3H5 - будет 75 списков, в каждом из которых по 103 элемента)
                            var spis = new List<List<aOne>>(toEndOfElectrons);


                            for (g = 0; g < toEndOfElectrons; g++)
                            {
                                spis.Add(new List<aOne>(basFuncCount));
                                for (i = 0; i < basFuncCount; i++)
                                    spis[g].Add(new aOne() { orbNum = g, orbEn = orbEnergies[g] * 27.21, elName = elm[i], crName = coord[i], lvlNum = lvvvlNum[i], numLine = i, value = (Math.Pow(myArray[g][i], 2) / Math.Pow(myArray[g].Sum(), 2)) }); //пихаем сразу нормированные значения вклада уровня и умножаем орбитальные энергии на 27,21

                            }




                            spectr sp = new spectr();

                            // double sumOne = myArray[1].Sum();




                            double[] a = new double[toEndOfElectrons];


                            //запросы к базе данных

                            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\YandexDisk\ДИПЛОМ\FES\FES\DatabaseCrs.mdf;Integrated Security=True";

                            sqlConnect = new SqlConnection(connectionString);
                            double[][] inten = new double[toEndOfElectrons][];
                            for (i = 0; i < toEndOfElectrons; i++)
                            {
                                inten[i] = new double[basFuncCount]; // и basFuncCount строк
                            }



                            try
                            {
                                sqlConnect.Open();
                                using (SqlCommand sql = sqlConnect.CreateCommand())
                                {
                                    //double[] vbl = new double[basFuncCount]; 
                                  /*  for (g = 0; g < toEndOfElectrons; g++)
                                    {
                                        for (i = 0; i < basFuncCount; i++)
                                        {
                                            SqlCommand com = new SqlCommand("SELECT [Сечение фотоионизации] FROM Table2 WHERE [Элемент] =" + "'" + spis[g].ElementAt(i).elName + "'" + " AND " + "[Уровень]=" + "'" + getType(spis[g].ElementAt(i).crName) + "'" + " AND " + "[Тип]=" + "'" + _radType + "'", sqlConnect);
                                            SqlDataReader read = com.ExecuteReader();

                                            if (read.HasRows)
                                            {
                                                // MessageBox.Show("There are data");
                                                while (read.Read())
                                                {
                                                    inten[g][i] = Convert.ToDouble(read.GetValue(0))/*получаем сечения и сразу умножаем на вклад и коэффициент* spis[g].ElementAt(i).value * cf;
                                                }
                                            }
                                            else
                                                MessageBox.Show("There are no data 1 " + i);
                                            read.Close();
                                        }
                                    }

                                    string ln="";*/

                                    for (g = 0; g < toEndOfElectrons; g++)
                                    {
                                        for (i = 0; i < basFuncCount; i++)
                                        {
                                            string quer = "SELECT [Сечение] FROM [Table] WHERE [Энергия] <" + spis[g].ElementAt(0).orbEn.ToString().Replace(',', '.') + "+10 AND [Энергия]>" + spis[g].ElementAt(0).orbEn.ToString().Replace(',', '.') + "-10 AND [Элемент]="+ "'" + spis[g].ElementAt(i).elName + "'" + " AND [Тип]=" + "'" + getType(spis[g].ElementAt(i).crName) + "' AND [Излучение]=" + "'" + _radType + "'";
                                            SqlCommand com1 = new SqlCommand(quer, sqlConnect);
                                            SqlDataReader read1 = com1.ExecuteReader();
                                            if (read1.HasRows)
                                            {
                                                // MessageBox.Show("There are data");
                                                while (read1.Read())
                                                {
                                                    object crsval = read1.GetValue(0);
                                                   // object elname = read1.GetValue(1);
                                                  //  object orbname = read1.GetValue(2);
                                                    //  ln = ln + elname.ToString() + " " + orbname.ToString() + " " + crsval.ToString() + "/ " + g + "/ " + i + "/  ***   ";
                                                    inten[g][i] = Convert.ToDouble(crsval)/*получаем сечения и сразу умножаем на вклад и коэффициент*/* spis[g].ElementAt(i).value * cf;

                                                }
                                            }
                                            else
                                            {
                                               // Console.Out.WriteLine("There are no data  " + g + " " + i);
                                                inten[g][i] = 0;
                                            }
                                            
                                            read1.Close();
                                        }
                                    }
                                    //Console.Out.WriteLine(ln);

                                }
                                sqlConnect.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка подключения к базе данных с сечениями!2     " + ex);
                            }

                            double[] intn = new double[toEndOfElectrons];
                            for (i = 0; i < toEndOfElectrons; i++)
                            {

                                intn[i] = inten[i].Sum();
                                

                            }

                            for (i = 0; i < toEndOfElectrons; i++)
                            {
                                if (Double.IsNaN(intn[i]) || Double.IsInfinity(intn[i]))
                                    intn[i] = intn.Average();
                            }

                            bool f = true; int StartInten = 0;
                            while (f == true)
                            {
                                if (intn[StartInten] != 0)
                                    f = false;
                                StartInten++;
                            }

                            g = StartInten; i = 0;

                            Axis ax = new Axis();
                            ax.Title = "Номер орбитали";
                           
                            sp.spector.ChartAreas[0].AxisX = ax;
                            Axis ay = new Axis();
                            ay.Title = "Интенсивность";
                            sp.spector.ChartAreas[0].AxisY = ay;
                            sp.spector.Series["graph"].LegendText = subTitle;
                            //применяем нормальное распределение и добавляем на график


                            while (g < toEndOfElectrons)
                            {
                                a[g] = sp.spector.DataManipulator.Statistics.NormalDistribution(intn[g]);
                                sp.spector.Series["graph"].Points.AddY(a[g]);
                                g++;
                            }

                            


                            string fname = Path.GetFileNameWithoutExtension(openFileDialog1.FileName.ToString()) + " " + _radType.ToString() + " coef " + _coef.ToString();
                            string fLocation = Path.GetDirectoryName(openFileDialog1.FileName.ToString())+"\\";
                            sp.Show();
                            sp.spector.SaveImage(fLocation + fname + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла: " + ex.Message);
                }
            }




            /*  FileStream file1 = new FileStream("Ni_C3H5.out", FileMode.Open); //создаем файловый поток
                  //   FileStream file1 = new FileStream("Si6O11H8_Mg(acac).out", FileMode.Open);
                     StreamReader reader = new StreamReader(file1); // создаем «потоковый читатель» и связываем его с файловым потоком 

                     int shellsCount=0, basFuncCount=0, electronsCount=0, countLines;
                     countLines = 0;
                     //ДОБАВИТЬ ИСКЛЮЧЕНИЕ ФАЙЛ НЕ СУЩЕСТВУЕТ


                     //Console.WriteLine(reader.ReadLine()); //считываем все данные с потока и выводим на экран


                     //считываем функции из файлов, используя ключевые слова
                     while (!reader.EndOfStream)
                     {
                         firstLine = reader.ReadLine();

                         if (firstLine.Contains("TOTAL NUMBER OF SHELLS"))
                         {
                             curLine = firstLine.Substring(firstLine.IndexOf("=") + 1);
                             shellsCount = Convert.ToInt16(curLine);
                             System.Windows.Forms.MessageBox.Show(shellsCount.ToString());
                         }

                         if (firstLine.Contains("TOTAL NUMBER OF BASIS FUNCTIONS"))
                         {
                             curLine = firstLine.Substring(firstLine.IndexOf("=") + 1);
                             basFuncCount = Convert.ToInt16(curLine);
                             System.Windows.Forms.MessageBox.Show(basFuncCount.ToString());
                         }

                         if (firstLine.Contains("NUMBER OF ELECTRONS"))
                         {
                             curLine = firstLine.Substring(firstLine.IndexOf("=") + 1);
                             electronsCount = Convert.ToInt16(curLine);
                             //System.Windows.Forms.MessageBox.Show(electronsCount.ToString());
                         }

                         if (firstLine.Contains("EIGENVECTORS"))
                         {
                             System.Windows.Forms.MessageBox.Show(countLines.ToString());
                             break;
                         }
                         countLines++;
                     }



                     reader.Close(); //закрываем поток
                     string[] arStr = File.ReadAllLines("Ni_C3H5.out");

                     List<string> arrFunc = new List<string>(); //попробуем туда закинуть наши функции начиная с 711 строки до 2314


                     int i = 0; //счетчик по столбцам
                     int x=0, y=0;
                     x = electronsCount % 5;
                     //делим на 5, т.к. в файлах по 5 столбцов
                     if (x != 0)
                         y = electronsCount / 5 + 1;  
                     else
                         y = electronsCount / 5;

                     int toEndOfFunc=0;
                     toEndOfFunc = y * (basFuncCount + 4);

                     for (i = 0; i < toEndOfFunc; i++)
                         arrFunc.Add(arStr[countLines + 3 + i]);

             */

            //System.Windows.Forms.MessageBox.Show(firstLine);

        }
    }
}
