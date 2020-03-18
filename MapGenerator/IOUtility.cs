using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace MinimapGen.MapGenerator
{
    public class IOUtility
    {
        public static double FromSageFloat16(ushort v)
        {
            byte upper = (byte) (v >> 8);
            byte lower = (byte) (v & 0xFF);
            return (double) (int) upper * 10f + (double) (int) lower * 9.96f / 256f;
        }

        public static void DecompressData(BinaryReader input, BinaryWriter output)
        {
            long oldPos4 = 0L;
            long newPos4 = 0L;
            byte code14 = 0;
            byte code13 = 0;
            byte code12 = 0;
            byte code11 = 0;
            int count9 = 0;
            int repeatAvailable4 = 0;
            int size = GetUncompressedSize(input);
            long i = 0L;
            while (true)
            {
                if (output.BaseStream.Length * 100 / size > i)
                {
                    i = output.BaseStream.Length * 100 / size;
                }

                code14 = input.ReadByte();
                if ((code14 & 0x80) == 0)
                {
                    code13 = input.ReadByte();
                    count9 = (code14 & 3);
                    output.Write(input.ReadBytes(count9));
                    oldPos4 = output.BaseStream.Position;
                    newPos4 = oldPos4 - 1 - (code13 + (code14 & 0x60) * 8);
                    repeatAvailable4 = (int) (oldPos4 - newPos4);
                    count9 = (code14 & 0x1C) / 4 + 3;
                    output.BaseStream.Seek(-repeatAvailable4, SeekOrigin.Current);
                    byte[] temp3 = new BinaryReader(output.BaseStream).ReadBytes(count9);
                    output.BaseStream.Seek(0L, SeekOrigin.End);
                    output.Write(temp3);
                    if (count9 > repeatAvailable4)
                    {
                        CopyRepeat(input, output, oldPos4, newPos4, count9, repeatAvailable4);
                    }
                }
                else if ((code14 & 0x40) == 0)
                {
                    code13 = input.ReadByte();
                    code12 = input.ReadByte();
                    count9 = code13 >> 6;
                    output.Write(input.ReadBytes(count9));
                    oldPos4 = output.BaseStream.Position;
                    newPos4 = oldPos4 - 1 - (((code13 & 0x3F) << 8) + code12);
                    repeatAvailable4 = (int) (oldPos4 - newPos4);
                    count9 = (code14 & 0x3F) + 4;
                    output.BaseStream.Seek(-repeatAvailable4, SeekOrigin.Current);
                    byte[] temp2 = new BinaryReader(output.BaseStream).ReadBytes(count9);
                    output.BaseStream.Seek(0L, SeekOrigin.End);
                    output.Write(temp2);
                    if (count9 > repeatAvailable4)
                    {
                        CopyRepeat(input, output, oldPos4, newPos4, count9, repeatAvailable4);
                    }
                }
                else if ((code14 & 0x20) == 0)
                {
                    code13 = input.ReadByte();
                    code12 = input.ReadByte();
                    code11 = input.ReadByte();
                    count9 = (code14 & 3);
                    output.Write(input.ReadBytes(count9));
                    oldPos4 = output.BaseStream.Position;
                    newPos4 = oldPos4 - 1 - (((code14 & 0x10) >> 4 << 16) + (code13 << 8) + code12);
                    repeatAvailable4 = (int) (oldPos4 - newPos4);
                    count9 = ((code14 & 0xC) >> 2 << 8) + code11 + 5;
                    output.BaseStream.Seek(-repeatAvailable4, SeekOrigin.Current);
                    byte[] temp = new BinaryReader(output.BaseStream).ReadBytes(count9);
                    output.BaseStream.Seek(0L, SeekOrigin.End);
                    output.Write(temp);
                    if (count9 > repeatAvailable4)
                    {
                        CopyRepeat(input, output, oldPos4, newPos4, count9, repeatAvailable4);
                    }
                }
                else
                {
                    count9 = (code14 & 0x1F) * 4 + 4;
                    if (count9 > 112)
                    {
                        break;
                    }

                    output.Write(input.ReadBytes(count9));
                }
            }

            count9 = (code14 & 3);
            output.Write(input.ReadBytes(count9));
            output.BaseStream.Seek(0L, SeekOrigin.Begin);
        }

        private static int GetUncompressedSize(BinaryReader br)
        {
            int compressedsize = 0;
            ushort headerflags = (ushort) ((br.ReadByte() << 8) + br.ReadByte());
            if ((headerflags & 0x3EFF) == 4347)
            {
                int x2 = headerflags & 0x8000;
                if (x2 > 0)
                {
                    x2 = 1;
                }

                x2 += 3;
                for (int i = 0; i < x2; i++)
                {
                    compressedsize = (compressedsize << 8) + br.ReadByte();
                }
            }

            return compressedsize;
        }


        private static void CopyRepeat(BinaryReader input, BinaryWriter output, long oldPos, long newPos, int count,
            int repeatAvailable)
        {
            int copyFromEnd = count - repeatAvailable;
            int i = 0;
            while (i < copyFromEnd)
            {
                output.BaseStream.Seek(oldPos, SeekOrigin.Begin);
                byte[] temp = new BinaryReader(output.BaseStream).ReadBytes(copyFromEnd - i);
                oldPos += temp.Length;
                i += temp.Length;
                output.BaseStream.Seek(0L, SeekOrigin.End);
                output.Write(temp);
            }
        }

        public static void SaveTGA(Bitmap img, string filePath)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(filePath));
            bw.Write(131072u);
            bw.Write(0);
            bw.Write(0);
            short width = (short) img.Width;
            short height = (short) img.Height;
            bw.Write(width);
            bw.Write(height);
            bw.Write((short) 32);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bw.Write(img.GetPixel(x, height - 1 - y).ToArgb());
                }
            }

            bw.Close();
        }


        public static T[,] ReadArray<T>(BinaryReader br, int width, int height) where T : struct
        {
            T[,] array = new T[height, width];
            Type type = typeof(T);
            if (type == typeof(bool))
            {
                byte temp = 0;
                for (int y2 = 0; y2 < height; y2++)
                {
                    for (int x2 = 0; x2 < width; x2++)
                    {
                        if (x2 % 8 == 0)
                        {
                            temp = br.ReadByte();
                        }

                        array[y2, x2] = (T) (object) ((temp & (1 << x2 % 8)) > 0);
                    }
                }
            }
            else
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (type == typeof(int))
                        {
                            array[y, x] = (T) (object) br.ReadInt32();
                            continue;
                        }

                        if (type == typeof(short))
                        {
                            array[y, x] = (T) (object) br.ReadInt16();
                            continue;
                        }

                        if (type == typeof(ushort))
                        {
                            array[y, x] = (T) (object) br.ReadUInt16();
                            continue;
                        }

                        if (type == typeof(byte))
                        {
                            array[y, x] = (T) (object) br.ReadByte();
                            continue;
                        }

                        throw new Exception($"Type: {type.Name} is not supported for method ReadArray");
                    }
                }
            }

            return array;
        }

        public static void writeData(bool[,] array)
        {
            StreamWriter writer = new StreamWriter("impassable.txt");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (j > 0)
                    {
                        writer.Write(",");
                    }

                    writer.Write(array[i, j]);
                }

                writer.Write("\r\n");
            }

            writer.Close();
        }

        public static bool[,] getDataInField(bool[,] data, int borderWidth)
        {
            bool[,] result = new bool[data.GetLength(0) - 2 * borderWidth, data.GetLength(1) - 2 * borderWidth];
            for (int i = borderWidth; i < data.GetLength(0) - borderWidth; i++)
            {
                for (int j = borderWidth; j < data.GetLength(1) - borderWidth; j++)
                {
                    result[i - borderWidth, j - borderWidth] = data[i, j];
                }
            }

            return result;
        }

        public static Color interpolateColor(double height, double height1, double height2, Color color1, Color color2)
        {
            double gap = (height2 - height1);
            int diffR = color2.R - color1.R;
            int diffG = color2.G - color1.G;
            int diffB = color2.B - color1.B;
            double ratio = (height - height1) / gap;
            int resR = (int) (ratio * diffR) + color1.R;
            int resG = (int) (ratio * diffG) + color1.G;
            int resB = (int) (ratio * diffB) + color1.B;
            // if (resR > 256)
            // {
            //     resR = 30;
            // }
            return Color.FromArgb(255, resR, resG, resB);
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public static string[] getFilesNames(string[] mapNames)
        {
            string[] result=new string[mapNames.Length];
            for (int i = 0; i < mapNames.Length; i++)
            {
                result[i] = Path.GetFileName(mapNames[i]);
            }

            return result;
        }

        public static String checkUpdate(string version)
        {
            WebClientEx webClient = new WebClientEx();
            try
            {
                string NewVersion = webClient.DownloadString("https://raw.githubusercontent.com/wu162/Ra3MinimapGenerator/master/version.txt");
                if (NewVersion.Equals(version))
                {
                    return null;
                }
                else
                {
                    string downloadUrl = webClient.DownloadString("https://raw.githubusercontent.com/wu162/Ra3MinimapGenerator/master/downloadUrl.txt");
                    return downloadUrl;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static int findNearHeight(double target, double[] heights)
        {
            double min = Math.Abs(target-heights[0]);
            int index = 0;
            for (int i = 0; i < heights.Length; i++)
            {
                if (Math.Abs(target-heights[i]).CompareTo(min)==-1)
                {
                    min = Math.Abs(target - heights[i]);
                    index = i;
                }
            }

            if (target.CompareTo(heights[index])==-1)
            {
                index--;
            }

            return index;
        }

        public static bool needInterpolate(double target, double[] heights)
        {
            for (int i = 0; i < heights.Length; i++)
            {
                if (target.Equals(heights[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}