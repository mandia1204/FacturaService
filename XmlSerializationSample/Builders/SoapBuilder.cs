using System.IO;
using System.Text;
using XmlSerializationSample.Models;

namespace XmlSerializationSample.Builders
{
    public class SoapBuilder
    {
        private Encoding _encoding;
        private string _encodingName;
        public SoapBuilder(Encoding encoding)
        {
            _encoding = encoding;
            _encodingName = _encoding.BodyName;
        }

        public void Build(SoapRequest request)
        {
            using (Stream stream = request.Request.GetRequestStream())
            {
                //write initial header
                WriteSoapHeader(stream);

                //write soap request
                byte[] bytes = _encoding.GetBytes(request.SoapStr);
                stream.Write(bytes, 0, bytes.Length);

                WriteAttachment(stream, request.FileContent, request.FileName);
                //request.SoapXml.Save(stream);
                //zip the request
                //using (var gz = new GZipStream(stream, CompressionMode.Compress, false))
                //{
                //    //request.SoapXml.Save(gz);
                //    //WriteToStream(gz, request);
                //}
            }
        }

        public string GetBoundary(string soapPart)
        {
            int num = 0;
            var tail = "";
            switch (soapPart)
            {
                case "init":
                    num = 4;
                    break;
                case "body":
                    num = 6;
                    break;
                case "end":
                    num = 6;
                    tail = "--";
                    break;
            }

            return string.Format("{0}=_Part_12_1770977271.1490462803037{1}", new string('-', num), tail);
        }

        private void WriteSoapHeader(Stream stream)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine(GetBoundary("body"));
            //sb.AppendLine(string.Format("Content-Type: text/xml; charset=UTF-8"));
            sb.AppendLine(string.Format("Content-Type: text/xml; charset={0}", _encodingName));
            sb.AppendLine("Content-Transfer-Encoding: 8bit");
            sb.AppendLine("Content-ID: <rootpart@soapui.org>");
            sb.AppendLine("");

            byte[] bytes = _encoding.GetBytes(sb.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteAttachment(Stream stream, string fileContent, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(GetBoundary("body"));
            sb.AppendLine("Content-Type: text/plain; charset=us-ascii");
            sb.AppendLine("Content-Transfer-Encoding: 7bit");
            //sb.AppendLine("Content-ID: <attachmentSample.txt>");
            //sb.AppendLine("Content-Disposition: attachment; name=\"attachmentSample.txt\"");
            sb.AppendLine(string.Format("Content-ID: <{0}>", fileName));
            sb.AppendLine(string.Format("Content-Disposition: attachment; name=\"{0}\"", fileName));
            sb.AppendLine("");
            sb.AppendLine(fileContent);
            sb.AppendLine(GetBoundary("end"));

            byte[] bytes = _encoding.GetBytes(sb.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
