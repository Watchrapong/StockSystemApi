using Microsoft.EntityFrameworkCore;
using StockSystem.Api.Data;
using StockSystem.Api.Domain;
using StockSystem.Api.Exception;
using StockSystem.Api.Extensions;
using StockSystem.Api.Request.ShoppingCart;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly UnitOfWork _unitOfWork;

    public ShoppingCartService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public object GetCart(Guid userId)
    {
        var cart = _unitOfWork.Repo<ShoppingCart>().Table
                              .Where(s => s.PaymentDateOnUtc == null)
                              .Where(s => s.UserId == userId)
                              .Select(s => new
                              {
                                  s.UserId,
                                  Items = s.Items.Select(i => new
                                  {
                                      i.ProductId,
                                      ProductName = i.Product.Name,
                                      ProductCode = i.Product.Code,
                                      i.Product.PricePerUnit,
                                      i.Quantity,
                                  }).ToList(),
                                  s.TotalPrice
                              }).FirstOrDefault();

        return cart;
    }

    public void AddProduct(AddProductReq req)
    {
        var stock = _unitOfWork.Repo<Stock>().Table
                               .Include(s => s.Product)
                               .FirstOrDefault(s => s.ProductId == req.ProductId);
        if (stock == null)
        {
            throw new AppException("Product not found");
        }

        if (req.Quantity > stock.Number)
        {
            throw new AppException("Quantity exceeds stock");
        }

        var cart = _unitOfWork.Repo<ShoppingCart>().Table
                              .Include(s => s.Items)
                              .Where(s => s.PaymentDateOnUtc == null)
                              .FirstOrDefault(s => s.UserId == req.UserId);

        if (cart == null)
        {
            var newCart = new ShoppingCart()
            {
                UserId = req.UserId,
                Items = new List<ShoppingCartItem>() { new() { ProductId = stock.ProductId, Quantity = req.Quantity, PricePerUnit = stock.Product.PricePerUnit } },
                TotalPrice = stock.Product.PricePerUnit * req.Quantity
            };
            _unitOfWork.Repo<ShoppingCart>().Insert(newCart);
        }

        if (cart != null)
        {
            var currentProduct = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId);
            if (currentProduct != null)
            {
                currentProduct.Quantity += req.Quantity;
                currentProduct.PricePerUnit = stock.Product.PricePerUnit;
            }

            if (currentProduct == null)
            {
                cart.Items.Add(new  ShoppingCartItem()
                {
                    ProductId = req.ProductId,
                    Quantity = req.Quantity,
                    PricePerUnit = stock.Product.PricePerUnit,
                });
            }

            cart.TotalPrice = cart.GetSummaryTotalPrice();
            _unitOfWork.Repo<ShoppingCart>().Update(cart);
        }
        stock.Number -= req.Quantity;
        _unitOfWork.Repo<Stock>().Update(stock);
        
        _unitOfWork.Save();
    }

    public void RemoveProduct(AddProductReq req)
   {
        var stock = _unitOfWork.Repo<Stock>().Table
                               .Include(s => s.Product)
                               .FirstOrDefault(s => s.ProductId == req.ProductId);
        if (stock == null)
        {
            throw new AppException("Product not found");
        }

        var cart = _unitOfWork.Repo<ShoppingCart>().Table
                              .Include(s => s.Items)
                              .Where(s => s.PaymentDateOnUtc == null)
                              .FirstOrDefault(s => s.UserId == req.UserId);

        if (cart == null)
        {
            throw new AppException("Cart not found");
        }
       
        var currentProduct = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId);
        if (currentProduct == null)
        {
            throw new AppException("Product not found in cart");
        }
        if (req.Quantity > currentProduct.Quantity)
        {
            throw new AppException("Quantity exceeds stock");
        }
        currentProduct.Quantity -= req.Quantity;
        currentProduct.PricePerUnit += req.Quantity;
        if (currentProduct.Quantity == 0)
        {
            _unitOfWork.Repo<ShoppingCartItem>().Delete(currentProduct);
        }

        cart.TotalPrice = cart.GetSummaryTotalPrice();
        _unitOfWork.Repo<ShoppingCart>().Update(cart);
        
        stock.Number -= req.Quantity;
        _unitOfWork.Repo<Stock>().Update(stock);
        
        _unitOfWork.Save();
    }

    public void ClearCart(Guid userId)
    {
        var cart = _unitOfWork.Repo<ShoppingCart>().Table
                              .Include(s => s.Items)
                              .Where(s => s.UserId == userId)
                              .FirstOrDefault(s => s.PaymentDateOnUtc == null);
        if (cart == null)
        {
            throw new AppException("Cart not found");
        }

        cart.TotalPrice = 0;
        _unitOfWork.Repo<ShoppingCart>().Update(cart);
        _unitOfWork.Repo<ShoppingCartItem>().DeleteRange(cart.Items);
        _unitOfWork.Save();
    }

    public void Checkout(Guid userId)
    {
        var cart = _unitOfWork.Repo<ShoppingCart>().Table
                              .Include(s => s.Items)
                              .Where(s => s.PaymentDateOnUtc == null)
                              .FirstOrDefault(s => s.UserId == userId);

        if (cart == null)
        {
            throw new AppException("Cart not found");
        }
        
        cart.PaymentDateOnUtc = DateTime.UtcNow;
        _unitOfWork.Repo<ShoppingCart>().Update(cart);
        _unitOfWork.Save();
    }
}