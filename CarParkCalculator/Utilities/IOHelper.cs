using System;
using System.IO;

namespace CarParkCalculator.Utilities
{
    public static class IOHelper
    {
        public static string ReadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("*********ERROR************");
                Console.WriteLine("Reading File Failed : " + ex);
                return "";
            }
        }
    }
}
