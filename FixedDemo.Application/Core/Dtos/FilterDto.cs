namespace FixedDemo.Application.Core.Dtos
{
    public class FilterDto
    {
        public required string Operator { get; set; }
        public required string Field { get; set; }
        public required string Value { get; set; }
    }
}
