using Clothes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Interfaces
{
    public interface IAdded
    {
        public IActionResult Add(Product product);
    }
}
