using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Net;

namespace Task2
{

    public interface Filedata
    {
        int UpdateFilePermission(string newFilePermission, string secret);
        int AppendDate(int data);
        
        string FileName
        {
            get;
        }

        string FileExtension
        {
            get;
        }

    }


    class PDF : Filedata
    {
        private string fileName;
        private string filePermission;
        private int fileSize;
        private string fileExtension;


        public string Print()
        {
            return "Parse PDF";
        }

  
        //Constructor
        public PDF(string newName)
        {
            fileName = newName;
            FileExtension = "PDF";
            filePermission = "";
        }


        //{"FileExtension":"PDF","FileName":"Document","FilePermission":"NOTSET","FileSize":0}

        [JsonConstructor]
        public PDF(string filename, int filesize, string filepermission) :this(filename)
        {
            this.FileSize = filesize;
            this.UpdateFilePermission(filepermission, "UltraSecretPassword");
        }



        // public property
        public string FileExtension
        {
            get // this code is executed when the value of a property is read
            {
                return fileExtension;
            }
            set // this code is executed when a value is assigned to the property
            {
                // the keyword 'value' represents the value that is assigned
                if (value.Length != 3) throw new Exception("Extension must be 3 characters long.");
                fileExtension = value;
            }
        }

        // Read Only
        public string FileName => fileName;


        public string FilePermission
        {
            get // this code is executed when the value of a property is read
            {
                if (filePermission == "")
                {
                    return "NOTSET";
                }
                return filePermission;
            }
        }


        // public property
        public int FileSize
        {
            get // this code is executed when the value of a property is read
            {
                return fileSize;
            }
            set // this code is executed when a value is assigned to the property
            {
                // the keyword 'value' represents the value that is assigned
                if (value < 0) throw new Exception("Size must be greater than 0");
                fileSize = value;
            }
        }

        // public method
        public int UpdateFilePermission(string newFilePermission, string secret)
        {
            if (secret == "UltraSecretPassword")
            {
                filePermission = newFilePermission;
                return 0;
            }

            return 1;
        }


        // public method
        public int AppendDate(int data)
        {
            if (data < 0)   return 0;

            fileSize += data;

            return 1;
        }


    }



    class Audio : Filedata
    {
        private string fileName;
        private string filePermission;
        private int fileSize;
        private string fileExtension;

        //Constructor
        public Audio(string newName)
        {
            fileName = newName;
            FileExtension = "wav";
            filePermission = "RW-R--R--";
        }


        public string GetSoundData()
        {
            return "Return Sound Data";
        }


        // public property
        public string FileExtension
        {
            get // this code is executed when the value of a property is read
            {
                return fileExtension;
            }
            set // this code is executed when a value is assigned to the property
            {
                // the keyword 'value' represents the value that is assigned
                if (value.Length != 3) throw new Exception("Extension must be 3 characters long.");
                fileExtension = value;
            }
        }


        public string FileName => fileName;


        public string FilePermission
        {
            get // this code is executed when the value of a property is read
            {
                if (filePermission == "")
                {
                    return "NOTSET";
                }
                return filePermission;
            }
        }


        // public property
        public int FileSize
        {
            get // this code is executed when the value of a property is read
            {
                return fileSize;
            }
            set // this code is executed when a value is assigned to the property
            {
                // the keyword 'value' represents the value that is assigned
                if (value < 0) throw new Exception("Size must be greater than 0");
                fileSize = value;
            }
        }

        // public method
        public int UpdateFilePermission(string newFilePermission, string secret)
        {
            if (secret == "UltraSecretPassword")
            {
                filePermission = newFilePermission;
                return 0;
            }

            return 1;
        }


        // public method
        public int AppendDate(int data)
        {
            if (data < 0) return 0;

            fileSize += data;

            return 1;
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            //TASK2
            PDF data1 = new PDF("ProjectDescription");

            Console.WriteLine($"Filename: {data1.FileName}.{data1.FileExtension}  Size:{data1.FileSize} Permission:{data1.FilePermission}");

            Console.WriteLine("Change Permission");
            data1.UpdateFilePermission("RWX------", "UltraSecretPassword");
            Console.WriteLine($"Filename: {data1.FileName}.{data1.FileExtension}  Size:{data1.FileSize} Permission:{data1.FilePermission}");

            Console.WriteLine("Change Size");
            data1.FileSize = 500;
            Console.WriteLine($"Filename: {data1.FileName}.{data1.FileExtension}  Size:{data1.FileSize} Permission:{data1.FilePermission}");

            Console.WriteLine("Append data");
            data1.AppendDate(12);
            Console.WriteLine($"Filename: {data1.FileName}.{data1.FileExtension}  Size:{data1.FileSize} Permission:{data1.FilePermission}");
            //TASK2

            Console.WriteLine("---------------------");
            //TASK3

            Filedata[] directory = new Filedata[] { new Audio("Mozart"), new PDF("Document"), new Audio("Sounds"), new PDF("PrjDescr") };

            foreach (Filedata element in directory)
            {
                Console.WriteLine($"Current File:  {element.FileName}.{element.FileExtension} ");
            }
            //TASK3

            Console.WriteLine("---------------------");
            //TASK4

            var directory2 = new PDF[] { new PDF("Document"), new PDF("PrjDescr") };

            directory2[1].FileSize = 50;
            directory2[1].UpdateFilePermission("RWX------", "UltraSecretPassword");



            Console.WriteLine("---Serialize Object to JSON String");
            string json = JsonConvert.SerializeObject(directory2);
            Console.WriteLine("---Print JSON Variable");
            Console.WriteLine(json);

            Console.WriteLine("---Write JSON Variable to C:\\PDF.json");
            File.WriteAllText(@"PDF.json", json);
            string content = File.ReadAllText(@"PDF.json");

            Console.WriteLine("---Print Content from PDF.json");
            Console.WriteLine(content);

            Console.WriteLine("---Deserialize Object from JSON File");
            PDF[] directory3 = JsonConvert.DeserializeObject<PDF[]>(content);

            Console.WriteLine("---Printt Deserialize JSON");

            foreach (PDF element in directory3)
            { 
                Console.WriteLine($"Filename: {element.FileName}.{element.FileExtension}  Size:{element.FileSize} Permission:{element.FilePermission}");
            }


            //TASK4

            Console.WriteLine("---------------------");
            //TASK6
            var producer = new Subject<PDF>();

            producer
                .Subscribe(x => Console.WriteLine($"received New File :{x.FileName}.{x.FileExtension} size: {x.FileSize}"));

            producer
                .Where(x => x.FileSize < 30)
                .Subscribe(x => Console.WriteLine($"Small File"));
            producer
                .Where(x => x.FileSize >= 30)
                .Where(x => x.FileSize < 70)
                .Subscribe(x => Console.WriteLine($"Medium File"));

            producer
                .Where(x => x.FileSize >= 70)
                .Subscribe(x => Console.WriteLine($"Big File"));

            Random rnd = new Random();
            for (var i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));

                //if(rnd.Next(1, 100)%2 != 0)
                producer.OnNext(new PDF("PDF Nr." + i, rnd.Next(1, 100), "RWX------")); // push value i to subscribers

            }

            //-----------T6.2

            // Task<string> futureData = new WebClient().DownloadStringAsync(url);
            // futureData.ContinueWith(t => Console.WriteLine(t.Result));


            //string data = await new WebClient().DownloadStringAsync(url);
            //Console.WriteLine(data);

            //TASK6


        }
    }
}
