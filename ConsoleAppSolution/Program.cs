﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

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
        private double _diam1, _diam2, _length, _area1, _area2, _area_mid;  
        
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
        private double _totpres1, _tottemp1, _totpres2, _tottemp2, _tottemp_mid;
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
            }

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

            /*Console.WriteLine(GetMassFlow(channels[0].TotalPressureIn(), channels[0].TotalPressureOut(), channels[0].area_mid(), channels[0].Ksi12_first(), channels[0].TotaTemperatureMid(), 287) + "\t" + channels[0].MassFlow());
            Console.WriteLine(GetMassFlow(channels[1].TotalPressureIn(), channels[1].TotalPressureOut(), channels[1].area_mid(), channels[1].Ksi12_first(), channels[1].TotaTemperatureMid(), 287) + "\t" + channels[1].MassFlow());*/
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
            double MassFlowNumerator;
            double MassFlowDenominator;
            int iter;
            double[] epsilon = new double[5]{ 0.1, 0.01, 0.001, 0.0001, 0.00001 };

            for (int i = 0; i < NumSection; i++)
            {
                MassFlowNumerator = 1;
                MassFlowDenominator = 1;
                delta = 1;
                iter = 0;
                if (SumSection[1, i] > 1)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        while (Math.Abs(delta) > epsilon[k] && MassFlowNumerator != 0)
                        {
                            iter += 1;
                            if (delta < 0)
                            {
                                for (int j = 0; j < NumChannel; j++)
                                {
                                    if (SumSection[0, i] == channels[j].output_section)
                                    {
                                        p_out = channels[j].TotalPressureOut() - epsilon[k] * 1000;
                                        p_in = channels[j].TotalPressureIn();
                                        t_mid = channels[j].TotaTemperatureMid();
                                        ksi_mid = channels[j].Ksi12_first();
                                        a_mid = channels[j].area_mid();
                                        channels[j].OutTmdParameters(p_out, channels[j].TotalTemperatureOut());
                                        MassFlowNumerator += GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid);
                                        MassFlowDenominator += GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid);
                                        //Console.WriteLine("Iter " + iter + ": " + GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid));
                                    }
                                    else if (SumSection[0, i] == channels[j].input_section)
                                    {
                                        p_in = channels[j].TotalPressureIn() - epsilon[k] * 1000;
                                        p_out = channels[j].TotalPressureOut();
                                        t_mid = channels[j].TotaTemperatureMid();
                                        ksi_mid = channels[j].Ksi12_first();
                                        a_mid = channels[j].area_mid();
                                        channels[j].InTmdParameters(p_in, channels[j].TotalTemperatureIn());
                                        MassFlowNumerator -= GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid);
                                        //Console.WriteLine("Iter " + iter + ": " + GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid));
                                    }
                                }
                            }
                            else if (delta > 0)
                            {
                                for (int j = 0; j < NumChannel; j++)
                                {
                                    if (SumSection[0, i] == channels[j].output_section)
                                    {
                                        p_out = channels[j].TotalPressureOut() + epsilon[k] * 1000;
                                        p_in = channels[j].TotalPressureIn();
                                        t_mid = channels[j].TotaTemperatureMid();
                                        ksi_mid = channels[j].Ksi12_first();
                                        a_mid = channels[j].area_mid();
                                        channels[j].OutTmdParameters(p_out, channels[j].TotalTemperatureOut());
                                        MassFlowNumerator += GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid);
                                        MassFlowDenominator += GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid);
                                        //Console.WriteLine("Iter " + iter + ": " + GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid));
                                    }
                                    else if (SumSection[0, i] == channels[j].input_section)
                                    {
                                        p_in = channels[j].TotalPressureIn() + epsilon[k] * 1000;
                                        p_out = channels[j].TotalPressureOut();
                                        t_mid = channels[j].TotaTemperatureMid();
                                        ksi_mid = channels[j].Ksi12_first();
                                        a_mid = channels[j].area_mid();
                                        channels[j].InTmdParameters(p_in, channels[j].TotalTemperatureIn());
                                        MassFlowNumerator -= GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid);
                                        //Console.WriteLine("Iter " + iter + ": " + GetMassFlow(p_in, p_out, a_mid, ksi_mid, t_mid));
                                    }
                                }
                            }
                            delta = MassFlowNumerator / MassFlowDenominator;
                            //Console.WriteLine("Iter " + iter + ": " + MassFlowNumerator + "\t" + MassFlowDenominator + "\t" + delta);
                        }
                        //Console.WriteLine("Global Iter Epsilon: " + (k + 1));
                        //Console.ReadLine();
                        //iter = 0;
                    }
                    
                }
            }
            for (int i = 0; i < NumChannel; i++)
            {
                Console.WriteLine("Канал №" + (i + 1));
                Console.WriteLine(channels[i].input_section + ":\t" + channels[i].TotalPressureIn() + "\t" + channels[i].TotalTemperatureIn() + "\n" + channels[i].output_section + ":\t" + channels[i].TotalPressureOut() + "\t" + channels[i].TotalTemperatureOut());
            }
            Console.WriteLine(GetMassFlow(channels[0].TotalPressureIn(), channels[0].TotalPressureOut(), channels[0].area_mid(), channels[0].Ksi12_first(), channels[0].TotaTemperatureMid()));
            Console.WriteLine(GetMassFlow(channels[1].TotalPressureIn(), channels[1].TotalPressureOut(), channels[1].area_mid(), channels[1].Ksi12_first(), channels[1].TotaTemperatureMid()));
        }
        static double GetMassFlow(double pressure_in, double pressure_out, double area_mid, double ksi_mid, double temper_mid, double R = 287)
        {
            double MassFlow;
            if (pressure_in == pressure_out || pressure_in < pressure_out)
            {
                MassFlow = Math.Sqrt((1000) * Math.Pow(area_mid, 2) / (ksi_mid * R * temper_mid));
            }
            else
            {
                MassFlow = Math.Sqrt((Math.Pow(pressure_in, 2) - Math.Pow(pressure_out, 2)) * Math.Pow(area_mid, 2) / (ksi_mid * R * temper_mid));
            }            
            return MassFlow;        
        }
        static double GetDensity(double pressure_in, double pressure_out, double area_mid, double ksi_mid, double massflow)
        {
            double Density;
            Density = ksi_mid * Math.Pow(massflow, 2) / (2 * Math.Pow(area_mid, 2) * (pressure_in - pressure_out));
            return Density;
        }
    }
}