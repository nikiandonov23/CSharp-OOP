namespace SecureOpsSystem
{
    public class SecurityTool
    {
        public SecurityTool(string name, string category, double effectiveness)
        {
            Name = name;
            Category = category;
            Effectiveness = effectiveness;
        }

        public string Name { get; private set; }
        public string Category { get; private set; }
        public double Effectiveness { get; private set; }

        public override string ToString()
        {
            return $"Name: {Name}, Category: {Category}, Effectiveness: {Effectiveness:F2}";
        }
    }
}
