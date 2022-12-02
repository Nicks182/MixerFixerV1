using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlGenerator;
using Services;

namespace Web
{
    public partial class HTML_Templates
    {
        public string _Img_GetSrc(Bitmap P_Image)
        {
            return "data:image/png;base64, " + Srv_Utils._ImageToBase64(P_Image);
        }

        
        public string _CommObjToJSON(Web_InterCommMessage P_CommMessage)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(P_CommMessage);
        }

        private string _GetHtmlId(Guid P_UniqueId)
        {
            return P_UniqueId.ToString().Replace("-", "_");
        }
    }

    public class JavaScriptValueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue((string)value);
        }
    }

}
