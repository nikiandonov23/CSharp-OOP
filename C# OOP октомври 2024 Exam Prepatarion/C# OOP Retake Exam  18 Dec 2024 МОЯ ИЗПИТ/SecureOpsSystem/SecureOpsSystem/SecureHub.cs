using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace SecureOpsSystem
{
    public class SecureHub
    {
        private int capacity;
        private List<SecurityTool> tools;

        public SecureHub(int capacity)
        {
            Capacity = capacity;
            tools = new List<SecurityTool>();
        }

        public int Capacity 
        {
            get => this.capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity must be greater than 0.");
                }
                this.capacity = value;
            } 
        }
        public IReadOnlyCollection<SecurityTool> Tools => tools.AsReadOnly();

        public string AddTool(SecurityTool tool)
        {
            if (tools.Any(t => t.Name == tool.Name))
            {
                return $"Security Tool {tool.Name} already exists in the hub.";
            }

            if (tools.Count >= Capacity)
            {
                return "Secure Hub is at full capacity.";
            }

            tools.Add(tool);
            return $"Security Tool {tool.Name} added successfully.";
        }

        public bool RemoveTool(SecurityTool tool)
        {
            return tools.Remove(tool);
        }

        public SecurityTool DeployTool(string toolName)
        {
            var tool = tools.FirstOrDefault(t => t.Name == toolName);
            if (tool != null)
            {
                tools.Remove(tool);
            }

            return tool;
        }

        public string SystemReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Secure Hub Report:");
            sb.AppendLine($"Available Tools: {tools.Count}");

            foreach (var tool in tools.OrderByDescending(t => t.Effectiveness))
            {
                sb.AppendLine(tool.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
