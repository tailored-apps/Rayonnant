using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wise.DummyModule
{
    public class CustomDropDown : ComboBox
    {
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (Text != null && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                var textBox = e.OriginalSource as TextBox;
                if (!string.IsNullOrEmpty(textBox.SelectedText) && textBox.Text.Replace(textBox.SelectedText, "").Length == 0)
                {
                    SelectedIndex = -1;
                }
                else if (string.IsNullOrEmpty(textBox.SelectedText) && (string.IsNullOrEmpty(textBox.Text) || textBox.Text.Length == 1))
                {
                    SelectedIndex = -1;

                }
            }
        }
    }
}
