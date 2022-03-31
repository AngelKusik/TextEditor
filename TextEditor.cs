// Author: Angelica Kusik
// Date: March 31, 2022
// Description: 
// A simple text editor application created as Lab 5 requirement for the NETD course that provides the user with some
//basic text edition features such as save, save as, copy, paste, cut, and more.

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
        //Declare a "global" variable to hold the file path and set it
        //equal to an empty string until the user selects where the file must be saved.
        string openFile = String.Empty;
        public formTextEditor()
        {
            InitializeComponent();
        }
        #region "Event Handlers"
        /// <summary>
        /// Saves the file to the selected location.
        /// </summary>
        private void SaveClick(object sender, EventArgs e)
        {
            //if openFile (the file that is currently open) has no value,
            //call "Save As" instead!
            if (openFile == String.Empty)
            {
                SaveAsClick(sender, e);
            }
            //If openFile has a value, just call the SaveFile() function!
            else if (openFile != String.Empty)
            {
                SaveFile(openFile);
            }
        }
        /// <summary>
        /// Allows the user to select the location where the file must be saved.
        /// </summary>
        private void SaveAsClick(object sender, EventArgs e)
        {
            //Declare, instantiate and configure a new SaveFileDialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (*.alf)|*.alf";

            //If the person picks a file and clicks "OK"
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //Set openFile equal to the File path 
                openFile = saveDialog.FileName;
                //Call the SaveFile function to write the contents of the textbox into the file.
                SaveFile(openFile);
            }
        }
        /// <summary>
        /// Closes the application.
        /// </summary>
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
        /// <summary>
        /// Copies all the text or a selected part of the text on the textbox. If there is no text to be copied, issues an error message.
        /// </summary>
        private void CopyClick(object sender, EventArgs e)
        {
            //If the user clicks the textBox.
            if (textBoxBody.Focused)
            {
                //If the user selects some text in the textBox 
                if(textBoxBody.SelectedText != String.Empty)
                {
                    //Copy the selected text to the clipboard.
                    Clipboard.SetText(textBoxBody.SelectedText);
                }
                //If the user don't select any text in the textBox but the textBox
                //contains some text.
                else if(textBoxBody.SelectedText == String.Empty && textBoxBody.Text != String.Empty)
                {
                    //Copy all the text in the textBox to the Clipboard and 
                    //let the user know everything was copied.
                    Clipboard.SetText(textBoxBody.Text);
                    MessageBox.Show("Copy Successful.", "All text in the textbox was copied.");
                }
                //If the use attempt to copy but there is no text on the textBox.
                else
                {
                    //Issue an error message.
                    MessageBox.Show("Copy Failed.", "No text is selected.");
                }
            //Just in case anything else goes wrong (I heard its a good idea to always have an else on a if block
            //to handle any exceptions that I may not have antecipated.)
            }else
            {
                MessageBox.Show("Ops.. something went wrong.");
            }
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
            //Create a System.IO object to make file access easier
            FileStream fileToAcess = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter write = new StreamWriter(fileToAcess);

            //Write the current text in the textbox into the file.
            write.Write(textBoxBody.Text);

            //Close the write 
            write.Close();
        }
        #endregion
    }
}
