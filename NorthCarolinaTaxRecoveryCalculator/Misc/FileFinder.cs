using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace NorthCarolinaTaxRecoveryCalculator.Misc
{
    public class FileFinder
    {
        public static string FindFile(string filename)
        {
            var path = filename;             
            if (File.Exists(path))
            {
                return path;
            }

            path = HostingEnvironment.MapPath("~/" + filename);                
            if (File.Exists(path))
            {
                return path;
            }

            path = System.AppDomain.CurrentDomain.BaseDirectory + "/" + filename;
            if (File.Exists(path))
            {
                return path;
            }

            throw new Exception("Could Not Find " + filename);
        }
    }
}