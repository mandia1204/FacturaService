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
                WriteSoapBody(stream, request.SoapStr);

                //add attachment header
                WriteAttachmentHeader(stream, request.FileName);

                //write the zip file
                WriteFile(stream, request.FileContent);

                //write end of request
                WriteEndFile(stream);
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

        private void WriteEndFile(Stream stream)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(GetBoundary("end"));

            byte[] bytes = _encoding.GetBytes(sb.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteSoapBody(Stream stream, string body)
        {
            byte[] bytes = _encoding.GetBytes(body);
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteSoapHeader(Stream stream)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine(GetBoundary("body"));
            sb.AppendLine(string.Format("Content-Type: text/xml; charset={0}", _encodingName));
            sb.AppendLine("Content-Transfer-Encoding: 8bit");
            sb.AppendLine("Content-ID: <rootpart@soapui.org>");
            sb.AppendLine("");

            byte[] bytes = _encoding.GetBytes(sb.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteFile(Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteAttachmentHeader(Stream stream, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(GetBoundary("body"));
            sb.AppendLine("Content-Type: application/zip");
            sb.AppendLine("Content-Transfer-Encoding: binary");
            sb.AppendLine(string.Format("Content-ID: <{0}>", fileName));
            sb.AppendLine(string.Format("Content-Disposition: attachment; name=\"{0}\", filename=\"{0}\"", fileName));
            sb.AppendLine("");

            byte[] bytes = _encoding.GetBytes(sb.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
