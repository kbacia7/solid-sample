using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book : CModel
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }

    [ForeignKey("Author")]
    public int Author_ID { get; set; }
    public virtual Author Author { get; set; }
}