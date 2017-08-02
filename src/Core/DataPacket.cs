using System.Collections.Generic;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     A single packet of numeric data. Includes all channels
    /// </summary>
    public class DataPacket
    {

        /// <summary>
        ///     The collection of actual data
        /// </summary>
        private List<List<double>> _data = new List<List<double>>();

        /// <summary>
        ///     Creates a new, empty DataPacket
        /// </summary>
        public DataPacket() { }

        /// <summary>
        ///     Creates a new DataPacket with given data
        /// </summary>
        /// <param name="data">The preliminary data</param>
        public DataPacket(double[][] data)
        {
            // Add all of the channels to the data
            for (var k = 0; k < data.Length; k++)
                _data.Add(new List<double>(data[k]));
        }

        /// <summary>
        ///     Creates a new DataPacket with given data
        /// </summary>
        /// <param name="data">The preliminary data</param>
        public DataPacket(List<List<double>> data)
        {
            // Add all of the channels to the data
            for (var k = 0; k < data.Count; k++)
                _data.Add(new List<double>(data[k]));
        }

        /// <summary>
        ///     Clones an existing DataPacket
        /// </summary>
        /// <param name="data">The data packet to clone</param>
        public DataPacket(DataPacket data)
        {
            // Add all of the channels to the data
            for (var k = 0; k < data.ChannelCount; k++)
                _data.Add(new List<double>(data[k]));
        }

        /// <summary>
        ///     Index accessor for a given channel, and data point
        /// </summary>
        /// <param name="channel">The data channel to get</param>
        /// <param name="point">The point in the data channel to get</param>
        /// <returns>The value of the data at the given sample on the given channel</returns>
        public double this[int channel, int point] {
            get
            {
                return _data[channel][point];
            }
            set
            {
                _data[channel][point] = value;
            }
        }

        /// <summary>
        ///     Index accessor for a given channel
        /// </summary>
        /// <param name="channel">The data channel to get</param>
        /// <returns>The contents of the data channel</returns>
        public List<double> this[int channel]
        {
            get
            {
                return _data[channel];
            }
            set
            {
                _data[channel] = value;
            }
        }

        /// <summary>
        ///     The number of channels available
        /// </summary>
        public int ChannelCount { get { return _data.Count; } }

        /// <summary>
        ///     The number of points available in a given channel
        /// </summary>
        /// <param name="id">The ID of the channel to get</param>
        /// <returns>The number of points in the channel</returns>
        public int PointCount(int id) { return _data[id].Count; }

        /// <summary>
        ///     Adds any number of points to a given channel
        /// </summary>
        /// <param name="channel">The channel to add the data to</param>
        /// <param name="data">The data to add</param>
        public void AddPoints(int channel, params double[] data)
        {
            _data[channel].AddRange(data);
        }

        /// <summary>
        ///     Adds any number of points to a given channel.
        ///     Be cautious of using this, as order may not be garunteed depending on data's type
        /// </summary>
        /// <param name="channel">The channel to add the data to</param>
        /// <param name="data">The data to add</param>
        public void AddPoints(int channel, IEnumerable<double> data)
        {
            _data[channel].AddRange(data);
        }

        /// <summary>
        ///     Adds a new channel to the data set
        /// </summary>
        /// <param name="data">The data the channel starts with</param>
        /// <returns>The number of the channel added</returns>
        public int AddChannel(params double[] data)
        {
            var list = new List<double>();
            _data.Add(list);
            list.AddRange(data);
            return _data.IndexOf(list);
        }

        /// <summary>
        ///     Adds a new channel to the data set.
        ///     Be cautious of using this, as order may not be garunteed depending on data's type
        /// </summary>
        /// <param name="data">The data the channel starts with</param>
        /// <returns>The number of the channel added</returns>
        public int AddChannel(IEnumerable<double> data)
        {
            var list = new List<double>();
            _data.Add(list);
            list.AddRange(data);
            return _data.IndexOf(list);
        }

        /// <summary>
        ///     Merges a data packet with this one, adding
        ///     the channels from the packet to the end.
        /// </summary>
        /// <param name="data">The data packet to take the channels from</param>
        public void Merge(DataPacket data)
        {
            // TODO : There may be a bug by just taking references...
            for (var k = 0; k < data.ChannelCount; k++)
                _data.Add(data[k]);
        }

        /// <summary>
        ///     Adds values to a specific channel, creating it
        ///     if it doesn't exist
        /// </summary>
        public void AddToChannel(int channel, double data)
        {
            if (_data.Count <= channel)
                AddChannel(data);
            else
                _data[channel].Add(data);
        }

        /// <summary>
        ///     Adds values to a specific channel, creating it
        ///     if it doesn't exist
        /// </summary>
        /// <returns>The new current index</returns>
        public void AddToChannel(int channel, DataPacket data)
        {
            if (_data.Count <= channel)
                AddChannel(data[0]);
            else
                _data[channel].AddRange(data[0]);
        }

        /// <summary>
        ///     Removes all of the empty channels.
        ///     Starts from the end, and stops if one is not empty.
        /// </summary>
        public void RemoveEmptyChannels()
        {
            for (var k = _data.Count - 1; k >= 0; k--)
            {
                if (_data[k].Count <= 0)
                    _data.RemoveAt(k);
                else
                    return;
            }
        }

        /// <summary>
        ///     Ensures that a specific number of channels exist.
        ///     Returns false if over the count
        /// </summary>
        /// <param name="count">The number of channels to ensure</param>
        /// <returns>True if the amount was reached, false if went over</returns>
        public bool EnsureChannelCount(int count)
        {
            if (_data.Count <= count)
                while (_data.Count < count)
                    _data.Add(new List<double>());
            else
                return false;
            return true;
        }

        /// <summary>
        ///     Removes the specified channel from the data set
        /// </summary>
        /// <param name="channel">The index of the channel to remove</param>
        public void RemoveChannel(int channel)
        {
            _data.RemoveAt(channel);
        }

        /// <summary>
        ///     Adds data from another data packet to this one
        /// </summary>
        /// <param name="packet">The packet to add data from</param>
        /// <param name="allowCreateChannel">If new channels can be created</param>
        public void Add(DataPacket packet, bool allowCreateChannel = true)
        {
            // Ignore if empty
            if (packet == null)
                return;

            // Don't allow channel creation normally
            if (packet.ChannelCount != ChannelCount && !allowCreateChannel)
                throw new System.Exception("Data Packets with different channel counts attempted to merge when forbidden to do so.");

            // Loop until something runs out
            var itrA = packet.ChannelCount < ChannelCount ? packet.ChannelCount : ChannelCount;
            for (var k = 0; k < itrA; k++)
                _data[k].AddRange(packet[k]);

            // Only attach new channels if needed
            if (itrA < packet.ChannelCount)
                for (var k = itrA; k < packet.ChannelCount; k++)
                    AddChannel(packet[k]);
        }

        /// <summary>
        ///     Clears all data from the packet
        /// </summary>
        public void Clear()
        {
            _data.Clear();
        }

        /// <summary>
        ///     Gets a single point from the packet.
        ///     The point retrieved will be removed
        /// </summary>
        /// <param name="channel">The channel number to pop fom</param>
        /// <returns>The data requested</returns>
        public double Pop(int channel)
        {
            var toReturn = _data[channel][0];
            _data[channel].RemoveAt(0);
            return toReturn;
        }

        /// <summary>
        ///     Gets a specified number of points from the packet.
        ///     Positive numbers mean from the beginning.
        ///     Negative numbers mean from the end.
        ///     If not enough points exist, what does exist will return.
        ///     The points retrieved will be removed
        /// </summary>
        /// <param name="channel">The channel number to pop from</param>
        /// <param name="count">The number of points to retrieve</param>
        /// <returns>The data requested</returns>
        public List<double> PopRange(int channel, int count)
        {
            var toReturn = new List<double>();
            var rcount = count;

            // Begin popping off the required values
            if (count > 0)
            {
                // Normal method. Get everything up front
                rcount = count < toReturn.Count ? toReturn.Count : count;
                for (var k = 0; k < rcount; k++)
                    toReturn.Add(_data[channel][k]);
                _data.RemoveRange(0, rcount);
            }
            else
            {
                // Reverse method. Take from the back
                rcount = toReturn.Count + count > 0 ? toReturn.Count + count : 0;
                for (var k = rcount; k < toReturn.Count; k++)
                    toReturn.Add(_data[channel][k]);
                _data.RemoveRange(rcount, toReturn.Count - rcount);
            }

            return toReturn;
        }

        /// <summary>
        ///     Gets a specified number of points from the packet.
        ///     Positive numbers mean from the beginning.
        ///     Negative numbers mean from the end.
        ///     If not enough points exist, what does exist will return.
        /// </summary>
        /// <param name="channel">The channel number to pop from</param>
        /// <param name="count">The number of points to retrieve</param>
        /// <returns>The data requested</returns>
        public List<double> PeekRange(int channel, int count)
        {
            var toReturn = new List<double>();
            var rcount = count;

            // Begin popping off the required values
            if (count > 0)
            {
                // Normal method. Get everything up front
                rcount = count < toReturn.Count ? toReturn.Count : count;
                for (var k = 0; k < rcount; k++)
                    toReturn.Add(_data[channel][k]);
            }
            else
            {
                // Reverse method. Take from the back
                rcount = toReturn.Count + count > 0 ? toReturn.Count + count : 0;
                for (var k = rcount; k < toReturn.Count; k++)
                    toReturn.Add(_data[channel][k]);
            }

            return toReturn;
        }

        /// <summary>
        ///     Pops a sub-packet with a specified number of points.
        ///     This will have the same number of channels
        /// </summary>
        /// <param name="count">The number of points to pop</param>
        /// <returns>A new Collection of doubles with the specified range</returns>
        public DataPacket PopSubPacket(int count)
        {
            var toReturn = new DataPacket();
            for (var k = 0; k < _data.Count; k++)
            {
                toReturn.AddChannel();
                toReturn.AddChannel(PopRange(k, count));
            }
            return toReturn;
        }

        /// <summary>
        ///     Checks to see if all channels have at least min points
        /// </summary>
        /// <param name="min">The minimum to check for</param>
        /// <returns>True if the count exists on all channels</returns>
        public bool MinCountOnAllChannels(int min = 1)
        {
            foreach (var channel in _data)
                if (channel.Count < min)
                    return false;
            return true;
        }

        /// <summary>
        ///     Ensures that a minimum of at least min points exists on every defined channel
        /// </summary>
        /// <param name="min">The minimum to enforce</param>
        /// <returns>False if all data channels are empty, and nothing was added</returns>
        public bool EnsureMinCountOnAllChannels(int min = 1, double def = 0)
        {
            int badCount = 0;
            foreach (var channel in _data)
                if (channel.Count < min)
                    badCount++;

            if (badCount >= _data.Count)
                return false;


            foreach (var channel in _data)
                while (channel.Count < min)
                    channel.Add(def);
            return true;
        }

    }
}
