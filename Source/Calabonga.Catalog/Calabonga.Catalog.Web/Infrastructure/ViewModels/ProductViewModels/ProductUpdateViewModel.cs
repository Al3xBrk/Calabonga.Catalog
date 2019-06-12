﻿using System;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels
{
    /// <summary>
    /// ViewModel for Product updating
    /// </summary>
    public class ProductUpdateViewModel : ViewModelBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Category identifier
        /// </summary>
        public Guid CategoryId { get; set; }
        
        /// <summary>
        /// Price
        /// </summary>
        public int? Price { get; set; }
    }
}