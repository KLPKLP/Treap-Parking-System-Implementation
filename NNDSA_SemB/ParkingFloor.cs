using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDSA_SemB
{
    public class ParkingFloor
    {
        public int TotalSpotCount { get; private set; }

        private Treap<int, string> occupiedSpots; // klíč: číslo parkovacího místa, hodnota: registrační značka vozidla

        public int OccupiedSpotCount => occupiedSpots.Count;

        public int FreeSpotCount => TotalSpotCount - OccupiedSpotCount;

        public ParkingFloor(int totalSpotCount)
        {
            if (totalSpotCount <= 0)
            {
                throw new ArgumentException("Počet parkovacích míst musí být kladný");
            }
            TotalSpotCount = totalSpotCount;
            occupiedSpots = new Treap<int, string>();
        }

        private bool IsValidSpotNumber(int spotNumber)
        {
            return spotNumber >= 1 && spotNumber <= TotalSpotCount;
        }

        public bool ParkCar(int spotNumber, string licensePlate)
        {
            if (!IsValidSpotNumber(spotNumber) || string.IsNullOrEmpty(licensePlate))
            {
                return false;
            }

            return occupiedSpots.Insert(spotNumber, licensePlate);
        }

        public bool RemoveCar(int spotNumber)
        {
            if (!IsValidSpotNumber(spotNumber))
            {
                return false;
            }
            return occupiedSpots.Remove(spotNumber);
        }

        public bool TryGetLicensePlate(int spotNumber, out string licensePlate)
        {
            licensePlate = null;
            if (!IsValidSpotNumber(spotNumber))
            {
                return false;
            }
            return occupiedSpots.TryGetValue(spotNumber, out licensePlate);
        }

        public List<KeyValuePair<int, string>> GetOccupiedSpotsInOrder()
        {
            return occupiedSpots.TraverseInOrder();
        }

        public void ClearFloor()
        {
            occupiedSpots.Clear();
        }

        public bool TryFindNearestFreeSpot(int requestedSpotNumber, out int nearestFreeSpotNumber)
        {
            nearestFreeSpotNumber = -1;

            if (!IsValidSpotNumber(requestedSpotNumber))
            {
                return false;
            }

            if (!occupiedSpots.ContainsKey(requestedSpotNumber))
            {
                nearestFreeSpotNumber = requestedSpotNumber;
                return true;
            }

            int leftBounary = requestedSpotNumber;
            int rightBoundary = requestedSpotNumber;

            KeyValuePair<int, string> predecessor; 
            KeyValuePair<int, string> successor;

            // Posouvám levou hranici bloku doleva, dokud nacházím bezprostředně navazující obsazené místo)
            while (occupiedSpots.TryGetPredecessor(leftBounary, out predecessor) && predecessor.Key == leftBounary - 1) // -1 protože hledám bezprostředně navazující místo
            {
                leftBounary = predecessor.Key;

            }

            while (occupiedSpots.TryGetSuccessor(rightBoundary, out successor) && successor.Key == rightBoundary + 1)
            {
                rightBoundary = successor.Key;

            }

            int leftCandidate = leftBounary - 1; //Kandidát vlevo je první místo před levým okrajem souvislého bloku
            int rightCandidate = rightBoundary + 1; //Kandidát vpravo je první místo za pravým okrajem souvislého bloku

            bool hasLeftCandidate = IsValidSpotNumber(leftCandidate);
            bool hasRightCandidate = IsValidSpotNumber(rightCandidate);

            if (!hasLeftCandidate && !hasRightCandidate)
            {
                return false;
            }

            if (hasLeftCandidate && !hasRightCandidate)
            {
                nearestFreeSpotNumber = leftCandidate;
                return true;
            }

            if (!hasLeftCandidate && hasRightCandidate)
            {
                nearestFreeSpotNumber = rightCandidate;
                return true;
            }

            int leftDistance = requestedSpotNumber - leftCandidate;
            int rightDistance = rightCandidate - requestedSpotNumber;

            if (leftDistance <= rightDistance)
            {
                nearestFreeSpotNumber = leftCandidate;
            }
            else
            {
                nearestFreeSpotNumber = rightCandidate;
            }
            return true;


        }

        public bool IsSpotOccupied(int spotNumber)
        {
            if (!IsValidSpotNumber(spotNumber))
            {
                return false;
            }
            return occupiedSpots.ContainsKey(spotNumber);
        }

        public bool IsFull()
        {
            return FreeSpotCount == 0;
        }

        
    }
}
