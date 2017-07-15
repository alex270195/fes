using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FES
{
    struct crs
    {
        public string elName;
        public string orb;
        public double crsValue;
        public string radType;
    }

    
    class crssSec
    {
        
        public List<crs> getCrsSec(string _path)
        {
            List<crs> crsList = new List<crs>();
            string[] arStr = File.ReadAllLines(_path);
            

            int i = 0;
            string line="";
            for (i = 0; i < arStr.Length; i++)
                line = line + arStr[i] + " ";
            string[] st1 = line.Split(' ');
            string st="";
            i = 0;
            while (i < st1.Length)
            {
                if (st1[i] != "")
                {
                    st = st + st1[i] + ",";
                    i++;
                }
                else
                {
                    st = st + "";
                    i++;
                }                
            }
            double _ccrs = 0;
            string[] arr = st.Split(',');
            for (i = 0; i < arr.Length-1; i=i+4)
            {
                if (arr[i].Contains("."))
                {
                    arr[i] = arr[i].Replace('.', ',');
                    _ccrs = Convert.ToDouble(arr[i]);
                }
                crsList.Add(new crs() {elName = arr[i], orb=arr[i+1], crsValue=_ccrs, radType=arr[i+3] });
               //crsList = crsList.GroupBy(n => n.elName);
            }

            var groupElm = from lmnt in crsList
                           group lmnt by lmnt.elName;

//            crsList.GroupBy(n => n.elName);

            return crsList;
        }
    }
}
