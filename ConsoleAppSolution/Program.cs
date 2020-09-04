using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppSolution
{
    class TubeChannel
    {
   


        public void Parameters(double diam1, double diam2, double length)
        {
            _diam1 = diam1;
            _diam2 = diam2;
            _area1 = (double)Math.Pow(diam1 / 1000, 2) / 4 * Math.PI;
            _area2 = (double)Math.Pow(diam2 / 1000, 2) / 4 * Math.PI;
            _length = length;
        }

        private double _diam1 = 0, _diam2 = 0, _length = 0, _area1 = 0, _area2 = 0;
        
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

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество каналов");
            int NumChannel = Convert.ToInt32(Console.ReadLine());
            int NumSection = NumChannel + 1;    //Подсчитывается количество сечений(узлов) в схеме

            Console.WriteLine("Введите количество входных сечений");
            int NumSectionIn = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите количество выходных сечений");
            int NumSectionOut = Convert.ToInt32(Console.ReadLine());

            if (NumSectionIn + NumSectionOut < NumSection)
            {
                Console.WriteLine("Введите номера входных сечений");
                double[] SectionIn = new double[NumSectionIn];
                for (int i = 0; i < NumSectionIn; i++)
                {
                    SectionIn[i] = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Введите номера выходных сечений");
                double[] SectionOut = new double[NumSectionOut];
                for (int i = 0; i < NumSectionOut; i++)
                {
                    SectionOut[i] = Convert.ToInt32(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Заданное количество входных и выходных сечений больше или равно общему количеству полученных сечений");
            }

            Console.WriteLine("Заполните номера сечений для каналов");

            TubeChannel[] channels = new TubeChannel[NumChannel];

            for (int i = 0; i < NumChannel; i++)
            {
                channels[i] = new TubeChannel();
                Console.WriteLine("Введите номер входного сечения для канала №" + (i + 1));
                channels[i].input_section = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите номер выходного сечения для канала №" + (i + 1));
                channels[i].output_section = Convert.ToInt32(Console.ReadLine());
            }       
            

        }
    }
}