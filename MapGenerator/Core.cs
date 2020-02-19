using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MinimapGen.MapGenerator
{
    public class Core
    {
        private int mapWidth;
        private int mapHeight;
        private int borderWidth;
        private int playableWidth;
        private int playableHeight;

        private double[,] heightMapData;

        // private static int[] pixels;
        private bool[,] passability;
        private bool[,] impassable;
        private string[] mapNames;
        private string filepath;
        private string savePath;

        public string SavePath
        {
            get => savePath;
            set => savePath = Path.Combine(rootPath, value, value + "_art.tga");
        }

        public string Filepath
        {
            get => filepath;
            set => filepath = Path.Combine(rootPath, value, value + ".map");
        }

        private Bitmap minimap;
        private string rootPath;

        public Bitmap Minimap => minimap;

        public string[] MapNames => mapNames;

        public Core()
        {
            filepath = "";
            findMaps();
        }

        private void findMaps()
        {
            rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Red Alert 3", "Maps");

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            mapNames = Directory.GetDirectories(rootPath);
        }

        public bool main()
        {
            if (filepath == "")
            {
                return false;
            }

            readData(filepath);

            minimap = GenMinimap();
            return true;
        }

        private void readData(string filepath)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(filepath));

            switch (br.ReadUInt32())
            {
                case 1884121923u:
                    break;
                case 5390661u:
                {
                    BinaryWriter bWriter = new BinaryWriter(new MemoryStream((int) br.BaseStream.Length));
                    br.BaseStream.Position = 8L;
                    IOUtility.DecompressData(br, bWriter);
                    br = new BinaryReader(bWriter.BaseStream);
                    br.BaseStream.Position = 4L;
                    break;
                }
            }

            br.BaseStream.Position = 4L;
            string[] assetStrings = new string[br.ReadInt32()];

            for (int i = assetStrings.Length - 1; i >= 0; i--)
            {
                assetStrings[i] = br.ReadString();
                int suffix = br.ReadInt32();
            }

            int readcount = 2;
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                int id = br.ReadInt32();
                short header = br.ReadInt16();
                int length = br.ReadInt32();
                string s = assetStrings[id - 1];
                br.BaseStream.Position -= 10L;
                // br.ReadBytes(length + 10);
                // br.BaseStream.Position -= length + 10;
                switch (s)
                {
                    case "HeightMapData":
                        readheightmap(br);
                        readcount--;
                        break;
                    case "BlendTileData":
                        readBlendTileData(br);
                        readcount--;
                        break;
                    default:
                        br.ReadBytes(length + 10);
                        break;
                }

                if (readcount == 0)
                {
                    break;
                }
            }

            br.Close();
        }

        private void readBlendTileData(BinaryReader br)
        {
            br.ReadInt32();
            br.ReadInt16();
            br.ReadInt32();
            int area = br.ReadInt32();
            IOUtility.ReadArray<ushort>(br, mapWidth, mapHeight);
            IOUtility.ReadArray<ushort>(br, mapWidth, mapHeight);
            IOUtility.ReadArray<ushort>(br, mapWidth, mapHeight);
            IOUtility.ReadArray<ushort>(br, mapWidth, mapHeight);
            bool[,] data = IOUtility.ReadArray<bool>(br, mapWidth, mapHeight);
            impassable = IOUtility.getDataInField(data, borderWidth);

            // StreamWriter writer = new StreamWriter("impassable.txt");
            // for (int i = 0; i < impassable.GetLength(0); i++)
            // {
            //     for (int j = 0; j < impassable.GetLength(1); j++)
            //     {
            //         if (j > 0)
            //         {
            //             writer.Write(",");
            //         }
            //
            //         writer.Write(impassable[i, j] ? "#####" : "1");
            //     }
            //
            //     writer.Write("\r\n");
            // }
            //
            // writer.Close();

            // IOUtility.writeData(impassable);
        }

        private void readheightmap(BinaryReader br)
        {
            br.ReadInt32();
            br.ReadInt16();
            br.ReadInt32();
            mapWidth = br.ReadInt32();
            mapHeight = br.ReadInt32();

            borderWidth = br.ReadInt32();
            int blockType = br.ReadInt32();
            byte[] unknownBlock = br.ReadBytes((blockType - 1) * 16 + 8);
            playableWidth = br.ReadInt32();
            playableHeight = br.ReadInt32();
            int area = br.ReadInt32();

            heightMapData = new double[mapHeight - 2 * borderWidth, mapWidth - 2 * borderWidth];

            //此处读出来高度图是进行了y轴翻转过的
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    double data = IOUtility.FromSageFloat16(br.ReadUInt16());

                    if (inPlayField(x, y))
                    {
                        heightMapData[y - borderWidth, x - borderWidth] = data;
                    }
                }
            }

            //计算真正的可玩区域
            playableWidth = mapWidth - 2 * borderWidth;
            playableHeight = mapHeight - 2 * borderWidth;
        }

        private Bitmap GenMinimap()
        {
            bool[,] impassableGen = impassable;
            if (MainWindow.ExpandEdge)
            {
                impassableGen = expand(impassable);
            }

            double waterHeight = 200.0;
            Bitmap bitmap = new Bitmap(playableWidth, playableHeight, PixelFormat.Format32bppArgb);
            Color water = Color.FromArgb(68, 94, 106);
            Color[] colors = MapHelper.getColors(MainWindow.Style1);
            double[] heights = findHeights();
            for (int i = 0; i < playableHeight; i++)
            {
                for (int j = 0; j < playableWidth; j++)
                {
                    if (heightMapData[i, j] - waterHeight < 0.00001)
                    {
                        bitmap.SetPixel(j, playableHeight - i - 1, water);
                        continue;
                    }

                    if (impassableGen[i, j])
                    {
                        bitmap.SetPixel(j, playableHeight - i - 1, Color.Black);
                        continue;
                    }

                    //平坦陆地着色
                    bool notColored = true;
                    for (int k = 0; k < heights.Length; k++)
                    {
                        if (heightMapData[i, j].Equals(heights[k]))
                        {
                            bitmap.SetPixel(j, playableHeight - i - 1, colors[k]);
                            notColored = false;
                            break;
                        }
                        else if (heightMapData[i, j] - heights[0] > 0.0001 && k < heights.Length - 1 &&
                                 !heightMapData[i, j].Equals(heights[k + 1]))
                        {
                            Color median = IOUtility.interpolateColor(heightMapData[i, j], heights[k], heights[k + 1],
                                colors[k], colors[k + 1]);
                            bitmap.SetPixel(j, playableHeight - i - 1, median);
                            notColored = false;
                            break;
                        }
                    }

                    if (notColored)
                    {
                        bitmap.SetPixel(j, playableHeight - i - 1, Color.White);
                    }
                }
            }

            // bitmap.Save("map preview.png", ImageFormat.Png);
            return bitmap;
        }

        private bool[,] expand(bool[,] array)
        {
            int[,] array1 = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j])
                    {
                        array1[i, j] = 1;
                    }
                    else
                    {
                        array1[i, j] = 0;
                    }
                }
            }

            int[] kernel = new int[] {-1, 0, 1};
            for (int i = 1; i < array1.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < array1.GetLength(1) - 1; j++)
                {
                    if (array1[i, j] == 1)
                    {
                        for (int k = 0; k < kernel.Length; k++)
                        {
                            for (int l = 0; l < kernel.Length; l++)
                            {
                                int x1 = i + k;
                                int y1 = j + l;
                                if (x1 > 0 && x1 < array1.GetLength(0) && y1 > 0 && y1 < array1.GetLength(1))
                                {
                                    array1[x1, y1] = 2;
                                }
                            }
                        }
                    }
                }
            }

            bool[,] result = new bool[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array1[i, j] == 2 || array1[i, j] == 1)
                    {
                        result[i, j] = true;
                    }
                }
            }

            return result;
        }

        private bool inPlayField(int x, int y)
        {
            return x >= borderWidth
                   && x < mapWidth - borderWidth
                   && y >= borderWidth
                   && y < mapHeight - borderWidth;
        }

        public double[] findHeights()
        {
            ArrayList heights = new ArrayList();
            double[] data = new double[heightMapData.Length];
            for (int i = 0; i < heightMapData.GetLength(0); i++)
            {
                for (int j = 0; j < heightMapData.GetLength(1); j++)
                {
                    data[j + i * heightMapData.GetLength(1)] = heightMapData[i, j];
                }
            }

            // Buffer.BlockCopy(heightMapData,0,data,0,heightMapData.Length);
            double max = data.Max();
            IEnumerable<IGrouping<double, double>> groupBy = data.GroupBy(e => e)
                .Where(e => e.Key > 200.0)
                .Where(e => e.Key - (int) e.Key < 0.001)
                .Where(e => e.Count() > 100);
            foreach (IGrouping<double, double> grouping in groupBy)
            {
                heights.Add(grouping.Key);
            }

            return (double[]) heights.ToArray(typeof(double));
        }
    }
}