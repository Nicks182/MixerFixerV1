using System;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using WindowsDisplayAPI.DisplayConfig;

namespace Services.PathHelper
{
    public class MF_PathHelper_Target
    {
        public MF_PathHelper_Target(PathTargetInfo targetInfo)
        {
            DevicePath = targetInfo.DisplayTarget.DevicePath;
            var index = DevicePath.IndexOf("{", StringComparison.InvariantCultureIgnoreCase);

            if (index > 0)
            {
                DevicePath = DevicePath.Substring(0, index).TrimEnd('#');
            }

            FrequencyInMillihertz = targetInfo.FrequencyInMillihertz;
            Rotation = targetInfo.Rotation.ToRotation();
            Scaling = targetInfo.Scaling.ToScaling();
            ScanLineOrdering = targetInfo.ScanLineOrdering.ToScanLineOrdering();

            try
            {
                DisplayName = targetInfo.DisplayTarget.FriendlyName;
            }
            catch
            {
                DisplayName = null;
            }

        }

        public MF_PathHelper_Target()
        {

        }

        public string DisplayName { get; set; } = "NA";
        public string DevicePath { get; set; } = "NA";

        public ulong FrequencyInMillihertz { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Rotation Rotation { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Scaling Scaling { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ScanLineOrdering ScanLineOrdering { get; set; }


        public PathTargetInfo ToPathTargetInfo()
        {
            var targetDevice =
                PathDisplayTarget.GetDisplayTargets()
                    .FirstOrDefault(
                        target => target.DevicePath.StartsWith(DevicePath,
                            StringComparison.InvariantCultureIgnoreCase));

            if (targetDevice == null)
            {
                return null;
            }

            return new PathTargetInfo(new PathDisplayTarget(targetDevice.Adapter, targetDevice.TargetId),
                FrequencyInMillihertz, ScanLineOrdering.ToDisplayConfigScanLineOrdering(),
                Rotation.ToDisplayConfigRotation(), Scaling.ToDisplayConfigScaling());
        }

    }

}
