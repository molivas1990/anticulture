using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio
{
    public static class Program
    {
        public static void Main()
        {
            Devices.OpenAlOutput outputDevice = new Devices.OpenAlOutput();
            Devices.SimpleWaveGenerator waveGeneratorDevice = new Devices.SimpleWaveGenerator();
            waveGeneratorDevice.OutputSlot.Connect(outputDevice.LeftChannelInputSlot);

            Console.WriteLine("Welcome to AntiCulture.Audio's grand debuts!");

            while (true)
            {
                Console.Write("Enter the type of wave to be outputted : ");
                string typeString = Console.ReadLine();

                Devices.SimpleWaveGenerator.WaveType type;
                if (typeString.Equals("sine", StringComparison.InvariantCultureIgnoreCase)) type = Devices.SimpleWaveGenerator.WaveType.Sine;
                else if (typeString.Equals("square", StringComparison.InvariantCultureIgnoreCase)) type = Devices.SimpleWaveGenerator.WaveType.Square;
                else if (typeString.Equals("saw", StringComparison.InvariantCultureIgnoreCase)) type = Devices.SimpleWaveGenerator.WaveType.Saw;
                else if (typeString.Equals("triangle", StringComparison.InvariantCultureIgnoreCase)) type = Devices.SimpleWaveGenerator.WaveType.Triangle;
                else
                {
                    Console.WriteLine("Unknown wave type");
                    continue;
                }

                Console.WriteLine("Outputting...");
                waveGeneratorDevice.Generate(type, 1024 * 20, 24000);

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
