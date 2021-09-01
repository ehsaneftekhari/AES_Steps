using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyAES
{
    public class Matric
    {
        public byte[,] matric { get; set; }

        public Matric()
        {
            this.matric = new byte[4, 4];
        }

        public Matric(int x, int y)
        {
            this.matric = new byte[x, y];
        }
        public Matric(byte[] data)
        {
            fillMatric(data);
        }
        public Matric(byte[,] matric)
        {
            this.matric = matric;
        }

        public Matric(Matric matric) : this(matric.matric.GetLength(0), matric.matric.GetLength(1))
        {
            byte[,] temp = matric.matric;
            int i = temp.GetLength(0) - 1;
            for (; i >= 0; i--)
            {
                int j = temp.GetLength(1) - 1;
                for (; j >= 0; j--)
                {
                    this.matric[i, j] = temp[i, j];
                }
            }
        }

        public Matric(string data)
        {
            if (data.Length > 16)
            {
                throw new Exception("string data length is more than 16");
            }
            byte[] tempdata = new byte[32];
            char[] chararay = data.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                tempdata[i] = (byte)chararay[i];
            }

            try
            {
                fillMatric(tempdata);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /****************************/

        public void fillMatric(byte[] data)
        {
            if (data.Length > 32)
            {
                throw new Exception("data length is more than 32");
            }
            matric = new byte[4, 4];
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matric[j, i] = data[index];
                    index++;
                }
            }
            int a = 10;
        }

        public string getPrintStringInDECMatric()
        {
            string printstring = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    printstring += matric[i, j].ToString() + "\t";
                }
                printstring += "\n";
            }
            return printstring;
        }

        public string getPrintStringInHEXMatric()
        {
            string printstring = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    printstring += MyMath.DectoHex(matric[i, j]) + "\t";
                }
                printstring += "\n";
            }
            return printstring;
        }

    }
}
