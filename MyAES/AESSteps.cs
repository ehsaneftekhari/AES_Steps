using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESSteps
{
    class AES
    {

        private static string[,] sboxtable =
        { 
          //    0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f 
        /*0*/{ "63", "7c", "77", "7b", "f2", "6b", "6f", "c5", "30", "01", "67", "2b", "fe", "d7", "ab", "76" },
        /*1*/{ "ca", "82", "c9", "7d", "fa", "59", "47", "f0", "ad", "d4", "a2", "af", "9c", "a4", "72", "c0" },
        /*2*/{ "b7", "fd", "93", "26", "36", "3f", "f7", "cc", "34", "a5", "e5", "f1", "71", "d8", "31", "15" },
        /*3*/{ "04", "c7", "23", "c3", "18", "96", "05", "9a", "07", "12", "80", "e2", "eb", "27", "b2", "75" },
        /*4*/{ "09", "83", "2c", "1a", "1b", "6e", "5a", "a0", "52", "3b", "d6", "b3", "29", "e3", "2f", "84" },
        /*5*/{ "53", "d1", "00", "ed", "20", "fc", "b1", "5b", "6a", "cb", "be", "39", "4a", "4c", "58", "cf" },
        /*6*/{ "d0", "ef", "aa", "fb", "43", "4d", "33", "85", "45", "f9", "02", "7f", "50", "3c", "9f", "a8" },
        /*7*/{ "51", "a3", "40", "8f", "92", "9d", "38", "f5", "bc", "b6", "da", "21", "10", "ff", "f3", "d2" },
        /*8*/{ "cd", "0c", "13", "ec", "5f", "97", "44", "17", "c4", "a7", "7e", "3d", "64", "5d", "19", "73" },
        /*9*/{ "60", "81", "4f", "dc", "22", "2a", "90", "88", "46", "ee", "b8", "14", "de", "5e", "0b", "db" },
        /*a*/{ "e0", "32", "3a", "0a", "49", "06", "24", "5c", "c2", "d3", "ac", "62", "91", "95", "e4", "79" },
        /*b*/{ "e7", "c8", "37", "6d", "8d", "d5", "4e", "a9", "6c", "56", "f4", "ea", "65", "7a", "ae", "08" },
        /*c*/{ "ba", "78", "25", "2e", "1c", "a6", "b4", "c6", "e8", "dd", "74", "1f", "4b", "bd", "8b", "8a" },
        /*d*/{ "70", "3e", "b5", "66", "48", "03", "f6", "0e", "61", "35", "57", "b9", "86", "c1", "1d", "9e" },
        /*e*/{ "e1", "f8", "98", "11", "69", "d9", "8e", "94", "9b", "1e", "87", "e9", "ce", "55", "28", "df" },
        /*f*/{ "8c", "a1", "89", "0d", "bf", "e6", "42", "68", "41", "99", "2d", "0f", "b0", "54", "bb", "16" },
        };

        private static byte[,] mixColumnsMatric =
        {
            {0x02, 0x03, 0x01, 0x01 },
            {0x01, 0x02, 0x03, 0x01 },
            {0x01, 0x01, 0x02, 0x03 },
            {0x03, 0x01, 0x01, 0x02 },
        };

        private static byte[,] RconTable =
        {
        //    1     2     3     4     5     6     7     8     9     10
            {0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36},
            {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},

        };

        public static Matric AESalgorithm(Matric input, Matric mainkey, int round, bool printlog)/*********************************************************************************************/
        {

            Matric temp = AddRoundKey(input, mainkey);
            if (printlog)
            {
                Console.WriteLine("/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\//\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\\n");
                Console.WriteLine("First AddRoundKeys Answer:");
                Console.WriteLine(temp.getPrintStringInHEXMatric());
                Console.WriteLine("/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\//\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\\n");
            }

            AESKey key = new AESKey(mainkey);
            for (int i = 1; i <= round; i++)
            {
                if (printlog)
                {
                    Console.WriteLine("/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ Rund " + i + " \\/\\/\\/\\/\\/\\/\\/\\/\\/\\//\\/\\/\\/\\/\\/\\/\\\n");
                }


                temp = SubBytes(temp);
                if (printlog)
                {
                    Console.WriteLine("SubBytes Answer:");
                    Console.WriteLine(temp.getPrintStringInHEXMatric());
                }

                temp = ShiftRows(temp);
                if (printlog)
                {
                    Console.WriteLine("ShiftRows Answer:");
                    Console.WriteLine(temp.getPrintStringInHEXMatric());
                }


                if (i < 10)
                {
                    if (printlog)
                    {
                        Console.WriteLine("**************************\nMixColumns:");
                    }
                    temp = MixColumns(temp, printlog);
                    if (printlog)
                    {
                        Console.WriteLine("MixColumns Answer:");
                        Console.WriteLine(temp.getPrintStringInHEXMatric());
                    }

                }

                
                if (printlog)
                {
                    Console.WriteLine("key Round "+i+" :");
                    Console.WriteLine(key.getKey(i).getPrintStringInHEXMatric());
                }

                temp = AddRoundKey(temp, key.getKey(i));
                if (printlog)
                {
                    Console.WriteLine("AddRoundKey Answer:");
                    Console.WriteLine(temp.getPrintStringInHEXMatric());
                }

                

                if (printlog)
                {
                    Console.WriteLine("/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\n");
                    Console.WriteLine("Round " + i + " Answer:");
                    Console.WriteLine(temp.getPrintStringInHEXMatric());
                }

            }
           
            
            return temp;
        }
        /********************************************************************************************************************************************************/

        public static Matric AddRoundKey(Matric input, Matric Key)
        {
            return MyMath.XOR(input, Key);
        }

        public static Matric SubBytes(Matric m)
        {
            Matric temp = new Matric();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp.matric[i, j] = SBox(m.matric[i, j]);
                }
            }
            return temp;
        }

        private static byte SBox(byte b)
        {
            string binHex = MyMath.DectoHex(b);
            if(binHex.Length == 1)
            {
                binHex = "0" + binHex;
            }
            int y = MyMath.HexToDec(binHex.Substring(1, 1));
            int x = MyMath.HexToDec(binHex.Substring(0, 1));
            string returner = sboxtable[x, y];

            return (byte)MyMath.HexToDec(returner);
        }

        public static Matric ShiftRows(Matric m)
        {
            Matric temp = new Matric(m);

            shiftRow(temp, 1, 1);
            shiftRow(temp, 2, 2);
            shiftRow(temp, 3, 3);

            return temp;
        }
        private static void shiftRow(Matric m, int rownumber, int count)
        {

            byte[] temparay = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                temparay[i] = m.matric[rownumber, i];
            }
            temparay = MyMath.cycleToLeft(temparay, count);
            for (int i = 0; i < 4; i++)
            {
                m.matric[rownumber, i] = temparay[i];
            }
        }
        public static Matric MixColumns(Matric m, bool printlog)
        {
            Matric ans = new Matric();
            for (int j = 0; j < 4; j++)//Column
            {
                for (int i = 0; i < 4; i++)//row
                {
                    if (printlog)
                    {
                        Console.WriteLine("------------");
                        Console.WriteLine("Calculating block [" + j + ", " + i + "]:");
                    }

                    byte blockAns = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        if (printlog)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Multiplication Block [" + j + ", " + k + "] * [" + k + ", " + i + "] :");
                        }
                        blockAns = (byte)(mixBlock(m.matric[k, j], mixColumnsMatric[i, k], printlog) ^ blockAns);


                    }
                    ans.matric[i, j] = blockAns;

                    if (printlog)
                    {
                        Console.WriteLine();
                        Console.WriteLine("block Answer in HEX = " + MyMath.DectoHex(blockAns));
                        Console.WriteLine("------------");
                    }
                }
            }
            if (printlog)
                Console.WriteLine();
            return ans;
        }

        public static byte mixBlock(byte a, byte b, bool printlog)
        {
            MyMath.BinaryPolynomial Polynomial_a = new MyMath.BinaryPolynomial(a);
            MyMath.BinaryPolynomial Polynomial_b = new MyMath.BinaryPolynomial(b);

            MyMath.BinaryPolynomial Polynomial_c = new MyMath.BinaryPolynomial(0x11B);

            MyMath.BinaryPolynomial MP_ans = MyMath.BinaryPolynomial.galvaMultiplication(Polynomial_a, Polynomial_b);
            if (printlog)
                Console.WriteLine(Polynomial_a.getPrintString() + " * " + Polynomial_b.getPrintString()
                    + " = " + MP_ans.getPrintString());

            MyMath.BinaryPolynomial ans = MP_ans;

            if (MP_ans.getBigestPower() > 7)
            {
                ans = MyMath.BinaryPolynomial.galvaMod(MP_ans, Polynomial_c);
                if (printlog)
                {
                    Console.WriteLine("###");
                    Console.WriteLine(MP_ans.getPrintString() + " % " + Polynomial_c.getPrintString()
                        + " = " + ans.getPrintString());
                    Console.WriteLine("###");
                }
            }

            if (printlog)
                Console.WriteLine("Answer In HEX = " + MyMath.DectoHex(ans.getByte()));

            return ans.getByte();
        }

        public static byte mixBlock(int a, int b, bool printlog)
        {
            MyMath.BinaryPolynomial Polynomial_a = new MyMath.BinaryPolynomial(a);
            MyMath.BinaryPolynomial Polynomial_b = new MyMath.BinaryPolynomial(b);

            MyMath.BinaryPolynomial Polynomial_c = new MyMath.BinaryPolynomial(0x11B);

            MyMath.BinaryPolynomial MP_ans = MyMath.BinaryPolynomial.galvaMultiplication(Polynomial_a, Polynomial_b);
            if (printlog)
                Console.WriteLine(Polynomial_a.getPrintString() + " * " + Polynomial_b.getPrintString()
                    + " = " + MP_ans.getPrintString());

            MyMath.BinaryPolynomial ans = MP_ans;

            if (MP_ans.getBigestPower() > 7)
            {
                ans = MyMath.BinaryPolynomial.galvaMod(MP_ans, Polynomial_c);
                if (printlog)
                {
                    Console.WriteLine("###");
                    Console.WriteLine(MP_ans.getPrintString() + " % " + Polynomial_c.getPrintString()
                        + " = " + ans.getPrintString());
                    Console.WriteLine("###");
                }
            }

            if (printlog)
                Console.WriteLine("Answer In HEX = " + MyMath.DectoHex(ans.getByte()));

            return ans.getByte();
        }

        public class AESKey
        {

            private List<Matric> keys;

            public AESKey(byte[] mainkey)
            {
                keys = new List<Matric>();
                keys.Add(new Matric(mainkey));
            }
            public AESKey(byte[,] mainkey)
            {
                keys = new List<Matric>();
                keys.Add(new Matric(mainkey));
            }
            public AESKey(Matric mainkey)
            {
                keys = new List<Matric>();
                keys.Add(new Matric(mainkey));
            }
            public AESKey(string mainkey)
            {
                keys = new List<Matric>();
                keys.Add(new Matric(mainkey));
            }

            private byte[] getColumn(int columnindex, int keynumber)
            {
                Matric tempkey = keys.ElementAt(keynumber);
                byte[] temp = new byte[4];

                for (int i = 0; i < 4; i++)
                {
                    temp[i] = tempkey.matric[i, columnindex];
                }

                return temp;
            }

            public Matric getKey(int round)
            {
                if(round>10 || round < 0)
                {
                    throw new Exception("round should be between -1 and 11");
                }
                if (round > keys.Count - 1)
                    createKey(round);
                return keys.ElementAt(round);
            }

            private void createKey(int roundnumber)
            {
                if (roundnumber < 1)
                {
                    throw new Exception("Main ainkey has been created");
                }
                for (int round = keys.Count; round <= roundnumber; round++)
                {
                    byte[,] newkey = new byte[4,4];
                    byte[,] previouskey = keys.ElementAt(round - 1).matric;
                    
                    byte[] previous_LastColumn = getColumn(3, round - 1);
                    previous_LastColumn = MyMath.cycleToLeft(previous_LastColumn, 1);
                    for (int i = 0; i < 4; i++)
                    {
                        newkey[i, 0] = (byte)(SBox(previous_LastColumn[i]) ^ previouskey[i, 0] ^ RconTable[i, round - 1]);
                    }
                    for(int j = 1; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            byte p = previouskey[i, j];
                            byte n = newkey[i, j - 1];
                            newkey[i, j] = (byte)(n ^ p);
                        }
                    }
                    keys.Add(new Matric(newkey));
                }
            }
        }
    }
}
