using System;

namespace solver_engine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solver engine");

            var request = new SolverReqest()
            {
                VehicleWithOrdersData = new VehicleWithOrders() //hardcoded here, but would normally be from user input above
                {
                    
                    Registration = "LS07OTC",                   
                    Capacity = 1000,
                    Orders = new System.Collections.Generic.List<Order>()
                    {
                        new Order() { Identifier ="Order 1", PickupLocation ="West Kensington", DeliveryLocation = "Uxbridge" },
                        new Order() { Identifier ="Order 2", PickupLocation ="West Kensington", DeliveryLocation = "Hounslow" },
                        new Order() { Identifier ="Order 1", PickupLocation ="West Kensington", DeliveryLocation = "Nottingham" }
                    }
                }
            };

            double cost = 0;
            string name = "";
            string error = "";

            var solverEngine = new SolverEngine();
            solverEngine.GetCost(request, out cost, out name, out error);

            if (cost == -1)
            {
                Console.WriteLine(String.Format("There was an error - {0}", error));
            }
            else
            {
                Console.WriteLine(String.Format("You cost is {0}, from engine: {1}", cost, name));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
