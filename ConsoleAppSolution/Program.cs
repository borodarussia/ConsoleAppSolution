using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppSolution
{
    class TubeChannel
    {
        private double _diam1, _diam2, _length;
        private int _input_node, _output_node;

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
        public int channel_input_node
        {
            get => _input_node;
            set => _input_node = value;
        }
        public int channel_outpute_node
        {
            get => _output_node;
            set => _output_node = value;
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

            Console.WriteLine("Check, " + channel1.tube_square_1);


        }
    }
}
