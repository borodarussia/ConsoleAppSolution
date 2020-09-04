using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ConsoleAppSolution
{
    class TubeChannel
    {


        public void GeomParameters(double diam1, double diam2, double length)
        {
            _diam1 = diam1;
            _diam2 = diam2;
            _area1 = (double)Math.Pow(diam1 / 1000, 2) / 4 * Math.PI;
            _area2 = (double)Math.Pow(diam2 / 1000, 2) / 4 * Math.PI;
            _length = length;
        }
        private double _diam1, _diam2, _length, _area1, _area2;  
        
        public double tube_diameter_in()
        {
            return _diam1;
        }
        public double tube_diameter_out()
        {
            return _diam2;
        }
        public double tube_area_in()
        {
            return _area1;
        }
        public double tube_area_out()
        {
            return _area2;
        }
        public double tube_length()
        {
            return _length;
        }
        public int input_section
        {
            get => _input_section;
            set => _input_section = value;
        }
        public int output_section
        {
            get => _output_section;
            set => _output_section = value;
        }
        private int _input_section, _output_section;
        public void InTmdParameters(double TotalPressureIn = 0,  double TotalTemperatureIn = 0)
        {
            _totpres1 = TotalPressureIn;
            _tottemp1 = TotalTemperatureIn;
        }
        public void OutTmdParameters(double TotalPressureOut = 0, double TotalTemperatureOut = 0)
        {
            _totpres2 = TotalPressureOut;
            _tottemp2 = TotalTemperatureOut;
        }
        private double _totpres1, _tottemp1, _totpres2, _tottemp2;
        public double TotalPressureIn()
        {
            return _totpres1;
        }
        public double TotalPressureOut()
        {
            return _totpres2;
        }
        public double TotalTemperatureIn()
        {
            return _tottemp1;
        }
        public double TotalTemperatureOut()
        {
            return _tottemp2;
        }
        public double Ksi12_first() //Первичное значение кси
        {
            _ksi12 = 0.98;
            return _ksi12;
        }
        private double _ksi12;

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество каналов");
            int NumChannel = Convert.ToInt32(Console.ReadLine());
            int NumSection = NumChannel + 1;    //Подсчитывается количество сечений(узлов) в схеме

            //***
            Console.WriteLine("\n***\n");

            Console.WriteLine("Введите количество входных сечений");
            int NumSectionIn = Convert.ToInt32(Console.ReadLine());

            //***
            Console.WriteLine("\n***\n");


            Console.WriteLine("Введите количество выходных сечений");
            int NumSectionOut = Convert.ToInt32(Console.ReadLine());

            //***
            Console.WriteLine("\n***\n");

            double[] SectionIn = new double[NumSectionIn];
            double[] SectionOut = new double[NumSectionOut];

            if (NumSectionIn + NumSectionOut < NumSection)
            {
                Console.WriteLine("Введите номера входных сечений");                
                for (int i = 0; i < NumSectionIn; i++)
                {
                    SectionIn[i] = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Введите номера выходных сечений");                
                for (int i = 0; i < NumSectionOut; i++)
                {
                    SectionOut[i] = Convert.ToInt32(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Заданное количество входных и выходных сечений больше или равно общему количеству полученных сечений");
            }

            TubeChannel[] channels = new TubeChannel[NumChannel];

            //***
            Console.WriteLine("\n***\n");

            Console.WriteLine("Заполните номера сечений для каналов");
            for (int i = 0; i < NumChannel; i++)
            {
                channels[i] = new TubeChannel();
                Console.WriteLine("Введите номер входного сечения для канала №" + (i + 1));
                channels[i].input_section = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите номер выходного сечения для канала №" + (i + 1));
                channels[i].output_section = Convert.ToInt32(Console.ReadLine());
                //***
                Console.WriteLine("\n***\n");
            }

            //***
            //Console.WriteLine("\n***\n");

            Console.WriteLine("Заполните диаметры и длину канала");
            for (int i = 0; i < NumChannel; i++)
            {
                Console.WriteLine("Введите диаметр на входе в канал №" + (i + 1));
                double diameter_in = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите диаметр на выходе из канала №" + (i + 1));
                double diameter_out = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите длину канала №" + (i + 1));
                double length_channel = Convert.ToInt32(Console.ReadLine());
                channels[i].GeomParameters(diameter_in, diameter_out, length_channel);
                //***
                Console.WriteLine("\n***\n");
            }

            Console.WriteLine("Введите термогазодинамические параметры для входных сечений");
            for (int i = 0; i < NumSectionIn; i++)
            {
                for (int j = 0; j < NumChannel; j++)
                {
                    if (SectionIn[i] == channels[j].input_section)
                    {
                        Console.WriteLine("Введите полное давление для " + SectionIn[i] + " сечения" );
                        double total_pressure_in = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите полную температуру для " + SectionIn[i] + " сечения");
                        double total_temper_in = Convert.ToInt32(Console.ReadLine());
                        channels[j].InTmdParameters(total_pressure_in, total_temper_in);
                    }
                }
            }

            //***
            Console.WriteLine("\n***\n");

            //Console.WriteLine("Check " + channels[0].TotalTemperatureOut());

            Console.WriteLine("Введите термогазодинамические параметры для выходных сечений");
            for (int i = 0; i < NumSectionOut; i++)
            {
                for (int j = 0; j < NumChannel; j++)
                {
                    if (SectionOut[i] == channels[j].output_section)
                    {
                        Console.WriteLine("Введите полное давление для " + SectionOut[i] + " сечения");
                        double tot_pressure_out = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите полную температуру для " + SectionOut[i] + " сечения");
                        double tot_temper_out = Convert.ToInt32(Console.ReadLine());
                        channels[j].OutTmdParameters(tot_pressure_out, tot_temper_out);
                    }
                }
            }

            //***
            Console.WriteLine("\n***\n");

            for (int i = 0; i < NumChannel; i++)
            {
                if (channels[i].TotalPressureOut() == 0)
                {
                    double tot_pressure = channels[0].TotalPressureIn();                   
                    double tot_temper = channels[0].TotalTemperatureIn();
                    channels[i].OutTmdParameters(tot_pressure, tot_temper);
                }
                else if (channels[i].TotalPressureIn() == 0)
                {
                    double tot_pressure = channels[0].TotalPressureIn();
                    double tot_temper = channels[0].TotalTemperatureIn();
                    channels[i].InTmdParameters(tot_pressure, tot_temper);
                }
                /*else if (channels[i].TotalPressureOut() == 0 && channels[i].TotalPressureIn() == 0) //Необходимо будет проверить на более сложных моделях, скорее всего нужно будет ввести ряд дополнительных ограничений
                {
                    double tot_pressure = channels[i - 1].TotalPressureIn();
                    double tot_temper = channels[i - 1].TotalTemperatureIn();
                    channels[i].InTmdParameters(tot_pressure, tot_temper);
                    channels[i].OutTmdParameters(tot_pressure, tot_temper);
                }*/
            }

            /*for (int i = 0; i < NumChannel; i++)
            {
                if (i+1 < NumChannel)
                {
                    for (int j = i + 1; j < NumChannel; j++)
                    {
                        if (channels[i].output_section == channels[j].output_section)
                        {
                            channels[j].OutTmdParameters(channels[i].TotalPressureOut(), channels[i].TotalTemperatureOut());
                        }
                    }
                }
            }*/

            for (int i = 0; i < NumChannel; i++)
            {
                for (int j = 0; j < NumChannel; j ++)
                {
                    if (channels[i].output_section == channels[j].output_section && i != j)
                    {
                        channels[j].OutTmdParameters(channels[i].TotalPressureOut(), channels[i].TotalTemperatureOut());
                    }
                    else if (channels[i].output_section == channels[j].input_section)
                    {
                        channels[j].InTmdParameters(channels[i].TotalPressureOut(), channels[i].TotalTemperatureOut());
                    }
                }
            }

            for (int i = 0; i < NumChannel; i++)
            {
                Console.WriteLine("Канал №" + (i + 1));
                Console.WriteLine(channels[i].input_section + ":\t" + channels[i].TotalPressureIn() + "\t" + channels[i].TotalTemperatureIn() + "\n" + channels[i].output_section +  ":\t" + channels[i].TotalPressureOut() + "\t" + channels[i].TotalTemperatureOut());
            }
        }
    }
}