﻿using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ToursimImagesService.Interfaces;
using ToursimImagesService.Models;
using ToursimImagesService.Models.DTOs;
using ToursimImagesService.Respositories;

namespace ToursimImagesService.Services
{

    // In your ToursImageServices class
    // In your ToursImageServices class
    public class ToursImageServices : ITourImageService
    {
    
        private readonly IRepo<int, TourImages> _tourImageRepository;

        public ToursImageServices(
         
            IRepo<int, TourImages> tourImageRepository)
        {
            
            _tourImageRepository = tourImageRepository;
        }

        public async Task<ICollection<ImageDTO>> GettingAllImagesByTourId(int tourId)
        {
            var allTourImages=await _tourImageRepository.GetAll();
            var images = allTourImages.Where(a => a.TourId == tourId).Select(x=>new ImageDTO { ImagePaths = x.ImagePaths });
            if(images!=null)
            {
                return images.ToList();
            }
            throw new Exception("no images found");
        }

        public async Task UploadImagesAsync(TourImages tourImages, List<IFormFile> images)
        {
            
            // Connect to Azure Blob Storage
            string connectionString = "BlobEndpoint=http://127.0.0.1:8888/devstoreaccount1;SharedAccessSignature=sv=2021-10-04&ss=btqf&srt=sco&st=2023-08-09T17%3A25%3A27Z&se=2023-08-14T17%3A25%3A00Z&sp=rwdflacu&sig=03LZnT0%2FbO28uzmzGlSIPWmZrhR6WdXZwJf9Cr51ImU%3D";

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("samples-workitems");

            // Create the container if it doesn't exist
            containerClient.CreateIfNotExists();

            foreach (var image in images)
            {
                // Generate a unique blob name
                string uniqueBlobName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // Upload the image to Azure Blob Storage
                BlobClient blobClient = containerClient.GetBlobClient(uniqueBlobName);
                using (var stream = image.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

              

                // Create a new image entry and associate it with the tour
                TourImages tourImage = new TourImages
                {
                    TourId = tourImages.TourId,
                    ImagePaths = blobClient.Uri.ToString(), // Store Blob Storage URL

                };

                await _tourImageRepository.Add(tourImage);
            }
        }
    }
}
