﻿#pragma warning disable


using System.Net;


class FtpClient
{
    private readonly string _server;
    private readonly string _username;
    private readonly string _password;

    public FtpClient(string server, string username, string password)
    {
        _server = server;
        _username = username;
        _password = password;
    }

    public void UploadFile(string localPath, string remotePath)
    {
        using (var client = new WebClient())
        {
            client.Credentials = new NetworkCredential(_username, _password);
            client.UploadFile($"ftp://{_server}/{remotePath}", "STOR", localPath);
        }
    }

    public void DownloadFile(string remotePath, string localPath)
    {
        using (var client = new WebClient())
        {
            client.Credentials = new NetworkCredential(_username, _password);
            client.DownloadFile($"ftp://{_server}/{remotePath}", localPath);
        }
    }

    public string[] ListDirectory(string path = "/")
    {
        var request = WebRequest.Create($"ftp://{_server}/{path}") as FtpWebRequest;
        request.Credentials = new NetworkCredential(_username, _password);
        request.Method = WebRequestMethods.Ftp.ListDirectory;

        using (var response = request.GetResponse() as FtpWebResponse)
        using (var stream = response.GetResponseStream())
        using (var reader = new StreamReader(stream))
        {
            var listing = reader.ReadToEnd();
            return listing.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}