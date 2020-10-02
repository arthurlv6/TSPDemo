using System;
using System.IO;
using TSPServer.Services;
using Xunit;

namespace TSP.API.XUnitTest
{
    public class UnitTest
    {
        [Fact]
        public void ImageSave()
        {
            String path = @"D:\a.jpg";
            using (var sr = File.OpenRead(path))
            {
                var storage = new ImageStore();
                var id=storage.SaveImage(sr).Result;
            }
        }
    }
}
