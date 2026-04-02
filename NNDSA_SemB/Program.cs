using System;
using System.Collections.Generic;
using System.IO;

namespace NNDSA_SemB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ParkingHouse parkingHouse = new ParkingHouse(3, 10);

                parkingHouse.ParkCar(1, 2, "1AB2345");
                parkingHouse.ParkCar(1, 5, "2CD4567");
                parkingHouse.ParkCar(2, 1, "3EF7890");
                parkingHouse.ParkCar(3, 8, "4GH1234");

                string filePath = Path.Combine(Environment.CurrentDirectory, "parkinghouse_data.txt");

                ParkingHouseFileRepository.SaveToFile(parkingHouse, filePath);
                Console.WriteLine($"Data byla uložena do souboru: {filePath}");
                Console.WriteLine();

                ParkingHouse loadedParkingHouse = ParkingHouseFileRepository.LoadFromFile(filePath);

                Console.WriteLine("Načtená data:");
                PrintParkingHouseState(loadedParkingHouse);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Došlo k chybě:");
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Stiskni libovolnou klávesu pro ukončení...");
            Console.ReadKey();
        }

        private static void PrintParkingHouseState(ParkingHouse parkingHouse)
        {
            for (int floorNumber = 1; floorNumber <= parkingHouse.FloorCount; floorNumber++)
            {
                Console.WriteLine($"Patro {floorNumber}:");

                List<KeyValuePair<int, string>> occupiedSpots = parkingHouse.GetOccupiedSpotsInOrder(floorNumber);

                if (occupiedSpots.Count == 0)
                {
                    Console.WriteLine("  Žádná obsazená místa.");
                }
                else
                {
                    foreach (KeyValuePair<int, string> occupiedSpot in occupiedSpots)
                    {
                        Console.WriteLine($"  Místo {occupiedSpot.Key}: {occupiedSpot.Value}");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}