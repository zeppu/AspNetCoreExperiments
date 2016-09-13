using System;
using System.Collections.Generic;
using Glyde.Core.DeviceDetection.Models;
using Glyde.Core.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Glyde.Core.DeviceDetection.Services
{
    public class DeviceInformationService : IRequestContextDataGenerator<IDeviceInformation>
    {
        public IDeviceInformation GenerateData(HttpContext context)
        {
            var ua = context.Request.Headers[HeaderNames.UserAgent];
            return new DeviceInformation(ua);
        }

        static void Main(string[] args)
        {
            string[] inputs;
            int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
            var land = new Dictionary<int, int>();
            int ourX = 0;
            for (int i = 0; i < surfaceN; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
                int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.

                land.Add(landX, landY);
            }

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int X = int.Parse(inputs[0]);
                int Y = int.Parse(inputs[1]);
                int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
                int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
                int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
                int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
                int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                int altitude = 0;

                foreach (var landKey in land.Keys)
                {
                    if (landKey <= X)
                        altitude = land[landKey];
                    else
                        break;

                }

                Console.Error.WriteLine($"X: {X}");
                if (altitude < 500)
                    Console.WriteLine("0 3");
                else
                    Console.WriteLine("0 1");

            }
        }
    }
}