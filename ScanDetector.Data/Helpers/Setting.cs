

namespace ScanDetector.Data.Helpers
{
    public class Setting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public string? ValueType { get; set; }
        public Guid? UserId { get; set; }
    }
}
