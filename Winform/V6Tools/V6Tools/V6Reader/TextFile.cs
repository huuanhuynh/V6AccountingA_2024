using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace V6Tools.V6Reader
{
    public class TextFile
    {
        /// <summary>
        /// Dọc dữ liệu file text vào Table
        /// <para> Kiểu 1: 1 dòng duy nhất, các giá trị cách nhau bằng dấu , hoặc ; hoặc / hoặc \r</para>
        /// <para> Kiểu 2: mỗi dòng 1 value => table 1 cột</para>
        /// <para> Kiểu 3: nhiều dòng, mỗi dòng cách nhau bằng tab, dòng đầu là tiêu đề</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ToTable(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            try
            {
                DataTable result = new DataTable();
                int countLine = 0;
                List<string> lines = new List<string>();
                string currentLine;
                string[] ss;
                string s;

                while(!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    lines.Add(s);
                    countLine++;
                }
                sr.Close();
                fs.Close();

                if(countLine == 1)
                {
                    ss = ReadOne(lines[0]);//.Split(',', ';', '/', '\t');
                    result.Columns.Add();
                    foreach (var item in ss)
                    {
                        var row = result.NewRow(); row[0] = item;
                        result.Rows.Add(row);
                    }
                }
                else if(countLine>1)
                {
                    currentLine = lines[0];
                    
                    //Tao cot cho table
                    ss = ReadOne(currentLine);
                    int numOfColumns = ss.Length;
                    foreach (var item in ss)
                    {
                        result.Columns.Add(item);
                    }
                    lines.RemoveAt(0);

                    //Tao cac dong
                    foreach (var line in lines)
                    {
                        ss = ReadOne(line);
                        var row = result.NewRow();
                        for (int i = 0; i < numOfColumns && i<ss.Length; i++)
                        {
                            row[i] = ss[i];
                        }
                        result.Rows.Add(row);
                    }
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                fs.Close();
                throw new Exception("TextFileToTable " + ex.Message);
            }
        }

        /// <summary>
        /// Hàm phục vụ cho hàm ToTable
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string[] ReadOne(string line)
        {
            while (line.Contains("\""))
            {
                var index1 = line.IndexOf("\"", 0, StringComparison.Ordinal);
                var index2 = line.IndexOf("\"", index1 + 1, StringComparison.Ordinal);
                var index3 = line.IndexOf(",", index1 + 1, StringComparison.Ordinal);
                while (index3>0 && index3 < index2)
                {
                    line = line.Remove(index3, 1);
                    line = line.Insert(index3, " ");
                    index3 = line.IndexOf(",", index1 + 1, 1, StringComparison.Ordinal);
                }
                line = line.Remove(index2, 1);
                line = line.Remove(index1, 1);
            }
            return line.Split('\t', ',');
        }

        /// <summary>
        /// Đọc một file trả về chuỗi chứa trong đó.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ToString(string fileName)
        {
            string result = null;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            try
            {
                result = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                result = ex.Message;
            }

            return result;
        }
    }
}
