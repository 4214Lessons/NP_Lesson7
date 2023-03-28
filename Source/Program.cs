#pragma warning disable

using System.Net;


// RequestFtpServer();
// UploadFile();
// DownloadFile();



// var ftpClient = new FtpClient("localhost", string.Empty, string.Empty);
// ftpClient.ListDirectory().ToList().ForEach(x => Console.WriteLine(x));



void RequestFtpServer()
{
    var request = WebRequest.Create("ftp://localhost:21") as FtpWebRequest;
    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

    using var response = request.GetResponse() as FtpWebResponse;
    using var stream = response.GetResponseStream();

    using var reader = new StreamReader(stream);

    var data = reader.ReadToEnd();
    Console.WriteLine(data);
}


void UploadFile()
{
    var request = WebRequest.Create("ftp://localhost:21/uploadedFile.txt") as FtpWebRequest;
    request.Method = WebRequestMethods.Ftp.UploadFile;

    using var requestStream = request.GetRequestStream();

    using var fs = new FileStream("destination.txt", FileMode.Open);
    fs.CopyTo(requestStream);
}


void DownloadFile()
{
    var request = WebRequest.Create("ftp://localhost:21/source.txt") as FtpWebRequest;
    request.Method = WebRequestMethods.Ftp.DownloadFile;

    using var response = request.GetResponse() as FtpWebResponse;
    using var stream = response?.GetResponseStream();

    using var fs = new FileStream("source.txt", FileMode.Create);
    stream.CopyTo(fs);
}