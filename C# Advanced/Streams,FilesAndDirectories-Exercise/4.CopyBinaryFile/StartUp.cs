namespace _4.CopyBinaryFile
{
    using System;
    using System.IO;

    class StartUp
    {
        static void Main(string[] args)
        {
            string picPath = "copyMe.png";
            string picCopyPath = "copyMe-copy.png";

            using (FileStream streamReader = new FileStream(picPath,FileMode.Open))
            {
                using (FileStream streamWriter = new FileStream(picCopyPath,FileMode.Create))
                {
                    while (true)
                    {
                        byte[] byteArray = new byte[4096];
                        int size = streamReader.Read(byteArray, 0, byteArray.Length);
                        if (size == 0)
                        {
                            break;
                        }
                        streamWriter.Write(byteArray, 0, size);
                    }
                    
                }
                
            }
        }
    }
}
