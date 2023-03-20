using Microsoft.AspNetCore.Http;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Exercise1.Service
{
    public class FileService
    {
        public async Task<string> uploadFile(IFormFile file, string url)
        {
            string fileName = file.FileName;
            try
            {
                var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\File");
                if (!Directory.Exists(pathBuild))
                {
                    Directory.CreateDirectory(pathBuild);
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\File", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var fileDownload = url.Split("/")[url.Split("/").Length - 1];
                var fileNameDownload = fileDownload.Split(".")[0];
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, "Upload\\File\\" + fileDownload);
                }

                Mp3ToWav("Upload\\File\\" + fileDownload, $"Upload\\File\\{fileNameDownload}.wav");

            }
            catch (Exception e)
            {
            }
            return fileName;
        }

        public void Mp3ToWav(string mp3File, string outputFile)
        {
            using (Mp3FileReader reader = new Mp3FileReader(mp3File))
            {
                using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
                }
            }
        }
    }
}
