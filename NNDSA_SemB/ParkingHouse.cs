using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDSA_SemB
{
    public class ParkingHouse
    {
        private List<ParkingFloor> floors;
        public int FloorCount => floors.Count;
        public int SpotsPerFloor { get; private set; }
        public ParkingHouse(int floorCount, int spotsPerFloor)
        {
            if (floorCount <= 0 || spotsPerFloor <= 0)
            {
                throw new ArgumentException("Počet pater a počet míst na patře musí být kladný");
            }
            floors = new List<ParkingFloor>();
            SpotsPerFloor = spotsPerFloor;
            for (int i = 0; i < floorCount; i++)
            {
                floors.Add(new ParkingFloor(spotsPerFloor));
            }
        }


        private bool IsValidFloorNumber(int floorNumber)
        {
            return floorNumber >= 1 && floorNumber <= FloorCount;
        }


        public bool ParkCar(int floorNumber, int spotNumber, string licensePlate)
        {
            if (!IsValidFloorNumber(floorNumber))
            {
                return false;
            }
            return floors[floorNumber - 1].ParkCar(spotNumber, licensePlate);
        }

        public bool RemoveCar(int floorNumber, int spotNumber)
        {
            if (!IsValidFloorNumber(floorNumber))
            {
                return false;
            }
            return floors[floorNumber - 1].RemoveCar(spotNumber);
        }

        public bool IsSpotOccupied(int floorNumber, int spotNumber)
        {
            if (!IsValidFloorNumber(floorNumber))
            {
                return false;
            }
            return floors[floorNumber - 1].IsSpotOccupied(spotNumber);
        }

        public bool TryGetLicensePlate(int floorNumber, int spotNumber, out string licensePlate)
        {
            licensePlate = null;
            if (!IsValidFloorNumber(floorNumber))
            {
                return false;
            }
            return floors[floorNumber - 1].TryGetLicensePlate(spotNumber, out licensePlate);
        }

        public List<KeyValuePair<int, string>> GetOccupiedSpotsInOrder(int floorNumber)
        {
            if (!IsValidFloorNumber(floorNumber))
            {
                return new List<KeyValuePair<int, string>>();
            }
            return floors[floorNumber - 1].GetOccupiedSpotsInOrder();
        }

        public bool TryFindNearestFreeSpot(int floorNumber, int requestedSpotNumber, out int nearestFreeSpot)
        {
            nearestFreeSpot = -1;
            if (!IsValidFloorNumber(floorNumber))
            {
                return false;
            }
            return floors[floorNumber - 1].TryFindNearestFreeSpot(requestedSpotNumber, out nearestFreeSpot);
        }

        public ParkingFloor GetFloor(int floorNumber)
        {
            if (!IsValidFloorNumber(floorNumber))
            {
                throw new ArgumentException("Neplatné číslo patra");
            }
            return floors[floorNumber - 1];
        }

        public void ClearAllFloors()
        {
            foreach (ParkingFloor floor in floors)
            {
                floor.ClearFloor();
            }
        }
    }
}
