using System;
using System.IO;
namespace WordsLoading
{
    /// <summary>
    /// Utilite to open text files 
    /// </summary>
    public static class FileOpener
    {
        /// <summary>
        /// Opens file at given directory
        /// </summary>
        /// <param name="path"> Path to file </param>
        /// <param name="isSuccesfull"> Was the file opening successfull </param>
        /// <param name="msg"> Error text if something went wrong </param>
        /// <returns> Stream </returns>
        public static Stream OpenFile(string path, out bool isSuccesfull, out string msg)
        {
            FileStream fs;

            try
            {
                fs = new FileStream(path, FileMode.Open);
            }
            catch (Exception e)
            {
                isSuccesfull = false;
                msg = e.Message;
                return null;
            }

            isSuccesfull = true;
            msg = "Success";

            return fs;
        }
    }
}