using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NNDSA_SemB
{
    public static class ParkingHouseFileRepository
    {
        public static void SaveToFile(ParkingHouse parkingHouse, string filePath)
        {
            if (parkingHouse == null)
            {
                throw new ArgumentNullException(nameof(parkingHouse));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Cesta k souboru nesmí být prázdná.");
            }

            using StreamWriter streamWriter = new StreamWriter(filePath, false);

            streamWriter.WriteLine($"FloorCount;{parkingHouse.FloorCount}");
            streamWriter.WriteLine($"SpotsPerFloor;{parkingHouse.SpotsPerFloor}");
            streamWriter.WriteLine("OccupiedSpots");

            for (int floorNumber = 1; floorNumber <= parkingHouse.FloorCount; floorNumber++)
            {
                List<KeyValuePair<int, string>> occupiedSpots = parkingHouse.GetOccupiedSpotsInOrder(floorNumber);

                foreach (KeyValuePair<int, string> occupiedSpot in occupiedSpots)
                {
                    streamWriter.WriteLine($"{floorNumber};{occupiedSpot.Key};{occupiedSpot.Value}");
                }
            }
        }

        public static ParkingHouse LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Cesta k souboru nesmí být prázdná.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Soubor neexistuje.", filePath);
            }

            string[] allLines = File.ReadAllLines(filePath);

            if (allLines.Length < 3)
            {
                throw new InvalidDataException("Soubor nemá očekávaný formát.");
            }

            int floorCount = ParseHeaderValue(allLines[0], "FloorCount");
            int spotsPerFloor = ParseHeaderValue(allLines[1], "SpotsPerFloor");

            if (!string.Equals(allLines[2], "OccupiedSpots", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException("Chybí sekce OccupiedSpots.");
            }

            ParkingHouse loadedParkingHouse = new ParkingHouse(floorCount, spotsPerFloor);

            for (int lineIndex = 3; lineIndex < allLines.Length; lineIndex++)
            {
                string currentLine = allLines[lineIndex].Trim();

                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                string[] parts = currentLine.Split(';');

                if (parts.Length != 3)
                {
                    throw new InvalidDataException($"Neplatný formát na řádku {lineIndex + 1}: {currentLine}");
                }

                int floorNumber = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int spotNumber = int.Parse(parts[1], CultureInfo.InvariantCulture);
                string licensePlate = parts[2].Trim();

                bool wasParkedSuccessfully = loadedParkingHouse.ParkCar(floorNumber, spotNumber, licensePlate);

                if (!wasParkedSuccessfully)
                {
                    throw new InvalidDataException(
                        $"Nepodařilo se načíst záznam na řádku {lineIndex + 1}: {currentLine}");
                }
            }

            return loadedParkingHouse;
        }

        private static int ParseHeaderValue(string line, string expectedKey)
        {
            string[] parts = line.Split(';');

            if (parts.Length != 2)
            {
                throw new InvalidDataException($"Neplatná hlavička: {line}");
            }

            if (!string.Equals(parts[0], expectedKey, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException($"Očekávána hlavička {expectedKey}, ale nalezeno: {parts[0]}");
            }

            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedValue))
            {
                throw new InvalidDataException($"Neplatná číselná hodnota v hlavičce: {line}");
            }

            return parsedValue;
        }
    }
}