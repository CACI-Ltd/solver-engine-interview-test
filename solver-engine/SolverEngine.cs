using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solver_engine
{
    public class SolverReqest
    {
        public VehicleWithOrders VehicleWithOrdersData { get; set; }
    }

    public class VehicleWithOrders
    {
        public string Registration { get; set; }
        public decimal Capacity { get; set; }
        public string Make { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public string Identifier { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
    }
    public class SolverEngine1
    {
        public SolverEngine1(string url, string port)
        {

        }

        public dynamic GetCost(dynamic request)
        {
            //makes a call to an engine service1            

            dynamic response = new ExpandoObject();
            response.Cost = 120;
            response.IsSuccess = true;

            return response;
        }
    }

    public class SolverEngine2
    {
        public SolverEngine2(string url, string port, dynamic request)
        {

        }

        public dynamic GetCost()
        {
            //makes a call to solver engine  service1
         
            dynamic response = new ExpandoObject();
            response.Cost = 234.56;            
            response.Name = "engine2";
            response.IsSuccess = true;


            return response;
        }
    }    

    public class SolverEngine
    {
        //pass request with vehiclewithorders return the best cost retrieved from 2 different types of  engines
        public double GetCost(SolverReqest request, out double cost, out string solverEngineName, out string errorMessage)
        {
            //initialise return variables
            cost = 0;
            solverEngineName = "";
            errorMessage = "";

            //validation
            if (request.VehicleWithOrdersData == null)
            {
                errorMessage = "Data is missing";
                return -1;
            }

            if (String.IsNullOrEmpty(request.VehicleWithOrdersData.Registration))
            {
                errorMessage = "Registration is required";
                return -1;
            }

            if (request.VehicleWithOrdersData.Orders == null)
            {
                errorMessage = "Orders are required";
                return -1;
            }

           


            //now call 3 external system and get the best cost
            cost = 0;

            //solver engine 1 requires Make to be specified
            if (request.VehicleWithOrdersData.Make != null)
            {
                SolverEngine1 system1 = new SolverEngine1("http://solver-system-1.com", "1234");

                dynamic request1 = new ExpandoObject();
                request1.Registration = request.VehicleWithOrdersData.Registration;
                request1.Capacity = request.VehicleWithOrdersData.Capacity;
               
                request1.Make = request.VehicleWithOrdersData.Make;
                request1.Orders = request.VehicleWithOrdersData.Orders;

                dynamic system1Response = system1.GetCost(request1);
                if (system1Response.IsSuccess)
                {
                    cost = system1Response.Cost;
                    solverEngineName = system1Response.Name;                   
                }
            }

            //engine 2 is always called
           
                dynamic request2 = new ExpandoObject();
                request2.Registration = request.VehicleWithOrdersData.Registration;
                request2.Capacity = request.VehicleWithOrdersData.Capacity;

                request2.Make = request.VehicleWithOrdersData.Make;
                request2.Orders = request.VehicleWithOrdersData.Orders;

                SolverEngine2 system2 = new SolverEngine2("http://solver-system-2.com", "1234", request2 );

               

                dynamic system2Response = system2.GetCost();
                if (system2Response.IsSuccess)
                {
                    cost = system2Response.Cost;
                    solverEngineName = system2Response.Name;
                }
                   
          

            if (cost == 0)
            {
                cost = -1;
            }

            return cost;
        }
    }
}
