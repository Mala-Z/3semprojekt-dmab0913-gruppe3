using System;
using System.Linq;
using System.Data.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Dijkstra;
using DatabaseLayer;

namespace UnitTest
{
    [TestClass]
    public class DijkstraRouteEngineTests
    {

        [TestMethod]
        public void Calculate_A_to_D_given_AB_BC_CD__should_be__ABCD()
        {
            var Results = Engine.CalculateShortestPathBetween(
                "A",
                "D",
                new Path<string>[] {
                new Path<string>() { Source = "A", Destination = "B", Cost = 3 },
                new Path<string>() { Source = "B", Destination = "C", Cost = 3 },
                new Path<string>() { Source = "C", Destination = "D", Cost = 3 }
            });

            Results.Sum(r => r.Cost).Should().Be(9);
            Results.Count.Should().Be(3);

            Results.First().Cost.Should().Be(3);
            Results.First().Source.Should().Be("A");
            Results.First().Destination.Should().Be("B");

            Results.Skip(1).First().Cost.Should().Be(3);
            Results.Skip(1).First().Source.Should().Be("B");
            Results.Skip(1).First().Destination.Should().Be("C");

            Results.Skip(2).First().Cost.Should().Be(3);
            Results.Skip(2).First().Source.Should().Be("C");
            Results.Skip(2).First().Destination.Should().Be("D");
        }

        [TestMethod]
        public void Calculate_A_to_D_given_AB_BC_CD_DE__should_be__ABCD()
        {
            var Results = Engine.CalculateShortestPathBetween(
                "A",
                "D",
                new Path<string>[] {
                new Path<string>() { Source = "A", Destination = "B", Cost = 3 },
                new Path<string>() { Source = "B", Destination = "C", Cost = 3 },
                new Path<string>() { Source = "C", Destination = "D", Cost = 3 },
                new Path<string>() { Source = "D", Destination = "E", Cost = 3 }
            });

            Results.Sum(r => r.Cost).Should().Be(9);
            Results.Count.Should().Be(3);

            Results.First().Cost.Should().Be(3);
            Results.First().Source.Should().Be("A");
            Results.First().Destination.Should().Be("B");

            Results.Skip(1).First().Cost.Should().Be(3);
            Results.Skip(1).First().Source.Should().Be("B");
            Results.Skip(1).First().Destination.Should().Be("C");

            Results.Skip(2).First().Cost.Should().Be(3);
            Results.Skip(2).First().Source.Should().Be("C");
            Results.Skip(2).First().Destination.Should().Be("D");
        }

        [TestMethod]
        public void Calculate_A_to_D_given_AB_AC_AD_AE_BC_CD_DE__should_be__ACD()
        {
            var Results = Engine.CalculateShortestPathBetween(
                "A",
                "D",
                new Path<string>[] {
                new Path<string>() { Source = "A", Destination = "B", Cost = 3 },
                new Path<string>() { Source = "A", Destination = "C", Cost = 3 },
                new Path<string>() { Source = "A", Destination = "D", Cost = 7 }, // set this just above ABC (3+3=6)
                new Path<string>() { Source = "A", Destination = "E", Cost = 3 },
 
                new Path<string>() { Source = "B", Destination = "C", Cost = 3 },
 
                new Path<string>() { Source = "C", Destination = "D", Cost = 3 },
 
                new Path<string>() { Source = "D", Destination = "E", Cost = 3 }
            });

            Results.Sum(r => r.Cost).Should().Be(6);
            Results.Count.Should().Be(2);

            Results.First().Cost.Should().Be(3);
            Results.First().Source.Should().Be("A");
            Results.First().Destination.Should().Be("C");

            Results.Skip(1).First().Cost.Should().Be(3);
            Results.Skip(1).First().Source.Should().Be("C");
            Results.Skip(1).First().Destination.Should().Be("D");
        }

        [TestMethod]
        public void Calculate_A_to_D_given_AB_AC_AD_AE_BC_CD_DE__should_be__AD()
        {
            var Results = Engine.CalculateShortestPathBetween(
                "A",
                "D",
                new Path<string>[] {
                new Path<string>() { Source = "A", Destination = "B", Cost = 3 },
                new Path<string>() { Source = "A", Destination = "C", Cost = 3 },
                new Path<string>() { Source = "A", Destination = "D", Cost = 5 }, // set this just below ABC (3+3=6)
                new Path<string>() { Source = "A", Destination = "E", Cost = 3 },
 
                new Path<string>() { Source = "B", Destination = "C", Cost = 3 },
 
                new Path<string>() { Source = "C", Destination = "D", Cost = 3 },
 
                new Path<string>() { Source = "D", Destination = "E", Cost = 3 }
            });

            Results.Sum(r => r.Cost).Should().Be(5);
            Results.Count.Should().Be(1);

            Results.Single().Cost.Should().Be(5);
            Results.Single().Source.Should().Be("A");
            Results.Single().Destination.Should().Be("D");
        }

        [TestMethod]
        public void Calculate_airport()
        {
            var db = DBConnection.GetInstance().GetConnection();
            var airports = db.Airports.OrderBy(x => x.airportID).ToDictionary(x => x.name);

            var results = Engine.CalculateShortestPathBetween(
                airports["AAL"],
                airports["FCO"],
                new Path<Airport>[] 
                {
                    new Path<Airport>() { Source = airports["AAL"], Destination = airports["BLL"], Cost = 1 },
                    new Path<Airport>() { Source = airports["BLL"], Destination = airports["FCO"], Cost = 11 },
                    new Path<Airport>() { Source = airports["AAL"], Destination = airports["CPH"], Cost = 2 },
                    new Path<Airport>() { Source = airports["CPH"], Destination = airports["LHR"], Cost = 3 },
                    new Path<Airport>() { Source = airports["LHR"], Destination = airports["DCA"], Cost = 1 },
                    new Path<Airport>() { Source = airports["DCA"], Destination = airports["FCO"], Cost = 2 }
                });

            Assert.AreEqual(8, results.Sum(p => p.Cost));
            //var test = airports["AAL"].GetType().GetProperty("name");
        }

        [TestMethod]
        public void Test_flight_to_path()
        {
            //var db = DBConnection.GetInstance().GetConnection();
            //var asd = from f in db.Flights where f.
            //Path<Flight>[] p = db.Flights.ToArray(false => )
        }


    } // TestClass
}
