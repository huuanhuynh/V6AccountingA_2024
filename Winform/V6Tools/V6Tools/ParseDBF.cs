using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
//Ngay 13 thang 9 nam 2013: Sua kieu int(I) va float(B)
//Ngay 03 thang 2 nam 2015: Da co ham Write by huuan
namespace V6Tools
{
    // Read an entire standard DBF file into a DataTable
    public class ParseDBF
    {
        #region ==== Structs ====
        // This is the file header for a DBF. We do this special layout with everything
        // packed so we can read straight from disk into the structure to populate it
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct DBFHeader
        {
            public byte version;
            public byte updateYear;
            public byte updateMonth;
            public byte updateDay;
            public Int32 numRecords;
            /// <summary>
            /// ??? bao gom DBFHeader va fieldHeader len ++??
            /// </summary>
            public Int16 headerLen;
            public Int16 recordLen;
            public Int16 reserved1;
            public byte incompleteTrans;
            public byte encryptionFlag;
            public Int32 reserved2;
            public Int64 reserved3;
            public byte MDX;
            public byte language;
            public Int16 reserved4;
        }

        // This is the field descriptor structure. There will be one of these for each column in the table.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct FieldDescriptor
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string fieldName;
            public char fieldType;
            public Int32 address;
            public byte fieldLen;
            /// <summary>
            /// Số chữ số phần thập phân (lẻ)
            /// </summary>
            public byte count;
            public Int16 reserved1;
            public byte workArea;
            public Int16 reserved2;
            public byte flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] reserved3;
            public byte indexFlag;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct DBTHeader
        {
            public Int32 nextBlockID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] reserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string fileName;
            public byte version; // 0x03 = Version III, 0x00 = Version IV
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] reserved3;
            public Int16 blockLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 490)]
            public byte[] reserved4;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct MemoHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] reserved;
            public Int16 startPosition;
            public Int32 fieldLength;
        }
        #endregion ==== struct ====

        #region ==== Read DBF ====
        /// <summary>
        /// Read an entire standard DBF file into a DataTable
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <returns></returns>
        public static DataTable ReadDBF(string dbfFile)
        {
            long start = DateTime.Now.Ticks;
            DataTable dt = new DataTable();
            BinaryReader recReader;
            string number;
            string year;
            string month;
            string day;
            int lDate;
            long lTime;
            DataRow row;
            int fieldIndex;

            // If there isn't even a file, just return an empty DataTable
            if ((false == File.Exists(dbfFile)))
            {
                return dt;
            }
            
            //Data for memo?
            Dictionary<int, string> memoLookup = ReadDBT(dbfFile);

            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                // Read the header into a buffer
                fs = new FileStream(dbfFile, FileMode.Open);
                br = new BinaryReader(fs);
                //br = new BinaryReader(File.OpenRead(dbfFile));
                byte[] buffer = br.ReadBytes(Marshal.SizeOf(typeof(DBFHeader)));

                // Marshall the header into a DBFHeader structure
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                DBFHeader header = (DBFHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DBFHeader));
                handle.Free();
                
                // Read in all the field descriptors. Per the spec, 13 (0D) marks the end of the field descriptors
                ArrayList fields = new ArrayList();
                
                byte btest = br.ReadByte();
                while ((13 != btest))//.PeekChar()))//PeekChar đọc thử ký tự tiếp theo
                {
                    fs.Position--;
                    buffer = br.ReadBytes(Marshal.SizeOf(typeof(FieldDescriptor)));
                    handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    fields.Add((FieldDescriptor)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(FieldDescriptor)));
                    handle.Free();
                    btest = br.ReadByte();
                }
                fs.Position--;

                //Try count all header len
                int tryCount = 0;
                tryCount += Marshal.SizeOf(typeof(DBFHeader));
                int oneFieldSize = Marshal.SizeOf(typeof(FieldDescriptor));
                tryCount += oneFieldSize*fields.Count;
                int tryCount2 = 0;
                foreach (FieldDescriptor item in fields)
                {
                    tryCount2 += item.fieldLen;
                }
                

                // Read in the first row of records, we need this to help determine column types below
                ((FileStream)br.BaseStream).Seek(header.headerLen + 1, SeekOrigin.Begin);
                buffer = br.ReadBytes(header.recordLen);
                recReader = new BinaryReader(new MemoryStream(buffer));

                // Create the columns in our new DataTable
                DataColumn col = null;
                foreach (FieldDescriptor field in fields)
                {
                    number = Encoding.ASCII.GetString(recReader.ReadBytes(field.fieldLen));
                    switch (field.fieldType)
                    {
                        case 'N':
                        case 'B'://double
                            col = new DataColumn(field.fieldName, typeof(decimal));
                            break;
                        case 'I':
                            col = new DataColumn(field.fieldName, typeof(Int32));
                            break;
                        case 'C':
                            col = new DataColumn(field.fieldName, typeof(string));
                            break;
                        case 'T':
                            col = new DataColumn(field.fieldName, typeof(DateTime));
                            break;
                        case 'D':
                            col = new DataColumn(field.fieldName, typeof(DateTime));
                            break;
                        case 'L':
                            col = new DataColumn(field.fieldName, typeof(bool));
                            break;
                        case 'F':
                            col = new DataColumn(field.fieldName, typeof(Double));
                            break;
                        case 'M':
                            col = new DataColumn(field.fieldName, typeof(string));
                            break;
                        case '0': continue;
                        //col = new DataColumn(field.fieldName, typeof(string));
                        //break;
                        default://0;
                            col = new DataColumn(field.fieldName, typeof(string));
                            break;
                    }
                    dt.Columns.Add(col);
                }

                // Skip past the end of the header.
                ((FileStream)br.BaseStream).Seek(header.headerLen, SeekOrigin.Begin);
                
                //string systemDecimalSymbol = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                //.Replace(".", systemDecimalSymbol));
                //row[fieldIndex] = float.Parse(number);

                NumberFormatInfo decimalSeparatorFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
                //row[fieldIndex] = float.Parse(number, decimalSeparatorFormat);
                
                // Read in all the records
                for (int counter = 0; counter < header.numRecords; counter++)
                {
                    // First we'll read the entire record into a buffer and then read each field from the buffer
                    // This helps account for any extra space at the end of each record and probably performs better
                    buffer = br.ReadBytes(header.recordLen);
                    recReader = new BinaryReader(new MemoryStream(buffer));
                    
                    // All dbf field records begin with a deleted flag field. Deleted - 0x2A (asterisk) else 0x20 (space)
                    if (recReader.ReadChar() == '*')
                    {
                        continue;
                    }

                    // Loop through each field in a record
                    fieldIndex = 0;
                    row = dt.NewRow();
                    foreach (FieldDescriptor field in fields)
                    {
                        switch (field.fieldType)
                        {
                            #region ==== Number ====
                            case 'F':
                                number = Encoding.ASCII.GetString(recReader.ReadBytes(field.fieldLen));
                                if (IsNumber(number))
                                {
                                    row[fieldIndex] = float.Parse(number, decimalSeparatorFormat);
                                }
                                else
                                {
                                    row[fieldIndex] = 0.0F;
                                }
                                break;
                            case 'N':  // Number
                                number = Encoding.ASCII.GetString(recReader.ReadBytes(field.fieldLen));
                                
                                if (IsNumber(number))
                                {   
                                    if (number.IndexOf('.') > -1)
                                    {
                                        row[fieldIndex] = decimal.Parse(number, decimalSeparatorFormat);
                                    }
                                    else
                                    {
                                        row[fieldIndex] = int.Parse(number, decimalSeparatorFormat);
                                    }
                                }
                                else
                                {
                                    row[fieldIndex] = 0;
                                }

                                break;
                            case 'B':   //Double
                                byte[] buf = recReader.ReadBytes(field.fieldLen);
                                var convDouble = BitConverter.ToDouble(buf, 0);
                                row[fieldIndex] = convDouble;
                                break;

                            case 'I':
                                byte[] bufi = recReader.ReadBytes(field.fieldLen);
                                var convInteger = BitConverter.ToInt16(bufi, 0);
                                row[fieldIndex] = convInteger;
                                break;

                            #endregion end Number

                            #region ==== String ====
                            case 'C': // String
                                //row[fieldIndex] = Encoding.ASCII.GetString(recReader.ReadBytes(field.fieldLen));
                                row[fieldIndex] = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));//Yes
                                //row[fieldIndex] = Encoding.Unicode.GetString(recReader.ReadBytes(field.fieldLen));
                                //row[fieldIndex] = Encoding.UTF32.GetString(recReader.ReadBytes(field.fieldLen));
                                //row[fieldIndex] = Encoding.UTF7.GetString(recReader.ReadBytes(field.fieldLen));//Yes Windows tiếng Việt
                                //row[fieldIndex] = Encoding.UTF8.GetString(recReader.ReadBytes(field.fieldLen));
                                break;
                            #endregion end String

                            #region ==== Date ====
                            case 'D': // Date (YYYYMMDD)
                                year = Encoding.ASCII.GetString(recReader.ReadBytes(4));
                                month = Encoding.ASCII.GetString(recReader.ReadBytes(2));
                                day = Encoding.ASCII.GetString(recReader.ReadBytes(2));
                                row[fieldIndex] = DBNull.Value;
                                try
                                {
                                    if (IsNumber(year) && IsNumber(month) && IsNumber(day))
                                    {
                                        if ((Int32.Parse(year) > 1900))
                                        {
                                            row[fieldIndex] = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                                        }
                                    }
                                }
                                catch
                                { }

                                break;
                            #endregion ==== end Date ====

                            #region ==== Timestamp, Bool ====
                            case 'T': // Timestamp, 8 bytes - two integers, first for date, second for time
                                // Date is the number of days since 01/01/4713 BC (Julian Days)
                                // Time is hours * 3600000L + minutes * 60000L + Seconds * 1000L (Milliseconds since midnight)
                                try
                                {
                                    lDate = recReader.ReadInt32();
                                    lTime = recReader.ReadInt32() * 10000L;
                                    if (lDate > 2415018.5)
                                    row[fieldIndex] = JulianToDateTime(lDate).AddTicks(lTime);
                                }
                                catch
                                {
                                    
                                }
                                break;

                            case 'L': // Boolean (Y/N)
                                if ('Y' == recReader.ReadByte())
                                {
                                    row[fieldIndex] = true;
                                }
                                else
                                {
                                    row[fieldIndex] = false;
                                }

                                break;
                            #endregion end timestamp, bool

                            #region ==== Memo ====
                            case 'M': // Memo
                                {
                                    int intRef;
                                    string strRef = Encoding.ASCII.GetString(recReader.ReadBytes(field.fieldLen)).Trim();
                                    if (Int32.TryParse(strRef, out intRef))
                                    {
                                        if (memoLookup.ContainsKey(intRef))
                                            row[fieldIndex] = memoLookup[intRef];
                                        else
                                            row[fieldIndex] = string.Empty;
                                    }
                                    else
                                        row[fieldIndex] = null;
                                    break;
                                }
                            #endregion ====  ====

                            #region ==== Not support and default ====
                            case '0': continue;
                            //number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));//Yes
                            //if (number == "\0")
                            //    row[fieldIndex] = null;
                            //else
                            //    row[fieldIndex] = number;
                            //break;
                            default:
                                row[fieldIndex] = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));//Yes
                                break;
                            #endregion ====  ====
                        }
                        fieldIndex++;
                    }

                    recReader.Close();
                    dt.Rows.Add(row);
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                if (null != br)
                {
                    br.Close();
                }
            }

            //long count = DateTime.Now.Ticks - start;

            return dt;
        }
        #endregion ==== read ====

        #region ==== Write DBF ====
        /// <summary>
        /// Ghi dữ liệu ra file dbf
        /// </summary>
        /// <param name="data">Bảng dữ liệu</param>
        /// <param name="fileName">Tên file ghi</param>
        public static void WriteDBF(DataTable data, string fileName)
        {
            #region Make field struct list
            List<FieldDescriptor> fields = new List<FieldDescriptor>();
            int currentFieldIndex = -1;
            foreach (DataColumn column in data.Columns)
            {
                FieldDescriptor f = new FieldDescriptor();
                currentFieldIndex++;
                f.fieldName = column.ColumnName;
                if (currentFieldIndex == 0)
                    f.address = 1;
                else
                {
                    FieldDescriptor tf = fields[currentFieldIndex - 1];
                    f.address = tf.address + tf.fieldLen;
                }
                
                switch (column.DataType.ToString())
                {
                    
                    case "System.Decimal":
                        f.fieldType = 'N';//B?
                        f.fieldLen = 20;// "-79228162514264337593543950335".Length;"        22000.00"//độ dài con số kể cả dấu .
                        f.count = 4;//Số chữ số phần thập phân (lẻ)
                        break;
                    case "System.Int":
                        f.fieldType = 'I';
                        f.fieldLen = 4;
                        f.count = 0;
                        break;
                    case "System.Double":
                        f.fieldType = 'B';
                        f.fieldLen = 8;
                        f.count = 0;
                        break;
                    case "System.Single": case "System.Float":
                        f.fieldType = 'F';
                        f.fieldLen = 10;//ở đây kiểu N và F đang dùng độ dài cố định, thực chất
                        f.count = 2;    //khi design có thể khác. vd float(10,2)=>len=13,count=2
                        break;
                    case "System.String":
                        f.fieldType = 'C';
                        //findMaxLen
                        foreach (DataRow row in data.Rows)
                        {
                            int l1 = row[currentFieldIndex].ToString().Length;
                            if (l1 > byte.MaxValue) l1 = byte.MaxValue;
                            if (l1 > f.fieldLen) f.fieldLen = (byte)l1;
                        }
                        if (f.fieldLen == 0) f.fieldLen = 10;
                        f.count = 0;
                        break;
                    case "System.DateTime":
                        f.fieldType = 'T';
                        f.fieldLen = 8;
                        f.count = 0;
                        break;
                    //case 'Date':      //trường hợp chỉ lưu ngày tháng năm, không lưu ngày giờ
                        //f.fieldType = 'D';
                        //f.fieldLen = 8;//value format YYYYMMDD
                        //f.count = 0;
                    case "System.Boolean":
                        f.fieldType = 'L';
                        f.fieldLen = 1;
                        f.count = 0;
                        break;
                   
                    //case 'M'://Memo
                    //    col = new DataColumn(field.fieldName, typeof(string));
                    //    break;
                    //case '0': continue;
                    ////col = new DataColumn(field.fieldName, typeof(string));
                    ////break;
                    default:
                        f.fieldType = 'C';
                        //findMaxLen
                        foreach (DataRow row in data.Rows)
                        {
                            int l1 = row[currentFieldIndex].ToString().Length;
                            if (l1 > byte.MaxValue) l1 = byte.MaxValue;
                            if (l1 > f.fieldLen) f.fieldLen = (byte)l1;
                        }
                        
                        f.count = 0;
                        break;
                }
                fields.Add(f);
                
            }
                        

            // Field Struct
            //private struct FieldDescriptor
            //{
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            //    public string fieldName;
            //    public char fieldType;
            //    public Int32 address;
            //    public byte fieldLen;
            //    public byte count;
            //    public Int16 reserved1;
            //    public byte workArea;
            //    public Int16 reserved2;
            //    public byte flag;
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            //    public byte[] reserved3;
            //    public byte indexFlag;
            //}
            #endregion make fields list

            #region Make Header struct
            
            //private struct DBFHeader
            //{
            //    public byte version;
            //    public byte updateYear;
            //    public byte updateMonth;
            //    public byte updateDay;
            //    public Int32 numRecords;
            //    public Int16 headerLen;
            //    public Int16 recordLen;
            //    public Int16 reserved1;
            //    public byte incompleteTrans;
            //    public byte encryptionFlag;
            //    public Int32 reserved2;
            //    public Int64 reserved3;
            //    public byte MDX;
            //    public byte language;
            //    public Int16 reserved4;
            //}

            DBFHeader dbfHeader = new DBFHeader();
            DateTime n = DateTime.Now;
            
            int headerLen = 0;
            headerLen += Marshal.SizeOf(typeof(DBFHeader));
            int oneFieldHeaderLen = Marshal.SizeOf(typeof(FieldDescriptor));
            headerLen += oneFieldHeaderLen * fields.Count;

            int recordLen = 1;  //1 for * or space
            foreach (FieldDescriptor item in fields)
            {
                 recordLen += item.fieldLen;
            }
            //=== Header infomation in right order ===
            dbfHeader.version = 48;
            dbfHeader.updateDay = (byte)n.Day;
            dbfHeader.updateMonth = (byte)n.Month;
            dbfHeader.updateYear = (byte)(n.Year - 2000);
            dbfHeader.numRecords = data.Rows.Count;
            dbfHeader.headerLen = (short)(headerLen + 100);
            dbfHeader.recordLen = (short)recordLen;
            dbfHeader.reserved1 = 0;
            dbfHeader.incompleteTrans = 0;
            dbfHeader.encryptionFlag = 0;
            dbfHeader.reserved2 = 0;
            dbfHeader.reserved3 = 0;
            dbfHeader.MDX = 0;
            dbfHeader.language = 3;
            dbfHeader.reserved4 = 0;
            //==========================================
            #endregion make header struct

            #region Write info and data to File (stream)
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            //Write Header
            byte[] dbfHeaderBytes = getBytes(dbfHeader);
            
            //int cOffset = 0;
            fs.Write(dbfHeaderBytes, 0, dbfHeaderBytes.Length);
            //cOffset += dbfHeaderBytes.Length;

            //Write field header,
            foreach (FieldDescriptor item in fields)
            {
                byte[] fieldBytes = getBytes(item);
                fs.Write(fieldBytes, 0, fieldBytes.Length);
                //cOffset += fieldBytes.Length;
            }
            // end with char13
            fs.WriteByte(13);
            //cOffset++;

            //Write data, row start with space char20h
            //Start from headerLength
            fs.Position = dbfHeader.headerLen;
            //cOffset = dbfHeader.headerLen;
            int rowPos = 0;
            for (int i = 0; i < dbfHeader.numRecords; i++)
            {
                rowPos = dbfHeader.headerLen + (i * (dbfHeader.recordLen));
                fs.WriteByte(0x20);   //(space) for normal row, deleted row use 0x2A (*)
                DataRow row = data.Rows[i];
                bool b = false;
                double d = 0.0;
                string str = "";
                int value = 0;
                int j = 0;
                int pos = 0;
                foreach (FieldDescriptor fd in fields)
                {
                    pos = rowPos + fd.address;  //Test it good
                    fs.Position = pos;          // use for less location error
                    switch (fd.fieldType)
                    {
                        #region - Write field -
                        case 'N':
                            try
                            {
                                str = String.Format("{0:F" + fd.count + "}", (decimal)row[j]);
                                str = str.Replace(',', '.');
                                //fix length right justify, if left use {0,-len}
                                str = String.Format("{0," + fd.fieldLen + "}", str);
                            }
                            catch
                            {
                                str = String.Format("{0," + fd.fieldLen + "}", "");
                            }

                            fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                            break;
                        case 'F':
                            try
                            {
                                str = string.Format("{0:F" + fd.count + "}", (float)row[j]);
                                str = str.Replace(',', '.');
                                str = string.Format("{0," + fd.fieldLen + "}", str);
                            }
                            catch
                            {
                                str = string.Format("{0," + fd.fieldLen + "}", "");
                            }

                            fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                            break;
                        case 'B':
                            try
                            {
                                d = (double)row[j];
                            }
                            catch
                            {
                                d = 0.0;
                            }
                            bw.Write(d);
                            break;
                        case 'I':
                            try
                            {
                                value = (int)row[j];
                            }
                            catch
                            {
                                value = 0;
                            }
                            bw.Write(value);
                            break;
                        case 'C':
                            str = row[j].ToString();
                            str = string.Format("{0,-"+fd.fieldLen+"}", str);
                            byte[] ts = GetBytes8(str);
                            fs.Write(ts, 0, str.Length);
                            break;
                        case 'T':
                            try
                            {
                                DateTime dt = (DateTime)row[j];
                                value = DateTimeToJulian(dt);
                                bw.Write(value);
                                //Time is hours * 3600000L + minutes * 60000L + Seconds * 1000L (Milliseconds since midnight)
                                value = (int)dt.TimeOfDay.TotalMilliseconds;// (dt.Hour * 3600 + dt.Minute * 60 + dt.Second) * 1000;
                                bw.Write(value);
                            }
                            catch (Exception)
                            {
                                fs.Write(new byte[8], 0, 8);
                            }
                            break;
                        case 'D':
                            try
                            {
                                DateTime dtd = (DateTime)row[j];
                                str = dtd.ToString("yyyyMMdd");
                                fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                            }
                            catch
                            {
                                fs.Write(new byte[8], 0, 8);
                            }
                            break;
                        case 'L'://Logical
                            try
                            {
                                b = (bool)row[j];
                            }
                            catch
                            {
                                b = false;
                            }
                            if (b) fs.WriteByte((byte)'Y');
                            else fs.WriteByte((byte)'N');
                            break;
                        //case 'M':
                        //    col = new DataColumn(field.fieldName, typeof(string));
                        //    break;
                        default://0;
                            str = row[j].ToString();
                            str = String.Format("{0,-" + fd.fieldLen + "}", str);
                            fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                            break;
                        #endregion write field
                    }
                    j++;
                }
            }
            fs.Close();
            #endregion write all
        }
        #endregion

        #region ==== Extra Methods ====
        private static byte[] GetBytes8(string str)
        {
            byte[] bytes = new byte[str.Length * 1];
            for (int i = 0; i < str.Length; i++)
            {
                bytes[i] = (byte)str[i];
            }
            return bytes;
        }
        private static Dictionary<int, string> ReadDBT(string dbfFile)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            string dbtFile =
                Path.GetDirectoryName(dbfFile) + @"\" + Path.GetFileNameWithoutExtension(dbfFile) + ".dbt";

            if (!File.Exists(dbtFile))
                return result;

            BinaryReader br = null;
            try
            {
                br = new BinaryReader(File.OpenRead(dbtFile));

                // Read the header into a buffer
                byte[] buffer = br.ReadBytes(Marshal.SizeOf(typeof(DBTHeader)));

                // Marshall the header into a DBTHeader structure
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                DBTHeader header = (DBTHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DBTHeader));
                handle.Free();

                int currentBlock = 1;

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    // Position reader at beginning of current block
                    br.BaseStream.Position = header.blockLength * currentBlock;

                    // Read the memo field header into a buffer
                    buffer = br.ReadBytes(Marshal.SizeOf(typeof(MemoHeader)));

                    // Marshall the header into a MemoHeader structure
                    handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    MemoHeader memHeader = (MemoHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(MemoHeader));
                    handle.Free();

                    int bytesToRead = memHeader.fieldLength - memHeader.startPosition;
                    String value = Encoding.ASCII.GetString(br.ReadBytes(bytesToRead));
                    result.Add(currentBlock, value);

                    // Find next block
                    while (br.BaseStream.Position > (header.blockLength * currentBlock))
                        currentBlock++;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (null != br)
                    br.Close();
            }

            return result;
        }

        /// <summary>
        /// Simple function to test is a string can be parsed. There may be a better way, but this works
        /// If you port this to .NET 2.0, use the new TryParse methods instead of this
        ///   *Thanks to wu.qingman on code project for fixing a bug in this for me
        /// </summary>
        /// <param name="number">string to test for parsing</param>
        /// <returns>true if string can be parsed</returns>
        public static bool IsNumber(string numberString)
        {
            numberString = numberString.Trim();
            if (numberString.StartsWith("-")) numberString = numberString.Substring(1);
            char[] numbers = numberString.ToCharArray();
            int number_count = 0;
            int point_count = 0;
            int space_count = 0;

            foreach (char number in numbers)
            {
                if ((number >= 48 && number <= 57))
                {
                    number_count += 1;
                }
                else if (number == 46)
                {
                    point_count += 1;
                }
                else if (number == 32)
                {
                    space_count += 1;
                }
                else
                {
                    return false;
                }
            }

            return (number_count > 0 && point_count < 2);
        }

        /// <summary>
        /// Convert a Julian Date to a .NET DateTime structure
        /// Implemented from pseudo code at http://en.wikipedia.org/wiki/Julian_day
        /// </summary>
        /// <param name="lJDN">Julian Date to convert (days since 01/01/4713 BC)</param>
        /// <returns>DateTime</returns>
        private static DateTime JulianToDateTime(int lJDN)
        {
            double p = Convert.ToDouble(lJDN);
            double s1 = p + 68569;
            double n = Math.Floor(4 * s1 / 146097);
            double s2 = s1 - Math.Floor((146097 * n + 3) / 4);
            double i = Math.Floor(4000 * (s2 + 1) / 1461001);
            double s3 = s2 - Math.Floor(1461 * i / 4) + 31;
            double q = Math.Floor(80 * s3 / 2447);
            double d = s3 - Math.Floor(2447 * q / 80);
            double s4 = Math.Floor(q / 11);
            double m = q + 2 - 12 * s4;
            double j = 100 * (n - 49) + i + s4;
            return new DateTime(Convert.ToInt32(j), Convert.ToInt32(m), Convert.ToInt32(d));
        }
        private static int DateTimeToJulian(DateTime dt)
        {
            int m = dt.Month;
            int d = dt.Day;
            int y = dt.Year;


            if (m < 3)
            {
                m = m + 12;
                y = y - 1;
            }
            long jd = d + (153 * m - 457) / 5 + 365 * y + (y / 4) - (y / 100) + (y / 400) + 1721119;
            return (int)jd;
        }

        private static byte[] getBytes(DBFHeader aux)
        {
            int length = Marshal.SizeOf(aux);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(aux, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }
        private static byte[] getBytes(FieldDescriptor aux)
        {
            int length = Marshal.SizeOf(aux);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(aux, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }
        private static byte[] getBytes(DBTHeader aux)
        {
            int length = Marshal.SizeOf(aux);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(aux, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }
        private static byte[] getBytes(MemoHeader aux)
        {
            int length = Marshal.SizeOf(aux);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(aux, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }
        #endregion ====  ====
    }
}