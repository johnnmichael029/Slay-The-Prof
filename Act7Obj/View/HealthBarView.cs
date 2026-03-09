using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class HealthBarView
    {
        public static void DrawHealthBar(int current, int max, ConsoleColor color)
        {
            int barWidth = 30;
            float percentage = max > 0 ? (float)current / max : 0;
            int filled = (int)(percentage * barWidth);

            Console.Write("  HP: [");
            Console.ForegroundColor = color;
            Console.Write(new string('█', Math.Max(0, filled)));
            Console.ResetColor();
            Console.Write(new string('░', Math.Max(0, barWidth - filled)));
            Console.WriteLine($"] {current}/{max}");
        }
    }
}
