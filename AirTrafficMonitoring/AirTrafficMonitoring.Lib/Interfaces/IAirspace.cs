﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public interface IAirSpace
    {
        bool IsInValidAirSpace(ITrack track);
    }
}
