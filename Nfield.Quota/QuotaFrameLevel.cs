using System;
using System.Collections.Generic;
using System.Linq;

namespace Nfield.Quota
{
    public class QuotaFrameLevel : IQuotaCell
    {
        public QuotaFrameLevel()
        {
            Variables = new List<QuotaFrameVariable>();
        }

        public QuotaFrameLevel(IEnumerable<QuotaFrameVariable> variables)
        {
            Variables = new List<QuotaFrameVariable>(variables);
        }

        public Guid Id { get; set; }

        public Guid DefinitionId { get; set; }

        public string Name { get; set; }

        public ICollection<QuotaFrameVariable> Variables { get; }
        IEnumerable<QuotaFrameVariable> IQuotaCell.Variables => Variables;

        public int? Target { get; set; }

        public int? MaxTarget { get; set; }

        public int? MaxOvershoot { get; set; }

        public bool IsHidden { get; set; }

        public QuotaFrameVariable this[string variableName]
        {
            get
            {
                var variable = Variables.FirstOrDefault(v => v.Name == variableName);
                if (variable == null)
                {
                    throw new InvalidOperationException(
                        $"Cannot find variable named '{variableName}' in on variable '{Name}' (variable id: {Id}).");
                }

                return variable;
            }
        }

        public QuotaFrameLevel this[string variableName, string levelName]
        {
            get
            {
                var variable = this[variableName];

                var level = variable.Levels.FirstOrDefault(l => l.Name == levelName);
                if (level == null)
                {
                    throw new InvalidOperationException(
                        $"Cannot find level named '{levelName}' on variable '{variableName}' (variable id: '{variable.Id}')");
                }

                return level;
            }
        }
    }
}