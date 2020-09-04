using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleAppSolution
{
    class TubeChannel
    {
        private double _diam1, _diam2, _length;

        public double tube_diameter_1
        {
            get => _diam1;
            set => _diam1 = value;
        }
        public double tube_diameter_2
        {
            get => _diam2;
            set => _diam2 = value;
        }
        public double tube_length
        {
            get => _length;
            set => _length = value;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            TubeChannel channel1 = new TubeChannel();

            channel1.tube_diameter_1 = 500;
            channel1.tube_diameter_2 = 500;
            channel1.tube_length = 200;

            Console.WriteLine("Check, " + channel1.tube_diameter_1);

            //Console.ReadKey()

        }
    }
}
