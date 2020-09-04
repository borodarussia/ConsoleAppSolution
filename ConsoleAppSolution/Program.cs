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
        }

        private double _diam1, _diam2, _length, _area1, _area2;
        private int _input_node, _output_node;

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


    }

    class Program
    {
        static void Main(string[] args)
        {
            TubeChannel channel1 = new TubeChannel();
            channel1.Parameters(500,500,400);

            Console.WriteLine(channel1.tube_area_in());

        }
    }
}