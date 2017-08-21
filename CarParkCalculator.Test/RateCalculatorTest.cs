using System;
using System.Collections.Generic;
using Autofac;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarParkCalculator.Test
{
    public class CalculatorCase
    {
        public ParkingTimer TimerData { get; set; }
        public ParkingRate Expected { get; set; }
    }

    [TestClass]
    public class RateCalculatorTest
    {

        private ICalculateService _calculateService;

        [TestInitialize]
        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BaseModule>();
            IContainer container = builder.Build();

            _calculateService = container.Resolve<ICalculateService>();
        }


        [TestMethod]
        public void SpecialType_WhenEnterEarly_ThenReturnEarlyBirdRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 6, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 15, 30, 0)
                    },
                    Expected = new ParkingRate
                    {
                        Name = "Early Bird",
                        Price = 13
                    }
                },
                
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 9, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 23, 30, 0)
                    },
                    Expected = new ParkingRate
                    {
                        Name = "Early Bird",
                        Price = 13
                    }
                },

                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new  DateTime(2017, 8, 15, 7, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 10, 00, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Early Bird",
                        Price = 13
                    }
                }
            };

            foreach (var c in cases)
            {
                var result = _calculateService.Calculate(c.TimerData).Result;
                Assert.AreEqual(c.Expected.Name, result.Name);
                Assert.AreEqual(c.Expected.Price, result.Price);
            }
        }

        [TestMethod]
        public void SpecialType_WhenEnterLate_ThenReturnNightRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer()
                    {
                        Entry = new DateTime(2017, 8, 20, 18, 0, 0),
                        Exit = new DateTime(2017, 8, 21, 6, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Night Rate",
                        Price = 6.5
                    }
                },
                
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 0, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 6, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Night Rate",
                        Price = 6.5
                    }
                },

                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 20, 20, 0, 0),
                        Exit = new DateTime(2017, 8, 21, 4, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Night Rate",
                        Price = 6.5
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 18, 23, 30, 0),
                        Exit = new DateTime(2017, 8, 19, 5, 50, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Night Rate",
                        Price = 6.5
                    }
                }
            };

            foreach (var c in cases)
            {
                var result = _calculateService.Calculate(c.TimerData).Result;
                Assert.AreEqual(c.Expected.Name, result.Name);
                Assert.AreEqual(c.Expected.Price, result.Price);
            }
        }

        [TestMethod]
        public void SpecialType_WhenEnterWeekend_ThenReturnWeekendRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 19, 0, 0, 0),
                        Exit = new DateTime(2017, 8, 21, 0, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Weekend Rate",
                        Price = 10
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 19, 8, 0, 0),
                        Exit = new DateTime(2017, 8, 20, 20, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Weekend Rate",
                        Price = 10
                    }
                }
            };

            foreach (var c in cases)
            {
                var result = _calculateService.Calculate(c.TimerData).Result;
                Assert.AreEqual(c.Expected.Name, result.Name);
                Assert.AreEqual(c.Expected.Price, result.Price);
            }
        }

        [TestMethod]
        public void GeneralType_WhenEnter_ThenReturnStandardRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 9, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 10, 0, 0)
                    },
                    Expected = new ParkingRate
                    {
                        Name = "Standard Rate",
                        Price = 5
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 10, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 12, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 10
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 12, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 15, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 15
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 15, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 19, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 20
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 12, 0, 0),
                        Exit = new DateTime(2017, 8, 17, 12, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 40
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer()
                    {
                        Entry = new DateTime(2017, 8, 15, 12, 0, 0),
                        Exit = new DateTime(2017, 8, 17, 15, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 55
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer()
                    {
                        Entry = new DateTime(2017, 8, 15, 12, 0, 0),
                        Exit = new DateTime(2017, 8, 17, 19, 0, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 60
                    }
                },
                new CalculatorCase()
                {
                    TimerData = new ParkingTimer
                    {
                        Entry = new DateTime(2017, 8, 15, 12, 0, 0),
                        Exit = new DateTime(2017, 8, 15, 12, 15, 0)
                    },
                    Expected = new ParkingRate()
                    {
                        Name = "Standard Rate",
                        Price = 5
                    }
                }
            };

            foreach (var c in cases)
            {
                var result = _calculateService.Calculate(c.TimerData).Result;
                Assert.AreEqual(c.Expected.Name, result.Name);
                Assert.AreEqual(c.Expected.Price, result.Price);
            }
        }
    }
}
