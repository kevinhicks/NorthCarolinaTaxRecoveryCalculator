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
            var TriedPaths = new List<string>();

            var path = filename;             
            if (File.Exists(path))
            {
                return path;
            }
            TriedPaths.Add(path);

            path = HostingEnvironment.MapPath("~/" + filename);                
            if (File.Exists(path))
            {
                return path;
            }
            TriedPaths.Add(path);

            path = System.AppDomain.CurrentDomain.BaseDirectory + "/" + filename;
            if (File.Exists(path))
            {
                return path;
            }
            TriedPaths.Add(path);

            //Buidl an error msg
            string msg = "Could Not Find " + filename + " in:\n";
            foreach (var p in TriedPaths)
            {
                msg += p + "\n";
            }
            throw new Exception(msg);
        }
    }
}