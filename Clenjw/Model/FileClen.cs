// lindexi
// 21:33

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
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

        public bool Check
        {
            set;
            get;
        }

        [JsonIgnore]
        public string Str
        {
            set;
            get;
        }

        public bool Equal(StorageFile file)
        {
            return File?.Path == file.Path;
        }

        /// <summary>
        /// </summary>
        /// <param name="font">一行字数</param>
        /// <param name="line">行数</param>
        /// <returns></returns>
        public string Progress(int font, int line)
        {
            int length = font*line;
            if (Poit < 0)
            {
                Poit = 0;
            }
            if (length > Str.Length - Poit)
            {
                length = Str.Length - Poit;
            }

            List<string> str = Spilt(Str.Substring(Poit, length), font);
            while (str.Count > line)
            {
                str.RemoveAt(str.Count - 1);
            }
            int n = str.Sum(temp => temp.Length);
            Poit += n;

            //List<string> str=new List<string>();
            //int n = 0;
            //int i = 0;
            //foreach (var temp in Spilt(Str.Substring(Poit, length), font))
            //{
            //    n += temp.Length;
            //    if (temp == "\n")
            //    {
            //        if (str.LastOrDefault() == "\n")
            //        {

            //        }
            //        else
            //        {
            //            str.Add(temp);
            //        }
            //    }
            //}

            //List<string> str = new List<string>();
            //int i = Poit;
            //int n = 0;
            //StringBuilder temp = new StringBuilder(font);
            //while (str.Count < line)
            //{
            //    if (i > Str.Length)
            //    {
            //        break;
            //    }
            //    temp.Append(Str[i]);
            //    if (Str[i] == '\n')
            //    {
            //        str.Add(temp.ToString());
            //        str.Add("\n");
            //        temp.Clear();
            //        n = 0;
            //    }
            //    else
            //    {
            //        n++;
            //        if (n > font)
            //        {
            //            str.Add(temp.ToString()+"\n");
            //            temp.Clear();
            //            n = 0;
            //        }
            //    }
            //    i++;
            //}
            //Poit = i;
            return Listr(str, font, line);
        }

        public string Up(int font, int line)
        {
            int length = font*line;
            int n = Poit;
            if (n < length)
            {
                length = n;
            }

            List<string> str = Spilt(Str.Substring(n - length, length), font);
            while (str.Count > line)
            {
                str.RemoveAt(0);
            }
            n = str.Sum(temp => temp.Length);
            Poit -= n;
            if (Poit < 0)
            {
                Poit = 0;
            }
            //List<string> str = new List<string>();
            //int i = Poit;
            //int n = 0;
            //StringBuilder temp = new StringBuilder(font);
            //while (str.Count < line)
            //{
            //    temp.Append(Str[i]);
            //    if (Str[i] == '\n')
            //    {
            //        //str.Add(temp.ToString());
            //        str.Insert(0, Sequence(temp.ToString()));
            //        str.Insert(0, "\n" );
            //        temp.Clear();
            //        n = 0;
            //    }
            //    else
            //    {
            //        n++;
            //        if (n > font)
            //        {
            //            //str.Add(temp.ToString());
            //            str.Insert(0, Sequence("\n" + temp.ToString()));
            //            temp.Clear();
            //            n = 0;
            //        }
            //    }
            //    i--;
            //    if (i < 0)
            //    {
            //        break;
            //    }
            //}
            //Poit = i;
            return Listr(str, font, line);
        }

        public async Task Read()
        {
            try
            {
                Str = await FileIO.ReadTextAsync(File);
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

        private List<string> Spilt(string str, int font)
        {
            //str = str.Replace("\r", "");
            List<string> temp = new List<string>();
            StringBuilder line = new StringBuilder(font);
            int n = 0;
            for (int i = 0; i < str.Length; i++)
            {
                line.Append(str[i]);
                n++;
                if (n >= font - 1)
                {
                    n = 0;
                    temp.Add(line.ToString());
                    line.Clear();
                }
                else if (str[i] == '\n')
                {
                    //if (n == 1 && temp.LastOrDefault() == "\n")
                    //{
                    //    n = 0;
                    //    line.Clear();
                    //}
                    //else
                    {
                        n = 0;
                        temp.Add(line.ToString());
                        line.Clear();
                    }
                }
            }

            return temp;
        }

        private string Listr(List<string> str, int font, int line)
        {
            if (str.Count == 0)
            {
                return "";
            }

            StringBuilder temp = new StringBuilder();
            //for (int i = 0; i < font; i++)
            //{
            //    temp.Append("0");
            //}


            //temp.Append("\n".PadLeft(font+font,'-'));
            for (int i = 0; i < str.Count; i++)
            {
                //if (str[i] == "\r\n" || str[i] == "\n")
                //{
                //    temp.Append("|" + " ".PadLeft(font + font) + "|" + str[i].PadLeft(font + font));
                //    //temp.Append(str[i]);
                //}
                //else
                {
                    temp.Append(str[i]);
                }
                if (!str[i].EndsWith("\n"))
                {
                    temp.Append("\n");
                }
            }

            for (int i = 0; i < line - str.Count; i++)
            {
                temp.Insert(0, "\n");
            }

            return temp.ToString();
        }

        private string Sequence(string str)
        {
            StringBuilder temp = new StringBuilder(str.Length);
            for (int i = str.Length - 1; i >= 0; i--)
            {
                temp.Append(str[i]);
            }
            return temp.ToString();
        }

        private static Encoding AutoEncoding(byte[] bom)
        {
            if (bom.Length != 4)
            {
                throw new ArgumentException();
            }
            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76)
            {
                return Encoding.UTF7;
            }
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                return Encoding.UTF8;
            }
            if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                return Encoding.Unicode; //UTF-16LE
            }
            if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                return Encoding.BigEndianUnicode; //UTF-16BE
            }
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
            {
                return Encoding.UTF32;
            }
            return Encoding.ASCII;
        }

        public int Poit
        {
            set;
            get;
        }

        [JsonIgnore]
        public StorageFile File
        {
            set;
            get;
        }

        public string Name
        {
            set;
            get;
        }
    }
}