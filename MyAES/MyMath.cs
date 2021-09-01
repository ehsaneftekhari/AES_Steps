using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace MyAES
{
    class MyMath
    {
        public static string DectoHex(int number)
        {
            if (number == 0)
            {
                return "0";
            }
            int i = 0;
            int num = number;
            string m = "";
            while (num > 0)
            {
                i = num % 16;
                switch (i)
                {
                    case 10: m += "A"; break;
                    case 11: m += "B"; break;
                    case 12: m += "C"; break;
                    case 13: m += "D"; break;
                    case 14: m += "E"; break;
                    case 15: m += "F"; break;
                    default: m += i; break;
                }
                num = num / 16;
            }
            char[] ch = m.ToCharArray();
            Array.Reverse(ch);
            string returner = "";
            foreach (char character in ch)
            {
                returner += character.ToString();
            }
            return returner;
        }

        public static int HexToDec(string number)
        {
            return int.Parse(number, NumberStyles.HexNumber);
        }

        public static Matric XOR(Matric a, Matric b)
        {
            Matric ans = new Matric(4, 4);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ans.matric[i, j] = (byte)(a.matric[i, j] ^ b.matric[i, j]);
                }
            }
            return ans;
        }

        public static byte[] cycleToLeft(byte[] b, int count)
        {
            byte[] temp = new byte[b.Length];
            b.CopyTo(temp, 0);
            for (int i = 0; i < count; i++)
            {
                byte tempb = temp[0];
                int j = 0;
                for (; j < temp.Length - 1; j++)
                {
                    temp[j] = temp[j + 1];
                }
                temp[j] = tempb;
            }
            return temp;
        }

        public class BinaryPolynomial
        {
            public List<monomial> monomiallist { get; }

            public BinaryPolynomial(int number)
            {
                if (number < 0)
                {
                    throw new Exception("number should be more than 0");
                }
                monomiallist = new List<monomial>();
                for (int i = 0; number > 0; i++)
                {
                    monomiallist.Add(new monomial(number % 2, i));
                    number /= 2;
                }
                monomiallist.RemoveAll(x => x.coefficient == 0);

            }

            public void copyTo(BinaryPolynomial BP)
            {
                foreach (monomial monomial in monomiallist)
                {
                    BP.addOrRemove(monomial.coefficient, monomial.power);
                }
            }

            public void addOrRemove(int coefficient, int power)
            {
                if (coefficient == 0)
                    return;

                monomial temp = monomiallist.FirstOrDefault(x => x.power == power);
                if (temp == null)
                {
                    monomiallist.Add(new monomial(coefficient, power));
                }
                else
                {
                    temp.coefficient += coefficient;
                }
                monomiallist.RemoveAll(x => x.coefficient == 0);
            }

            public void directeditMonomial(int coefficient, int power)
            {
                if (coefficient == 0)
                    return;

                monomial temp = monomiallist.FirstOrDefault(x => x.power == power);
                if (temp == null)
                {
                    throw new Exception("Monomial with this power does not exists");
                }
                else
                {
                    temp.coefficient = coefficient;
                }
                monomiallist.RemoveAll(x => x.coefficient == 0);
            }

            public void removeMonomial(int power)
            {
                monomiallist.RemoveAll(x => x.power == power);
            }

            public byte getByte()
            {
                byte number = 0;
                foreach (monomial monomial in monomiallist)
                {
                    number += (byte)(Math.Pow(2, monomial.power) * monomial.coefficient);
                }
                return number;
            }

            public int getInt()
            {
                int number = 0;
                foreach (monomial monomial in monomiallist)
                {
                    number += (int)(Math.Pow(2, monomial.power) * monomial.coefficient);
                }
                return number;
            }

            public int getBigestPower()
            {
                if (monomiallist.Count == 0)
                {
                    return -1;
                }
                int BigestPower = monomiallist.First().power;
                for (int i = 1; i < monomiallist.Count(); i++)
                {
                    if (BigestPower < monomiallist.ElementAt(i).power)
                        BigestPower = monomiallist.ElementAt(i).power;
                }
                return BigestPower;
            }

            public string getPrintString()
            {
                string returner = "";
                bool f = false;
                foreach (monomial monomial in monomiallist)
                {
                    if (f)
                    {
                        returner += "+";
                    }
                    f = true;
                    returner += monomial.print();
                }
                return returner;
            }

            public static BinaryPolynomial galvaMultiplication(BinaryPolynomial a, BinaryPolynomial b)
            {
                BinaryPolynomial temp = new BinaryPolynomial(0);
                if (a.getByte() < 0 || b.getByte() < 0)
                {
                    throw new Exception("a or b should not be lower than 0");
                }
                List<monomial> monomiallist1 = a.monomiallist;
                List<monomial> monomiallist2 = b.monomiallist;
                foreach (monomial monomial1 in monomiallist1)
                {
                    foreach (monomial monomial2 in monomiallist2)
                    {
                        temp.addOrRemove(1, monomial1.power + monomial2.power);
                    }
                }
                monomial[] temp_monomiallist = new monomial[temp.monomiallist.Count];
                temp.monomiallist.CopyTo(temp_monomiallist);

                monomial monomial;
                for (int i = 0; i < temp_monomiallist.Length; i++)
                {
                    monomial = temp_monomiallist[i];
                    if (monomial.coefficient % 2 == 0)
                        temp.removeMonomial(monomial.power);
                    else
                        temp.directeditMonomial(1, monomial.power);
                }
                //foreach (monomial monomial in temp_monomiallist)
                //{
                //    if (monomial.coefficient % 2 == 0)
                //        temp.removeMonomial(monomial.power);
                //    else
                //        temp.directeditMonomial(1, monomial.power);
                //}

                return temp;
            }

            public static BinaryPolynomial galvaMod(BinaryPolynomial a, BinaryPolynomial b)
            {
                int a_BigestPower = a.getBigestPower();
                int b_BigestPower = b.getBigestPower();

                if (a_BigestPower < b_BigestPower)
                {
                    throw new Exception("a or b should not be empty or hava power lower than 0");
                }

                BinaryPolynomial temp_a = new BinaryPolynomial(0);
                a.copyTo(temp_a);
                BinaryPolynomial temp_b = new BinaryPolynomial(0);
                b.copyTo(temp_b);
                BinaryPolynomial temp = new BinaryPolynomial(0);
                int powers_difference;
                while (temp_a.getBigestPower() > 7)
                {
                    a_BigestPower = temp_a.getBigestPower();
                    temp = new BinaryPolynomial(0);
                    powers_difference = a_BigestPower - b_BigestPower;
                    foreach (monomial monomial in temp_b.monomiallist)
                    {
                        temp.addOrRemove(1, monomial.power + powers_difference);
                    }
                    temp_a = polynomialXOR(temp, temp_a);
                }
                return temp_a;
            }
            public static BinaryPolynomial polynomialXOR(BinaryPolynomial a, BinaryPolynomial b)
            {
                int temp_a = a.getInt();
                int temp_b = b.getInt();
                int returner = temp_a ^ temp_b;
                return new BinaryPolynomial(returner);
            }

        }

        
    

        public class monomial
        {
            public int power { get; set; }
            public int coefficient { get; set; }

            public monomial(int coefficient, int power)
            {
                this.coefficient = coefficient;
                this.power = power;
            }

            public string print()
            {
                if (power == 0)
                    return "1";

                if (coefficient == 0)
                    return "0";

                if (coefficient == 1)
                    return "(X^" + power + ")";

                return "(" + coefficient + "X^" + power + ")";
            }
        }
    }
}
