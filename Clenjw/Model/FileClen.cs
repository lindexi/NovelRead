using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;

namespace lindexi.uwp.Clenjw.Model
{
    public class FileClen : IFileClen
    {
        public FileClen()
        {
        }

        public FileClen(StorageFile file)
        {
            File = file;
            int n = file.Name.LastIndexOf(".");
            if (n >= 0)
            {
                Name = file.Name.Substring(0, n);
            }
        }

        public bool Check { set; get; }

        public int Poit { set; get; }

        [JsonIgnore]
        public StorageFile File { set; get; }

        [JsonIgnore]
        public string Str { set; get; }

        public string Name { set; get; }

        public bool Equal(StorageFile file)
        {
            return File?.Path == file.Path;
        }

        public async Task Read()
        {
            
            try
            {
                Str = await Windows.Storage.FileIO.ReadTextAsync(File);
            }
            catch (ArgumentOutOfRangeException)
            {
                IBuffer buffer = await FileIO.ReadBufferAsync(File);
                DataReader reader = DataReader.FromBuffer(buffer);
                byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(fileContent);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding gbk = Encoding.GetEncoding("GBK");
                Str = gbk.GetString(fileContent);
            }
        }

        private static Encoding AutoEncoding(byte[] bom)
        {
            if (bom.Length != 4)
            {
                throw new ArgumentException();
            }
            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }
    }
}