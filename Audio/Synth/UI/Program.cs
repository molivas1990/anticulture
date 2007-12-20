using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AntiCulture.Audio.Synth.UI
{
    public static class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new DeviceListForm());

            /*
            Console.WriteLine("Welcome to AntiCulture.Audio.Synth's grand debuts!");

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
            }*/
        }
    }
}
