using System;
using System.Windows.Forms;
using PocketLadio.ExtCF.Windows.Forms;

namespace PocketLadio.Utility
{
    /// <summary>
    /// テキストボックスへのクリップボードの処理ユーティリティ
    /// </summary>
    public sealed class ClipboardTextBox
    {
        private ClipboardTextBox()
        {
        }

        public static void Cut(TextBox txtBox)
        {
            if (txtBox != null && txtBox.SelectionLength > 0)
            {
                Clipboard.SetText(txtBox.SelectedText);
                txtBox.SelectedText = "";
            }
        }

        public static void Copy(TextBox txtBox)
        {
            if (txtBox != null && txtBox.SelectionLength > 0)
            {
                Clipboard.SetText(txtBox.SelectedText);
            }
        }

        public static void Paste(TextBox txtBox) {
            string clipboardText = Clipboard.GetText();
            if (txtBox != null && clipboardText != null)
            {
                string Before = txtBox.Text.Substring(0, txtBox.SelectionStart);
                string After = txtBox.Text.Substring(txtBox.SelectionStart + txtBox.SelectionLength, txtBox.TextLength - (txtBox.SelectionStart + txtBox.SelectionLength));
                txtBox.Text = Before + clipboardText + After;
            }
        }
    }
}
