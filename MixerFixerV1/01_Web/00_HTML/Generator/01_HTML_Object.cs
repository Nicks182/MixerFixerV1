using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public class HTML_Object
    {
        public HTML_Object()
        {
            Children = new List<HTML_Object>();
            Attributes = new List<HTML_Object_Attribute>();
        }

        public StringBuilder RawValue { get; set; }
        public HTML_Object_Type Type { get; set; }
        public List<HTML_Object_Attribute> Attributes { get; set; }
        public List<HTML_Object> Children { get; set; }

        /// <summary>
        /// <para>Add HTML attributes like 'class', 'onclick', 'value', ect along with it's value.</para>
        /// <para>If internal List already contains an item with same name, vallue will be appended to internal StringBuilder along with a space to separate values.</para>
        /// </summary>
        public void Add_Attribute(string P_Name, string P_Value)
        {
            HTML_Object_Attribute L_HTML_Object_Attribute = Attributes.Where(a => a.Name.Equals(P_Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (L_HTML_Object_Attribute != null)
            {
                L_HTML_Object_Attribute.Value.Append(' ');
                L_HTML_Object_Attribute.Value.Append(P_Value);
            }
            else
            {
                Attributes.Add(new HTML_Object_Attribute
                {
                    Name = P_Name,
                    Value = new StringBuilder(P_Value)
                });
            }
        }

        public void Add_Child(HTML_Object P_HTML_Object)
        {
            Children.Add(P_HTML_Object);
        }
    }


    public enum HTML_Object_Type
    {
        IsRaw,
        IsDiv,
        IsImg,
        IsText,
        IsTextarea,
        IsSlider,
        IsCheck,
        IsLabel,
        IsButton,
        IsIcon,
    }


    public enum HTML_Object_Icon_Pos
    {
        None,
        IsLeft,
        IsRight,
    }

    public class HTML_Object_Attribute
    {
        //public HTML_Object_Attribute()
        //{
        //    Value = new StringBuilder();
        //}
        public string Name { get; set; }
        public StringBuilder Value { get; set; }
    }


    public enum HTML_Object_Icon
    {
        None,
        settings,
        
    }

}
