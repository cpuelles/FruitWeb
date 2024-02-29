using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FruitWeb.Models;

public class Color
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore] 
    [InverseProperty(nameof(Fruit.ColorNavigation))]
    public virtual IEnumerable<Fruit> Fruits { get; set; }
}