using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextEditor_Lab5
{
    public partial class formTextEditor : Form
    {
        string openFile = String.Empty;
        public formTextEditor()
        {
            InitializeComponent();
        }
        #region "Event Handlers"
        private void SaveClick(object sender, EventArgs e)
        {
            //if openFile (the file that is currently open) has no value,
            //call "Save As" instead!
            if (openFile == String.Empty)
            {
                //SaveAsClick();
            }
            //If openFile has a value, just call the SaveFile() function!
            else if (openFile != String.Empty)
            {
                SaveFile(openFile);
            }
        }
        /// <summary>
        /// TODO: Add description
        /// </summary>
        private void SaveAsClick(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (*.alf)|*.alf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                openFile = saveDialog.FileName;

                SaveFile(openFile);
            }
        }
        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void NewClick(object sender, EventArgs e)
        {

        }

        private void OpenClick(object sender, EventArgs e)
        {

        }

        private void CutClick(object sender, EventArgs e)
        {

        }

        private void CopyClick(object sender, EventArgs e)
        {

        }

        private void PasteClick(object sender, EventArgs e)
        {

        }

        private void AboutClick(object sender, EventArgs e)
        {

        }

        #endregion
        #region "Functions"
        private void SaveFile(string fileName)
        {
            FileStream fileToAcess = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter write = new StreamWriter(fileToAcess);

            write.Write(textBoxBody.Text);

            write.Close();
        }
        #endregion
    }
}
