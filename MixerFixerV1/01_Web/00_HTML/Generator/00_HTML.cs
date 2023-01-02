using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public partial class HTML
    {
        StringBuilder G_HtmlString = new StringBuilder();
        public StringBuilder _BuildHtml(HTML_Object P_HTML_Object)
        {
            G_HtmlString.Clear();
            _GenerateHtml(P_HTML_Object);
            return G_HtmlString;
        }

        public StringBuilder _BuildHtml(List<HTML_Object> P_HTML_Objects)
        {
            G_HtmlString.Clear();
            for (int i = 0; i < P_HTML_Objects.Count; i++)
            {
                _GenerateHtml(P_HTML_Objects[i]);
            }
            return G_HtmlString;
        }

        private void _GenerateHtml(HTML_Object P_HTML_Object)
        {
            // Build object
            // Get open tag?
            _GetTag_Open(P_HTML_Object);

            // Build Children
            for (int i = 0; i < P_HTML_Object.Children.Count; i++)
            {
                _GenerateHtml(P_HTML_Object.Children[i]);
            }

            // Get closing tag
            _GetTag_Close(P_HTML_Object);
            
        }


        private void _GetTag_Open(HTML_Object P_HTML_Object)
        {
            _GetTag(P_HTML_Object, false);
        }

        private void _GetTag_Close(HTML_Object P_HTML_Object)
        {
            _GetTag(P_HTML_Object, true);
        }

        private void _GetTag(HTML_Object P_HTML_Object, bool P_IsClosing)
        {
            switch(P_HTML_Object.Type)
            {
                case HTML_Object_Type.IsDiv:
                    _GetTag_IsDiv(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsImg:
                    _GetTag_IsImg(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsText:
                case HTML_Object_Type.IsSlider:
                case HTML_Object_Type.IsNumber:
                    _GetTag_IsInput(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsTextarea:
                    _GetTag_IsTextarea(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsLabel:
                    _GetTag_IsLabel(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsButton:
                    _GetTag_IsButton(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsIcon:
                    _GetTag_IsIcon(P_HTML_Object, P_IsClosing);
                    break;

                case HTML_Object_Type.IsRaw:
                    _GetTag_IsRaw(P_HTML_Object, P_IsClosing);
                    break;


            }


        }


        
    }

}
