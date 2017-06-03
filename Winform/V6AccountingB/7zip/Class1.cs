using System;
using System.IO;
using SevenZip.Compression.LzmaAlone;

namespace SevenZip
{
    public class Class1
    {
        private static ICodeProgress progress = new LzmaBench.CProgressInfo();

        //private static void CompressFileLZMA(string inFile, string outFile)
        //{
        //    Encoder coder = new Encoder();
        //    using (FileStream input = new FileStream(inFile, FileMode.Open))
        //    {
        //        using (FileStream output = new FileStream(outFile, FileMode.Create))
        //        {
        //            progress = new LzmaBench.CProgressInfo();
        //            coder.Code(input, output, -1, -1, progress);
        //            output.Flush();
        //        }
        //    }
        //}

        public static void CompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Write the encoder properties
            coder.WriteCoderProperties(output);

            // Write the decompressed file size.
            output.Write(BitConverter.GetBytes(input.Length), 0, 8);

            // Encode the file.
            coder.Code(input, output, input.Length, -1, null);
            output.Flush();
            output.Close();
        }

        public static void CompressFolderLZMA(string inDirectory, string outFile)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();

            FileStream output = new FileStream(outFile, FileMode.Create);

            // Write the encoder properties
            coder.WriteCoderProperties(output);

            long offsetEnd = 0;
            CompressFolderRecusive(coder, output, inDirectory, 0, out offsetEnd);
            var result = offsetEnd;
            output.Close();
        }

        private static void CompressFolderRecusive(
            SevenZip.Compression.LZMA.Encoder coder,
            FileStream output, string inDirectory, long offsetStart, out long offsetEnd)
        {
            int offset1 = (int) offsetStart;
            var files = Directory.GetFiles(inDirectory);
            foreach (string file in files)
            {
                // Write the decompressed file size.
                FileStream input = new FileStream(file, FileMode.Open);
                output.Write(BitConverter.GetBytes(input.Length), 0, 8);

                // Encode the file.
                coder.Code(input, output, input.Length, -1, null);
                output.Flush();
                offset1 = (int) output.Length;
            }
            var folders = Directory.GetDirectories(inDirectory);
            foreach (string folder in folders)
            {
                CompressFolderRecusive(coder, output, folder, offset1, out offsetEnd);
            }
            offsetEnd = output.Length;
        }

        public static void DecompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Read the decoder properties
            byte[] properties = new byte[5];
            input.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            input.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(input, output, input.Length, fileLength, null);
            output.Flush();
            output.Close();
        }
    }
}
