using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace Capstone
{
    public class ErrorLog
    {
        public static void LogError(SqlException ex)
        {
            string directory = Environment.CurrentDirectory;
            string filename = "errorlog.csv";
            string fullPath = Path.Combine(directory, filename);

            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                sw.WriteLine(DateTime.Now + ", " + ex);
            }
        }
    }
}
