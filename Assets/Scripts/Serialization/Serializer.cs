/*
	Serializer.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    public class Serializer
    {
        /// <summary>
        /// Saves the given data to a file of that has filename name.
        /// </summary>
        /// <typeparam name="T">The datatype of the data that will be saved.</typeparam>
        /// <param name="filename">The name of the file to be saved.</param>
        /// <param name="data">The data to be saved.</param>
        public static void Save<T>(string filename, T data) where T : class
        {
            using (Stream stream = File.OpenWrite(Application.persistentDataPath + "/" + filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                stream.Close();
            }
        }

        /// <summary>
        /// Loads the data if the file exists.
        /// </summary>
        /// <typeparam name="T">The datatype to be loaded.</typeparam>
        /// <param name="filename">The name of the file to be loaded.</param>
        /// <returns>Returns the data that was loaded.</returns>
        public static T Load<T>(string filename) where T : class
        {
            if (File.Exists(Application.persistentDataPath + "/" + filename))
            {
                try
                {
                    using (Stream stream = File.OpenRead(Application.persistentDataPath + "/" + filename))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        return formatter.Deserialize(stream) as T;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            return default(T);
        }
    }
}