﻿using AutoMapper;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Providers;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

namespace Calabonga.Catalog.Web.Infrastructure.Managers
{
    /// <summary>
    /// Product entity manager
    /// <see cref="IViewModelFactory{TEntity,TCreateViewModel,TUpdateViewModel}"/> implementation
    /// </summary>
    public class ProductManager : EntityManager<Product, ProductCreateViewModel, ProductUpdateViewModel>
    {
        private readonly IProductProvider _productProvider;
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public ProductManager(
            IProductProvider productProvider,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IMapper mapper,
            IViewModelFactory<Product, ProductCreateViewModel, ProductUpdateViewModel> viewModelFactory,
            IEntityValidator<Product> validator)
            : base(mapper, viewModelFactory, validator)
        {
            _productProvider = productProvider;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public override void OnEditBeforeMappings(ProductUpdateViewModel model, Product entity)
        {
            if (!entity.Visible)
            {
                var catalog = _unitOfWork.GetRepository<Category>().GetFirstOrDefault(predicate: x => x.Id == entity.CategoryId);
                if (catalog == null)
                {
                    Validator.AddValidationResult(new ValidationResult("Каталог не найден", true));
                }

                if (catalog != null && !catalog.Visible)
                {
                    if (!entity.Visible && model.Visible)
                    {
                        Validator.AddValidationResult(new ValidationResult("Нельзя включить товар, если его категория выключена", true));
                    }
                }
            }

            if (entity.Visible && !model.Visible)
            {
                var operation = _productProvider.ChangeReviewVisibilityByProductId(entity, false);
                if (!operation.Ok)
                {
                    Validator.AddValidationResult(new ValidationResult("Что-то пошло не так."));
                }
            }

            if (!entity.Visible && model.Visible)
            {
                var operation = _productProvider.ChangeReviewVisibilityByProductId(entity, true);
                if (!operation.Ok)
                {
                    Validator.AddValidationResult(new ValidationResult("Что-то пошло не так."));
                }
            }
        }
    }
}