using System.Drawing;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using WindowsDisplayAPI.DisplayConfig;
using WindowsDisplayAPI.Native.DisplayConfig;

namespace Services.PathHelper
{
    public class MF_PathHelper_Path
    {
        public MF_PathHelper_Path(PathInfo pathInfo)
        {
            SourceId = pathInfo.DisplaySource.SourceId;
            PixelFormat = pathInfo.PixelFormat;
            Position = pathInfo.Position;
            Resolution = pathInfo.Resolution;
            //Targets = pathInfo.TargetsInfo.Where(t => t.DisplayTarget.IsAvailable == true).Select(target => new MF_PathHelper_Target(target)).ToArray();
            Targets = pathInfo.TargetsInfo.Select(target => new MF_PathHelper_Target(target)).ToArray();
        }

        public MF_PathHelper_Path()
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public DisplayConfigPixelFormat PixelFormat { get; set; }

        public Point Position { get; set; }

        public Size Resolution { get; set; }

        public uint SourceId { get; set; }

        public MF_PathHelper_Target[] Targets { get; set; }


        public PathInfo ToPathInfo()
        {
            var targets = Targets.Select(target => target.ToPathTargetInfo()).Where(info => info != null).ToArray();

            if (targets.Any())
            {
                return new PathInfo(new PathDisplaySource(targets.First().DisplayTarget.Adapter, SourceId), Position,
                    Resolution, PixelFormat, targets);
            }

            return null;
        }

        
    }


}
