using System.IO;
using System.IO.Compression;
using System.Text;

namespace XmlSerializationSample.Util
{
    public class Zipper
    {
        public byte[] Zip(string fileName, string fileContent)
        {
            var fileBytes = Encoding.Unicode.GetBytes(fileContent);
            byte[] compressedBytes;
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    var fileInArchive = archive.CreateEntry(fileName + ".xml", CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(fileBytes))
                    {
                        fileToCompressStream.CopyTo(entryStream);
                    }
                }
                compressedBytes = outStream.ToArray();
            }
            return compressedBytes;
        }
    }
}
