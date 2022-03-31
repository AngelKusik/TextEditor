﻿// Author: Angelica Kusik
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
        /// <summary>
        /// Creates a new document by reseating the textbox to default values.
        /// </summary>

        private void NewClick(object sender, EventArgs e)
        {
            //clear the textbox and reseat the file path variable to create a new document.
            textBoxBody.Text = String.Empty;
            openFile = String.Empty;            
        }

        private void OpenClick(object sender, EventArgs e)
        {
            openFile = String.Empty;

           // OpenFileDialog openDialog = new OpenFileDialog();
            //FileStream fileToAcess = new FileStream(openFile, FileMode.Open, FileAccess.Read);
           // StreamReader read = new StreamReader(fileToAcess);

            //Read the current file in the textbox into the file.
            //read.ReadToEnd(fileToAcess.Text.ToString());

            //Close the write 
            //read.Close();
        }
        /// <summary>
        /// Cuts a selected section of text from the textbox and pastes it to the clipboard.
        /// </summary>
        private void CutClick(object sender, EventArgs e)
        {
            //If the user clicks the textBox (the textbox is on focus).
            if (textBoxBody.Focused)
            {
                //Ensure the user selected some text to cut.
                if (textBoxBody.SelectedText != String.Empty)
                {
                    //Cut the selected text in the control and add it to the clipboard using its own method.
                    CutText(textBoxBody);
                    //For future reference, I could also have used this: textBoxBody.Cut();
                }
                //If the user didn't select any text
                else
                {
                    //Display an error message informing that Cut failed.
                    MessageBox.Show("Cut Failed.", "No text is selected.");
                }
            }
        }
        /// <summary>
        /// Copies all the text or a selected part of the text on the textbox. If there is no text to be copied, issues an error message.
        /// </summary>
        private void CopyClick(object sender, EventArgs e)
        {
            //If the user clicks the textBox (the textbox is on focus).
            if (textBoxBody.Focused)
            {
                //If the user selects some text in the textBox 
                if(textBoxBody.SelectedText != String.Empty)
                {
                    //Copy the selected text to the clipboard using it's own method
                    CopyText(textBoxBody.SelectedText);
                    //Here I could also have used the textBoxBody.Copy() function.
                }
                //If the user don't select any text in the textBox but the textBox
                //contains some text.
                else if(textBoxBody.SelectedText == String.Empty && textBoxBody.Text != String.Empty)
                {
                    //Copy all the text in the textBox to the Clipboard using the CopyText method and 
                    //let the user know everything was copied.
                    CopyText(textBoxBody.SelectedText);
                    MessageBox.Show("Copy Successful.", "All text in the textbox was copied.");
                }
                //If the use attempt to copy but there is no text on the textBox.
                else
                {
                    //Issue an error message.
                    MessageBox.Show("Copy Failed.", "No text is selected.");
                }
            }
        }
        /// <summary>
        /// Pastes text from the clipboard over a selected text section of the textbox or after the last
        /// text character.
        /// </summary>
        private void PasteClick(object sender, EventArgs e)
        {
            //First check if is there any text in the Clipboard to be pasted in the textbox.
            if (Clipboard.ContainsText())
            {
                //If the Clipboard contains text call the PasteFunction() to paste it on the textbox.
                PasteText(textBoxBody);
            }
        }
        /// <summary>
        /// Dislays information about the application.
        /// </summary>
        private void AboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("Alf Text Editor" + Environment.NewLine +
            "By Angelica Kusik in partnership with Professor Kyle Chapman" + Environment.NewLine + 
            "\u00a9 2022 Angelica Kusik CO. All rights reserved.");
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
        /// <summary>
        /// Removes the selected text when the user clicks cut and add it to the clipboard.
        /// </summary>
        /// <param name="textbox">Passes the textBoxBody as a parameter</param>
        private void CutText(TextBox textbox)
        {
            //Add the cut text to the clipboard.
            Clipboard.SetText(textbox.SelectedText);

            //Create a variable to identify the end point of the selected text to be removed. 
            int selectionEnd = textbox.SelectionLength;
            //Remove the selected text from the textbox.
            textbox.Text = textbox.Text.Remove(textbox.SelectionStart, selectionEnd);
        }
        /// <summary>
        /// Copies selected text to the clipboard.
        /// </summary>
        /// <param name="selectedText"></param>
        private void CopyText(string selectedText)
        {
            //Copy the selected text to the clipboard.
            Clipboard.SetText(selectedText);
        }

        private void PasteText(TextBox textbox)
        {
            //Now paste the text from the clipboard where the user selected.
            textbox.Text = textbox.Text.Insert(textbox.SelectionStart, Clipboard.GetText());

        }
        #endregion
    }
}
