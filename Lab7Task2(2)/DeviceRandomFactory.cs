using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal class DeviceRandomFactory
    {
        private static Random rnd = new Random();

        // Створює випадковий пристрій (будь-якого типу)
        public static Device CreateRandomDevice()
        {
            // Вибираємо випадковий тип пристрою
            int deviceType = rnd.Next(5);

            switch (deviceType)
            {
                case 0:
                    {
                        return AirplaneUtils.CreateRandom();
                    }
                case 1:
                    {
                        return HelicopterUtils.CreateRandom();
                    }
                case 2:
                    {
                        return HotAirBalloonUtils.CreateRandom();
                    }
                case 3:
                    {
                        return DeltaplaneUtils.CreateRandom();
                    }
                default:
                    {
                        return FlyingCarpetUtils.CreateRandom();
                    }
            }
        }
    }
}
