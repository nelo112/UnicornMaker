﻿using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using UnicornServer.Properties;

namespace UnicornServer.Util
{
  public class ImageHandler
  {
    private readonly string _filesDirectory;
    // Fallback in case there is nothing in the AppSettings
    const string DefaultFileLocation = "Images";
    private const string AppSettingsKey = "ImageHandler.FilesLocation";

    public ImageHandler()
    {
      var fileLocation = ConfigurationManager.AppSettings[AppSettingsKey];
      _filesDirectory = string.IsNullOrWhiteSpace(fileLocation) ? DefaultFileLocation : fileLocation;
    }

    public HttpResponseMessage GetBodyImage(int id)
    {
      return GetImageAsHttpResponse("body", id);
    }

    public HttpResponseMessage GetHatImage(int id)
    {
      return GetImageAsHttpResponse("hat", id);
    }

    public HttpResponseMessage GetWingsImage(int id)
    {
      return GetImageAsHttpResponse("wings", id);
    }

    public HttpResponseMessage GetShoesImage(int id)
    {
      return GetImageAsHttpResponse("shoes", id);
    }

    private HttpResponseMessage GetImageAsHttpResponse(string type, int id)
    {
      var filename = type + "_" + id;
      var image = (Image) Resources.ResourceManager.GetObject(filename);
      if (image == null)
      {
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
      }
      var memoryStream = new MemoryStream();
      var result = new HttpResponseMessage(HttpStatusCode.OK);
      image.Save(memoryStream, ImageFormat.Png);
      result.Content = new ByteArrayContent(memoryStream.ToArray());
      result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

      return result;
    }
  }
}