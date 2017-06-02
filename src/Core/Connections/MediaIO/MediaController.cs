using System.IO;

namespace SeniorDesign.Core.Connections.MediaIO
{

    /// <summary>
    ///     A connection to an input or output source.
    ///     This does absolutely no decoding or packaging,
    ///     but rather sends and recieves raw data.
    ///     
    ///     These are usually just wrappers for other types of streams
    /// </summary>
    public abstract class MediaController : Stream
    {

        
    }
}
