using System;
using System.Collections.Generic;
using Autofac;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterModule<BaseModule>();
            IContainer container = builder.Build();

            var appService = container.Resolve<IAppService>();
            var timer = new ParkingTimer();
            var validator = new Validation();

            while (!validator.IsValid)
            {
                var input = new List<string>();

                Console.WriteLine("Entery date & time in format of (DD/MM/YYYY HH:mm) : ");
                input.Add(Console.ReadLine());

                Console.WriteLine("Exit date & time in format of  (DD/MM/YYYY HH:mm) : ");
                input.Add(Console.ReadLine());

                validator = appService.ValidateInput(input, out timer);
                if (!validator.IsValid) {
                    Console.WriteLine(validator.ErrorMessage);
                }
            }

            try
            {
                var response = appService.Process(timer).Result;
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message + "\n");
            }

            Console.WriteLine(" ");
            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }
    }
}
