﻿using System;
using System.Collections.Generic;
using TransponderReceiver;


namespace AirTrafficMonitoring.Lib
{
    // Track Objectification Software
    public class Tos : Subject<Tos>
    {
        public List<ITrack> RecievedTracks { get; set; } = new List<ITrack>();

        public Tos(ITransponderReceiver iTransponderReceiver)
        {
            iTransponderReceiver.TransponderDataReady += (sender, args) =>
            {
                RecievedTracks.Clear();
                foreach (var line in args.TransponderData)
                    if(CreateTrackObject(line) != null)
                        RecievedTracks.Add(CreateTrackObject(line));
                Notify(this);                
            };
        }

        private ITrack CreateTrackObject(string line)
        {
            var rawData = line.Split(';');
            if (rawData.Length != 5) return null;
            var timeStamp = rawData[4];
            return new Track
            {
                Tag = rawData[0],
                PositionX = int.Parse(rawData[1]),
                PositionY = int.Parse(rawData[2]),
                Altitude = int.Parse(rawData[3]),
                Timestamp = new DateTime(
                    int.Parse(timeStamp.Substring(0, 4)),
                    int.Parse(timeStamp.Substring(4, 2)),
                    int.Parse(timeStamp.Substring(6, 2)),
                    int.Parse(timeStamp.Substring(8, 2)),
                    int.Parse(timeStamp.Substring(10, 2)),
                    int.Parse(timeStamp.Substring(12, 2)),
                    int.Parse(timeStamp.Substring(14, 3))
                )
            };
        }
    }
}
