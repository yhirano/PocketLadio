using System;
using System.Windows.Forms;
using PocketLadio.ExtCF.Windows.Forms;

namespace PocketLadio.Util
{
    /// <summary>
    /// テキストボックスへのクリップボードの処理ユーティリティ
    /// </summary>
    public class ClipboardTextBox
    {
        private ClipboardTextBox()
        {
        }

        public static void Cut(TextBox textBox)
        {
            if (textBox.SelectionLength > 0)
            {
                Clipboard.SetText(textBox.SelectedText);
                textBox.SelectedText = "";
            }
        }

        public static void Copy(TextBox textBox)
        {
            if (textBox.SelectionLength > 0)
            {
                Clipboard.SetText(textBox.SelectedText);
            }
        }

        public static void Paste(TextBox textBox) {
            string ClipboardText = Clipboard.GetText();
            if (ClipboardText != null)
            {
                string Before = textBox.Text.Substring(0, textBox.SelectionStart);
                string After = textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength, textBox.TextLength - (textBox.SelectionStart + textBox.SelectionLength));
                textBox.Text = Before + ClipboardText + After;
            }
        }
    }
}
