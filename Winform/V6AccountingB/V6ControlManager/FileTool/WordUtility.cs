using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace V6ControlManager.FileTool
{
    public class WordUtility
    {
        public void ReplaceText(string templateFile, string saveFile, IDictionary<string, string> data)
        {
            try
            {
                using (var rs = File.OpenRead(templateFile))
                {

                    var generateFile = saveFile;
                    using (var ws = File.Create(generateFile))
                    {
                        var doc = new XWPFDocument(rs);

                        foreach (XWPFParagraph para in doc.Paragraphs)
                        {
                            foreach (KeyValuePair<string, string> item in data)
                            {
                                if (para.ParagraphText.Contains(item.Key))
                                {
                                    para.ReplaceText(item.Key, item.Value);
                                }
                            }
                        }
                        foreach (XWPFTable tab in doc.Tables)
                        {

                            foreach (var item in tab.Rows)
                            {
                                var cells = item.GetTableCells();
                                foreach (XWPFTableCell cell in cells)
                                {
                                    foreach (XWPFParagraph para in cell.Paragraphs)
                                    {
                                        foreach (KeyValuePair<string, string> item2 in data)
                                        {
                                            if (para.ParagraphText.Contains(item2.Key))
                                            {
                                                para.ReplaceText(item2.Key, item2.Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        doc.Write(ws);
                    }
                }

                
                
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        /// <summary>
        /// Thay thế nội dung trong file docx và lưu.
        /// </summary>
        /// <param name="fileDocx"></param>
        /// <param name="data"></param>
        public void ReplaceTextInterop(string fileDocx, IDictionary<string, string> data)
        {
            try
            {
                object fileName = fileDocx;
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
                Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: true);
                aDoc.Activate();
                
                foreach (KeyValuePair<string, string> item in data)
                {
                    FindAndReplaceInterop(wordApp, item.Key, item.Value);
                }
                aDoc.Save();
                aDoc.Close();

                Marshal.ReleaseComObject(aDoc);
                Marshal.ReleaseComObject(wordApp);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        private void FindAndReplaceInterop(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}
