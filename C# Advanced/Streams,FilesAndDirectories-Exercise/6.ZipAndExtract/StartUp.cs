namespace _6.ZipAndExtract
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string zipFile = @"..\..\..\myNewZip.zip";

            string file ="copyMe.png";

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }
    }
}
