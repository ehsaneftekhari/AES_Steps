using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAES
{
    class Program
    {
        static void Main(string[] args)
        {



            //AESA();



            /*****************************************/
            //testc Galva Math:
            //byte[,] test =
            //{
            //    {0xd4, 0xe0, 0xb8, 0x1e },
            //    {0xbf, 0xb4, 0x41, 0x27 },
            //    {0x5d, 0x52, 0x11, 0x98 },
            //    {0x30, 0xae, 0xf1, 0xe5 },
            //};
            //Matric p = new Matric(test);
            //Console.Write(p.getPrintStringInHEXMatric());
            //p = AES.MixColumns(p, true);
            //Console.WriteLine();
            //Console.Write(p.getPrintStringInHEXMatric());
            /*********************************************************************/

            /*****************************************/
            //test Keys:
            //byte[,] test =
            //{
            //    {0x2b, 0x28, 0xab, 0x09 },
            //    {0x7e, 0xae, 0xf7, 0xcf },
            //    {0x15, 0xd2, 0x15, 0x4f },
            //    {0x16, 0xa6, 0x88, 0x3c },
            //};

            //AES.AESKey testkey = new AES.AESKey(test);
            //for (int i = 0; i < 11; i++)
            //{
            //    Console.WriteLine(i);
            //    Console.WriteLine(testkey.getKey(i).getPrintStringInHEXMatric());
            //}
            /*********************************************************************/

            /*****************************************/
            //test GalvaMath:



            Console.WriteLine(AES.mixBlock(0x96, 0x04, true));

            /*********************************************************************/

            //MyMath.BinaryPolynomial num1 = new MyMath.BinaryPolynomial(0x255);
            //MyMath.BinaryPolynomial num2 = new MyMath.BinaryPolynomial(0x11B);
            //Console.Write(num1.getPrintString()+" mod ");
            //Console.WriteLine(num2.getPrintString()+" =");
            //Console.WriteLine(MyMath.BinaryPolynomial.galvaMod(num1, num2).getPrintString());


            Console.ReadLine();
        }


        public static void AESA()
        {
            byte[,] testPlainText =
                {
                    {0x32, 0x88, 0x31, 0xe0 },
                    {0x43, 0x5a, 0x31, 0x37 },
                    {0xf6, 0x30, 0x98, 0x07 },
                    {0xa8, 0x8d, 0xa2, 0x34 },
                };
            byte[,] testKey =
            {
                    {0x2b, 0x28, 0xab, 0x09 },
                    {0x7e, 0xae, 0xf7, 0xcf },
                    {0x15, 0xd2, 0x15, 0x4f },
                    {0x16, 0xa6, 0x88, 0x3c },
                };


            while (true)
            {

                Matric PT = null;
                bool f = false;
                Matric K = null;

                Console.WriteLine("Run Test? (Yes:\"y\"|No:other cahr)");
                if (Console.ReadLine().ToLower().Equals("y"))
                {
                    PT = new Matric(testPlainText);
                    K = new Matric(testKey);
                }
                else
                {

                    do
                    {
                        try
                        {
                            Console.WriteLine("Enter Plain Text:");
                            string PlainText = Console.ReadLine();
                            PT = new Matric(PlainText);
                            f = false;
                        }
                        catch (Exception Ex)
                        {
                            f = true;
                            Console.WriteLine(Ex.Message);
                        }
                    } while (f);


                    do
                    {
                        try
                        {
                            Console.WriteLine("Enter Key:");
                            string key = Console.ReadLine();
                            K = new Matric(key);
                            f = false;
                        }
                        catch (Exception Ex)
                        {
                            f = true;
                            Console.WriteLine(Ex.Message);
                        }
                    } while (f);

                }

                Console.WriteLine("--------------------------------------------------------------------"
                    + "\n Plain Text Matric:\n" + PT.getPrintStringInHEXMatric());

                Console.WriteLine("--------------------------------------------------------------------"
                    + "\n Key Matric:\n" + K.getPrintStringInHEXMatric() + "\n");



                Console.WriteLine("Enter The number of rounds you want to calculate:");
                int rounds = int.Parse(Console.ReadLine());

                bool details = true;
                Console.WriteLine("if you want to see details, Enter \"y\". and if you dont! Enter any char ");
                if (!Console.ReadLine().ToLower().Equals("y"))
                {
                    details = false;
                }

                Console.Write("press Enter...");
                Console.ReadLine();


                Console.Write(AES.AESalgorithm(PT, K, rounds, details).getPrintStringInHEXMatric());
            }
        }

    }
}
