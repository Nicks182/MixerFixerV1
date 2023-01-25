using System.Drawing;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using WindowsDisplayAPI.DisplayConfig;
using WindowsDisplayAPI.Native.DisplayConfig;

namespace Services.PathHelper
{
    public class MF_PathHelper
    {
        
    }

    public enum Rotation
    {
        Identity,
        Rotate90,
        Rotate180,
        Rotate270,
        Unknown
    }

    public enum Scaling
    {
        NotSpecified,
        Identity,
        Centered,
        Stretched,
        AspectRatioCenteredMax,
        Custom,
        Preferred
    }

    public enum ScanLineOrdering
    {
        NotSpecified,
        Progressive,
        InterlacedWithUpperFieldFirst,
        InterlacedWithLowerFieldFirst
    }

}
