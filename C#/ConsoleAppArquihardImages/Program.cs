using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace ConsoleAppArquihardImages
{
    class Program
    {
        private static string towriteFile;

        static void Main(string[] args)
        {
            get60Sample();
            //runExperimentScenarios();
            //setInverterImages();
        }

        public static void setInverterImages() 
        {

            for (int i=1;i<=8 ;i++) 
            {
                runExperiment(1, i);
            }
            
        }

        public static void get60Sample() 
        {
            for (int imagenes = 1; imagenes <= 8; imagenes++)
            {

                for (int algoritmo = 1; algoritmo <= 5; algoritmo++)
                {
                    Console.WriteLine("[" + algoritmo + "," + imagenes + "]");
                    for (int i=0; i<60;i++) 
                    {
                        towriteFile += ""+runExperiment(algoritmo,imagenes)+",";
                    }
                    towriteFile +="\n";
                }
            }

            System.IO.File.WriteAllText(@"resultcsv.csv", towriteFile);
        }

        public static void runExperimentScenarios()
        {
    
            for (int imagenes = 1; imagenes<=8 ;imagenes++) 
            {
                towriteFile += "=======Image version "+(imagenes)+"=======\n";

                for (int algoritmo=1;algoritmo<=5 ;algoritmo++) 
                {
                    towriteFile += "=====Algoritmo version "+(algoritmo)+"=====\n";

                    towriteFile += "" + runExperiment(algoritmo, imagenes) + "," + runExperiment(algoritmo, imagenes) + "," + runExperiment(algoritmo, imagenes)+"\n"; 

                }
            }

            System.IO.File.WriteAllText(@"resultcsv.csv", towriteFile);
        }

        public static long runExperiment(int algoritmo, int tamanoImagen)
        {
            string imagePath = @"images\grabber";
            long timeElapsed = 0;

            // CHANGE THIS TO TRUE IF YOU WANT TO SAVE THE INVERTED IMAGE ON THE SAME FOLDER AS THE ORIGINAL
            Boolean saveInverted = false;

            switch (tamanoImagen)
            {
                case 1:
                    imagePath = imagePath + "1";
                    break;

                case 2:
                    imagePath = imagePath + "2";
                    break;

                case 3:
                    imagePath = imagePath + "3";
                    break;

                case 4:
                    imagePath = imagePath + "4";
                    break;

                case 5:
                    imagePath = imagePath + "5";
                    break;

                case 6:
                    imagePath = imagePath + "6";
                    break;

                case 7:
                    imagePath = imagePath + "7";
                    break;

                case 8:
                    imagePath = imagePath + "8";
                    break;
            }
            imagePath = imagePath + ".bmp";

            int invertedPathNumber = 0;
            if (saveInverted)
            {
                invertedPathNumber = tamanoImagen;
            }

            switch (algoritmo)
            {
                case 1:
                    timeElapsed = algorithmVersion1(imagePath, invertedPathNumber);
                    break;

                case 2:
                    timeElapsed = algorithmVersion2(imagePath, invertedPathNumber);
                    break;

                case 3:
                    timeElapsed = algorithmVersion3(imagePath, invertedPathNumber);
                    break;

                case 4:
                    timeElapsed = algorithmVersion4(imagePath, invertedPathNumber);
                    break;

                case 5:
                    timeElapsed = algorithmVersion5(imagePath, invertedPathNumber);
                    break;
            }

            return timeElapsed;
        }

        public static long algorithmVersion1(string imagePath, int invertedPath)
        {
            long timeElapsed = 0;
            Bitmap pic = new Bitmap(Image.FromFile(imagePath));
            Stopwatch stopw = new Stopwatch();
            stopw.Restart();
            stopw.Start();

            for (int rows = 0; rows <= (pic.Width - 1); rows++)
            {
                for (int columns = 0; columns <= (pic.Height - 1); columns++)
                {
                    Color inv = pic.GetPixel(rows, columns);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(rows, columns, inv);
                }
            }
            stopw.Stop();
            timeElapsed = (long)(stopw.Elapsed.TotalMilliseconds * 1000000);

            if (invertedPath != 0)
            {
                Image image = (Image)pic;
                image.Save(@"images\grabberInverted" + invertedPath + ".bmp", ImageFormat.Bmp);
            }
            return timeElapsed;
        }

        public static long algorithmVersion2(string imagePath, int invertedPath)
        {
            long timeElapsed = 0;
            Bitmap pic = new Bitmap(Image.FromFile(imagePath));
            Stopwatch stopw = new Stopwatch();
            stopw.Restart();
            stopw.Start();

            for (int rows = 0; rows <= (pic.Width - 1); rows++)
            {
                for (int columns = 0; columns <= (pic.Height - 1); columns++)
                {
                    Color inv = pic.GetPixel(rows, columns);
                    inv = Color.FromArgb(255, (255 - inv.R), inv.G, inv.B);
                    pic.SetPixel(rows, columns, inv);
                }
            }
            for (int rows = 0; rows <= (pic.Width - 1); rows++)
            {
                for (int columns = 0; columns <= (pic.Height - 1); columns++)
                {
                    Color inv = pic.GetPixel(rows, columns);
                    inv = Color.FromArgb(255, inv.R, (255 - inv.G), inv.B);
                    pic.SetPixel(rows, columns, inv);
                }
            }
            for (int rows = 0; rows <= (pic.Width - 1); rows++)
            {
                for (int columns = 0; columns <= (pic.Height - 1); columns++)
                {
                    Color inv = pic.GetPixel(rows, columns);
                    inv = Color.FromArgb(255, inv.R, inv.G, (255 - inv.B));
                    pic.SetPixel(rows, columns, inv);
                }
            }

            stopw.Stop();
            timeElapsed = (long)(stopw.Elapsed.TotalMilliseconds * 1000000);
            if (invertedPath != 0)
            {
                Image image = (Image)pic;
                image.Save(@"images\grabberInverted" + invertedPath + ".bmp", ImageFormat.Bmp);
            }
            return timeElapsed;
        }

        public static long algorithmVersion3(string imagePath, int invertedPath)
        {
            long timeElapsed = 0;
            Bitmap pic = new Bitmap(Image.FromFile(imagePath));
            Stopwatch stopw = new Stopwatch();
            stopw.Restart();
            stopw.Start();

            for (int columns = 0; columns <= (pic.Height - 1); columns++)
            {
                for (int rows = 0; rows <= (pic.Width - 1); rows++)
                {
                    Color inv = pic.GetPixel(rows, columns);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(rows, columns, inv);
                }
            }

            stopw.Stop();
            timeElapsed = (long)(stopw.Elapsed.TotalMilliseconds * 1000000);
            if (invertedPath != 0)
            {
                Image image = (Image)pic;
                image.Save(@"images\grabberInverted" + invertedPath + ".bmp", ImageFormat.Bmp);
            }
            return timeElapsed;
        }

        public static long algorithmVersion4(string imagePath, int invertedPath)
        {
            long timeElapsed = 0;
            Bitmap pic = new Bitmap(Image.FromFile(imagePath));
            Stopwatch stopw = new Stopwatch();
            stopw.Restart();
            stopw.Start();


            for (int i = 0; i < pic.Width; i++)
            {
                for (int j = 0; j < pic.Height; j++)
                {
                    Color pixel = pic.GetPixel(i, j);
                    byte red = (byte)(255 - pixel.R);
                    Color newCo = Color.FromArgb(red, pixel.G, pixel.B);
                    pic.SetPixel(i, j, newCo);
                }
            }

            for (int i = pic.Width - 1; i >= 0; i--)
            {
                for (int j = pic.Height - 1; j >= 0; j--)
                {
                    Color pixel = pic.GetPixel(i, j);
                    byte green = (byte)(255 - pixel.G);
                    byte blue = (byte)(255 - pixel.B);
                    Color newCo = Color.FromArgb(pixel.R, green, blue);
                    pic.SetPixel(i, j, newCo);
                }
            }


            stopw.Stop();
            timeElapsed = (long)(stopw.Elapsed.TotalMilliseconds * 1000000);
            if (invertedPath != 0)
            {
                Image image = (Image)pic;
                image.Save(@"images\grabberInverted" + invertedPath + ".bmp", ImageFormat.Bmp);
            }
            return timeElapsed;
        }

        public static long algorithmVersion5(string imagePath, int invertedPath)
        {
            long timeElapsed = 0;
            Bitmap pic = new Bitmap(Image.FromFile(imagePath));
            Stopwatch stopw = new Stopwatch();
            stopw.Restart();
            stopw.Start();


            for (int i = 0; i < pic.Width - 1; i += 2)
            {
                for (int j = 0; j < pic.Height - 1; j += 2)
                {
                    Color pixel = pic.GetPixel(i, j);
                    byte red = (byte)(255 - pixel.R);
                    byte green = (byte)(255 - pixel.G);
                    byte blue = (byte)(255 - pixel.B);
                    Color newCo = Color.FromArgb(red, green, blue);
                    pic.SetPixel(i, j, newCo);

                    Color pixel2 = pic.GetPixel(i + 1, j);
                    byte red2 = (byte)(255 - pixel2.R);
                    byte green2 = (byte)(255 - pixel2.G);
                    byte blue2 = (byte)(255 - pixel2.B);
                    Color newCo2 = Color.FromArgb(red2, green2, blue2);
                    pic.SetPixel(i + 1, j, newCo2);

                    Color pixel3 = pic.GetPixel(i, j + 1);
                    byte red3 = (byte)(255 - pixel3.R);
                    byte green3 = (byte)(255 - pixel3.G);
                    byte blue3 = (byte)(255 - pixel3.B);
                    Color newCo3 = Color.FromArgb(red3, green3, blue3);
                    pic.SetPixel(i, j + 1, newCo3);

                    Color pixel4 = pic.GetPixel(i + 1, j + 1);
                    byte red4 = (byte)(255 - pixel4.R);
                    byte green4 = (byte)(255 - pixel4.G);
                    byte blue4 = (byte)(255 - pixel4.B);
                    Color newCo4 = Color.FromArgb(red4, green4, blue4);
                    pic.SetPixel(i + 1, j + 1, newCo4);

                }

            }


            stopw.Stop();
            timeElapsed = (long)(stopw.Elapsed.TotalMilliseconds * 1000000);
            if (invertedPath != 0)
            {
                Image image = (Image)pic;
                image.Save(@"images\grabberInverted" + invertedPath + ".bmp", ImageFormat.Bmp);
            }
            return timeElapsed;
        }
    }
}
