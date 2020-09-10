using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;

namespace ConsoleAppSolution
{
    class TubeChannel
    {

        //const double R = 287;
        public void GeomParameters(double diam1, double diam2, double length)
        {
            _diam1 = diam1;
            _diam2 = diam2;
            _area1 = (double)Math.Pow(diam1 / 1000, 2) / 4 * Math.PI;
            _area2 = (double)Math.Pow(diam2 / 1000, 2) / 4 * Math.PI;
            _length = length;
            _area_mid = (_area1 + _area2) / 2;
        }
        public double tube_diameter_in()
        {
            return _diam1;
        }
        public double tube_diameter_out()
        {
            return _diam2;
        }
        public double area_in()
        {
            return _area1;
        }
        public double area_out()
        {
            return _area2;
        }
        public double area_mid()
        {
            return _area_mid;
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
        public void InTmdParameters(double TotalPressureIn = 0, double TotalTemperatureIn = 0)
        {
            _totpres1 = TotalPressureIn;
            _tottemp1 = TotalTemperatureIn;
        }
        public void OutTmdParameters(double TotalPressureOut = 0, double TotalTemperatureOut = 0)
        {
            _totpres2 = TotalPressureOut;
            _tottemp2 = TotalTemperatureOut;
        }
        public double TotaTemperatureMid()
        {
            _tottemp_mid = (_tottemp1 + _tottemp2) / 2;
            return _tottemp_mid;
        }
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
        public void GetKsi12(double ksi12 = 0.98) //Первичное значение кси
        {
            _ksi12 = ksi12;
        }
        public double Ksi12()
        {
            return _ksi12;
        }
        public double MachNumberMid()
        {
            return _machnumber_mid;
        }
        public double LambdaMid()
        {
            return _lambda_mid;
        }
        public double DensityMid()
        {
            return _density_mid;
        }
        public double MassFlow()
        {
            if (_totpres1 <= _totpres2)
            {
                _massflow = Math.Sqrt((Math.Pow(_totpres1, 2) - Math.Pow(_totpres1 - 1000, 2)) * Math.Pow(_area_mid, 2) / (_ksi12 * 287 * _tottemp_mid));
            }
            else
            {
                _massflow = Math.Sqrt((Math.Pow(_totpres1, 2) - Math.Pow(_totpres2, 2)) * Math.Pow(_area_mid, 2) / (_ksi12 * 287 * _tottemp_mid));
            }
            _density_mid = _ksi12 * Math.Pow(_massflow, 2) / (2 * Math.Pow(_area_mid, 2) * (_totpres1 - (_totpres1 - 1000)));
            _velocity_mid = _massflow / (_density_mid * _area_mid);
            _lambda_mid = _velocity_mid / Math.Sqrt(2 * 1.4 / (1.4 + 1) * 287 * _tottemp_mid);
            _machnumber_mid = _lambda_mid * (2 / (1.4 + 1)) / (1 - (1.4 - 1) / (1.4 + 1) * Math.Pow(_lambda_mid, 2));
            if (_totpres1 <= _totpres2)
            {
                _massflow = Math.Sqrt(1000 * 2 * _density_mid * Math.Pow(_area_mid, 2) / (1.4 * (1 + 0.25 * Math.Pow(_machnumber_mid, 2))));
            }
            else
            {
                _massflow = Math.Sqrt((_totpres1 - _totpres2) * 2 * _density_mid * Math.Pow(_area_mid, 2) / (1.4 * (1 + 0.25 * Math.Pow(_machnumber_mid, 2))));
            }
            return _massflow;
        }
        private double _machnumber_mid,_massflow, _density_mid, _velocity_mid, _lambda_mid, _tau_lambda_mid, _pi_lambda_mid, _epsilon_lambda_mid, _ksi12, _totpres1, _tottemp1, _totpres2, _tottemp2, _tottemp_mid, _diam1, _diam2, _length, _area1, _area2, _area_mid;
        private int _input_section, _output_section;
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

            if (NumSectionIn + NumSectionOut <= NumSection)
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
                double diameter_in = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите диаметр на выходе из канала №" + (i + 1));
                double diameter_out = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите длину канала №" + (i + 1));
                double length_channel = Convert.ToDouble(Console.ReadLine());
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
                        Console.WriteLine("Введите полное давление для " + SectionIn[i] + " сечения");
                        double total_pressure_in = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите полную температуру для " + SectionIn[i] + " сечения");
                        double total_temper_in = Convert.ToDouble(Console.ReadLine());
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
                        double tot_pressure_out = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите полную температуру для " + SectionOut[i] + " сечения");
                        double tot_temper_out = Convert.ToDouble(Console.ReadLine());
                        channels[j].OutTmdParameters(tot_pressure_out, tot_temper_out);
                    }
                }
            }
            for (int i = 0; i < NumChannel; i++)
            {
                Console.WriteLine("Потери для канала №" + (i + 1));
                double ksi = Convert.ToDouble(Console.ReadLine());
                channels[i].GetKsi12(ksi);
            }
            //***
            Console.WriteLine("\n***\n");

            double pressure_max1 = 0;
            double pressure_min1 = 10000000000000000000;

            for (int i = 0; i < NumChannel; i++)
            {
                if (pressure_max1 < channels[i].TotalPressureOut())
                {
                    pressure_max1 = channels[i].TotalPressureOut();
                }
                else if (pressure_max1 < channels[i].TotalPressureIn())
                {
                    pressure_max1 = channels[i].TotalPressureIn();
                }

                if (pressure_min1 > channels[i].TotalPressureOut() && channels[i].TotalPressureOut() != 0)
                {
                    pressure_min1 = channels[i].TotalPressureOut();
                }
                else if (pressure_min1 > channels[i].TotalPressureIn() && channels[i].TotalPressureIn() != 0)
                {
                    pressure_min1 = channels[i].TotalPressureIn();
                }
            }

            double pressure_mid1 = (pressure_max1 + pressure_min1) / 2;

            double pressure_max = 0;
            double pressure_min = 10000000000000000000;
            for (int i = 0; i < NumChannel; i++)
            {
                if (pressure_max < channels[i].TotalPressureOut() && pressure_max1 != channels[i].TotalPressureOut())
                {
                    pressure_max = channels[i].TotalPressureOut();
                }
                else if (pressure_max < channels[i].TotalPressureIn() && pressure_max1 != channels[i].TotalPressureIn())
                {
                    pressure_max = channels[i].TotalPressureIn();
                }

                if (pressure_min > channels[i].TotalPressureOut() && channels[i].TotalPressureOut() != 0 && pressure_min1 != channels[i].TotalPressureOut())
                {
                    pressure_min = channels[i].TotalPressureOut();
                }
                else if (pressure_min > channels[i].TotalPressureIn() && channels[i].TotalPressureIn() != 0 && pressure_min1 != channels[i].TotalPressureIn())
                {
                    pressure_min = channels[i].TotalPressureIn();
                }
            }
            double pressure_mid = (pressure_max + pressure_min) / 2;

            for (int i = 0; i < NumChannel; i++)
            {
                if (channels[i].TotalPressureOut() == 0)
                {
                    double tot_pressure = pressure_min;
                    double tot_temper = channels[0].TotalTemperatureIn();
                    channels[i].OutTmdParameters(tot_pressure, tot_temper);
                }
                else if (channels[i].TotalPressureIn() == 0)
                {
                    double tot_pressure = pressure_min;
                    double tot_temper = channels[0].TotalTemperatureIn();
                    channels[i].InTmdParameters(tot_pressure, tot_temper);
                }
            }

            for (int i = 0; i < NumChannel; i++)
            {
                for (int j = 0; j < NumChannel; j++)
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

            // Вычисление

            int SumSect;
            int[,] SumSection = new int[2, NumSection];

            for (int i = 0; i < NumSection; i++)
            {
                SumSect = 0;
                for (int j = 0; j < NumChannel; j++)
                {
                    if (i + 1 == channels[j].input_section)
                    {
                        SumSect += 1;
                    }
                    else if (i + 1 == channels[j].output_section)
                    {
                        SumSect += 1;
                    }
                }
                SumSection[0, i] = i + 1;
                SumSection[1, i] = SumSect;
            }

            double delta;
            double p_out, p_in, t_mid, ksi_mid, a_mid;
            double p_max, p_min, p_mid;
            double MassFlowNumerator;

            for (int num_iter = 0; num_iter < 100; num_iter++)
            {
                p_max = 0;
                p_min = 1000000000;

                for (int i = 0; i < NumSection; i++)
                {
                    delta = 1000000;
                    if (SumSection[1, i] > 1)
                    {
                        for (int j = 0; j < NumChannel; j++)
                        {
                            if (SumSection[0, i] == channels[j].output_section || SumSection[0, i] == channels[j].input_section)
                            {
                                if (p_max < channels[j].TotalPressureOut())
                                {
                                    p_max = channels[j].TotalPressureOut();
                                }
                                if (p_max < channels[j].TotalPressureIn())
                                {
                                    p_max = channels[j].TotalPressureIn();
                                }
                                if (p_min > channels[j].TotalPressureOut())
                                {
                                    p_min = channels[j].TotalPressureOut();
                                }
                                if (p_min > channels[j].TotalPressureIn())
                                {
                                    p_min = channels[j].TotalPressureIn();
                                }
                            }
                        }
                        p_mid = (p_min + p_max) / 2;


                        while (Math.Abs(delta) > 0.0000001)
                        {
                            MassFlowNumerator = 0;
                            for (int j = 0; j < NumChannel; j++)
                            {
                                if (SumSection[0, i] == channels[j].output_section)
                                {
                                    p_out = p_mid;
                                    p_in = channels[j].TotalPressureIn();
                                    t_mid = channels[j].TotaTemperatureMid();
                                    //ksi_mid = channels[j].Ksi12();
                                    //a_mid = channels[j].area_mid();
                                    channels[j].OutTmdParameters(p_out, channels[j].TotalTemperatureOut());
                                    MassFlowNumerator += channels[j].MassFlow();
                                }
                                else if (SumSection[0, i] == channels[j].input_section)
                                {
                                    p_in = p_mid;
                                    p_out = channels[j].TotalPressureOut();
                                    t_mid = channels[j].TotaTemperatureMid();
                                    //ksi_mid = channels[j].Ksi12();
                                    //a_mid = channels[j].area_mid();
                                    channels[j].InTmdParameters(p_in, channels[j].TotalTemperatureIn());
                                    MassFlowNumerator -= channels[j].MassFlow();
                                }
                            }
                            if (MassFlowNumerator == delta)
                            {
                                break;
                            }
                            else
                            {
                                delta = MassFlowNumerator;
                                if (delta > 0)
                                {
                                    p_min = p_mid;
                                    p_mid = (p_max + p_min) / 2;
                                }
                                else if (delta < 0)
                                {
                                    p_max = p_mid;
                                    p_mid = (p_max + p_min) / 2;
                                }
                            }
                        }

                    }
                }
            }
            for (int i = 0; i < NumChannel; i++)
            {
                Console.WriteLine("Finish Канал №" + (i + 1));
                Console.WriteLine(channels[i].input_section + ":\t" + channels[i].TotalPressureIn() + "\t" + channels[i].TotalTemperatureIn() + "\n" + channels[i].output_section + ":\t" + channels[i].TotalPressureOut() + "\t" + channels[i].TotalTemperatureOut() + "\n" + channels[i].MassFlow() + "\t" + channels[i].MachNumberMid() + "\t" + channels[i].DensityMid());
            }
        }
    }
}
