using System;

namespace ships_api.Services
{
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
            throw new NotImplementedException();
        }
    }
}