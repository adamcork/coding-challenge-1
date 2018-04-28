using System;

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

        public NavigationService(Int16 gridXMaximum, Int16 gridYMaximum)
        {
            _gridXMaximum = gridXMaximum;
            _gridYMaximum = gridYMaximum;
        }

        public string ProcessShipInstructions(string startingPosition, string navigationalSequence)
        {
            var currentPosition = SplitPositionString(startingPosition);
            foreach(char c in navigationalSequence)
            {
                currentPosition = ApplyTransition(currentPosition, c);
            }

            return $"{currentPosition.XCoordinate} {currentPosition.YCoordinate} {currentPosition.Direction}";
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