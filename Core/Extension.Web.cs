using System.Web.UI.WebControls;

namespace Ventus
{
    partial class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="list"></param>
        /// <param name="display"></param>
        /// <param name="value"></param>
        public static DropDownList Fill(this DropDownList cbo, object list, string display = "Nombre", string value = "ID")
        {
            cbo.DataTextField = display;
            cbo.DataValueField = value;
            cbo.DataSource = list;
            cbo.DataBind();
            return cbo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DropDownList InsertBlank(this DropDownList cbo, string text)
        {
            cbo.Items.Insert(0, new ListItem(text, "0"));
            return cbo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbo"></param>
        /// <returns></returns>
        public static DropDownList InsertBlank(this DropDownList cbo)
        {
            return cbo.InsertBlank(string.Empty);
        }
    }
}
