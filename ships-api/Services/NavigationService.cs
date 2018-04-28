using System;
using System.Collections.Generic;

namespace ships_api.Services
{
    struct ShipPosition
    {
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public char Direction;
    }
    public class NavigationService
    {
        private Int16 _gridXMaximum;
        private Int16 _gridYMaximum;

        private List<ShipPosition> _lostShipsLastPositions = new List<ShipPosition>();

        public NavigationService(Int16 gridXMaximum, Int16 gridYMaximum)
        {
            _gridXMaximum = gridXMaximum;
            _gridYMaximum = gridYMaximum;
        }

        public string ProcessShipInstructions(string startingPosition, string navigationalSequence)
        {
            var currentPosition = SplitPositionString(startingPosition);

            var lost = "";

            foreach(char c in navigationalSequence)
            {
                if(c == 'F' &&_lostShipsLastPositions.Contains(currentPosition))
                {
                    continue;
                }

                var newPosition = ApplyTransition(currentPosition, c);
                if(newPosition.XCoordinate < 0 || newPosition.YCoordinate < 0 || newPosition.XCoordinate > _gridXMaximum || newPosition.YCoordinate > _gridYMaximum)
                {
                    lost = " LOST";
                    _lostShipsLastPositions.Add(currentPosition);
                    break;
                }

                currentPosition = newPosition;
            }

            return $"{currentPosition.XCoordinate} {currentPosition.YCoordinate} {currentPosition.Direction}{lost}";
        }

        private ShipPosition ApplyTransition(ShipPosition currentPosition, char navigationalOperation)
        {
            if(navigationalOperation == 'F')
            {
                switch(currentPosition.Direction)
                {
                    case 'N':
                        currentPosition.YCoordinate++;
                        break;
                    case 'E':
                        currentPosition.XCoordinate++;
                        break;
                    case 'S':
                        currentPosition.YCoordinate--;
                        break;
                    case 'W':
                        currentPosition.XCoordinate--;
                        break;                    
                }
            }

            if(navigationalOperation == 'L')
            {
                switch(currentPosition.Direction)
                {
                    case 'N':
                        currentPosition.Direction = 'W';
                        break;
                    case 'E':
                        currentPosition.Direction = 'N';
                        break;
                    case 'S':
                        currentPosition.Direction = 'E';
                        break;
                    case 'W':
                        currentPosition.Direction = 'S';
                        break;                    
                }
            }

            if(navigationalOperation == 'R')
            {
                switch(currentPosition.Direction)
                {
                    case 'N':
                        currentPosition.Direction = 'E';
                        break;
                    case 'E':
                        currentPosition.Direction = 'S';
                        break;
                    case 'S':
                        currentPosition.Direction = 'W';
                        break;
                    case 'W':
                        currentPosition.Direction = 'N';
                        break;                    
                }
            }

            return currentPosition;
        }

        private ShipPosition SplitPositionString(string position)
        {
            // TODO: Expand to handle any whitespace as per spec.
            var parts = position.Split(' '); 
            var shipPosition = new ShipPosition();

            // TODO: Add handling for invalid input
            shipPosition.XCoordinate = Int16.Parse(parts[0]); 
            shipPosition.YCoordinate = Int16.Parse(parts[1]);
            shipPosition.Direction = parts[2][0];

            return shipPosition;
        }
    }
}