using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Common.Converters
{
    public sealed class BackgroundConverter : IValueConverter
    {
       

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView =
                ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            // Get the index of a ListViewItem
            int index =
                listView.ItemContainerGenerator.IndexFromContainer(item);

            if (index % 2 == 0)
            {
                var color = new UISettings().GetColorValue(UIColorType.Accent);
                return color;
            }
            else
            {
                var color = new UISettings().GetColorValue(UIColorType.AccentDark2);
                return color;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
