﻿// Controllers/UploadController.cs
using Microsoft.AspNetCore.Mvc;
using ABCRetails.Models;
using ABCRetails.Services;

namespace ABCRetailers.Controllers
{
    public class UploadController : Controller
    {
        private readonly IAzureStorageService _storageService; // Added underscore

        public UploadController(IAzureStorageService storageService)
        {
            _storageService = storageService; // Added underscore
        }

        public IActionResult Index()
        {
            return View(new FileUploadModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FileUploadModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ProofOfPayment != null && model.ProofOfPayment.Length > 0)
                    {
                        // Upload to blob storage
                        var fileName = await _storageService.UploadFileAsync(model.ProofOfPayment, "payment-proofs"); // Added underscore

                        // Also upload to file share for contracts
                        await _storageService.UploadToFileShareAsync(model.ProofOfPayment, "contracts", "payments"); // Added underscore

                        TempData["Success"] = $"File uploaded successfully! File name: {fileName}";

                        // Clear the model for a fresh form
                        return View(new FileUploadModel());
                    }
                    else
                    {
                        ModelState.AddModelError("ProofOfPayment", "Please select a file to upload.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error uploading file: {ex.Message}");
                }
            }

            return View(model);
        }
    }
}