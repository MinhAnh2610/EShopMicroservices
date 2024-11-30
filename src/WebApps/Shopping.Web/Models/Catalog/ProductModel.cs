﻿namespace Shopping.Web.Models.Catalog;

public class ProductModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = default!;
  public List<string> Category { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string ImageFile { get; set; } = default!;
  public string Price { get; set; } = default!;
}

// wrapper classes
public record GetProductsResponse(IEnumerable<ProductModel> Products);
public record GetProductsByCategoryResponse(IEnumerable<ProductModel> Products);
public record GetProductByIdResponse(ProductModel Products);