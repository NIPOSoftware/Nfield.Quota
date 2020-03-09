﻿using System;
using System.Collections.Generic;
using Nfield.Quota.Models;

namespace Nfield.Quota.Builders
{
    public class QuotaFrameBuilder
    {
        private int? _target;
        private readonly IList<QuotaVariableDefinitionBuilder> _variableDefinitionBuilders;
        private readonly IList<QuotaFrameStructureBuilder> _structureBuilders;

        public QuotaFrameBuilder()
        {
            _variableDefinitionBuilders = new List<QuotaVariableDefinitionBuilder>();
            _structureBuilders = new List<QuotaFrameStructureBuilder>();
        }

        private void Add(QuotaVariableDefinitionBuilder builder)
        {
            _variableDefinitionBuilders.Add(builder);
        }
        public QuotaFrame Build()
        {
            var frame = new QuotaFrame
            {
                Target = _target,
            };

            foreach (var builder in _variableDefinitionBuilders)
            {
                builder.Build(frame);
            }

            foreach (var builder in _structureBuilders)
            {
                builder.Build(frame);
            }

            return frame;
        }

        public QuotaFrameBuilder Target(int? target)
        {
            _target = target;
            return this;
        }

        public QuotaFrameBuilder VariableDefinition(
            string variableName,
            string odinVariableName,
            IEnumerable<string> levelNames,
            VariableSelection selection = VariableSelection.NotApplicable,
            bool isMulti = false)
        {
            bool? isSelectionOptional = null;
            switch (selection)
            {
                case VariableSelection.Mandatory:
                    isSelectionOptional = false;
                    break;
                case VariableSelection.Optional:
                    isSelectionOptional = true;
                    break;
            }

            var variableDefinitionBuilder = new QuotaVariableDefinitionBuilder(
                Guid.NewGuid(),
                variableName,
                odinVariableName,
                levelNames,
                isSelectionOptional,
                isMulti
                );
            Add(variableDefinitionBuilder);
            return this;
        }

        public QuotaFrameBuilder VariableDefinition(
            string variableName,
            IEnumerable<string> levelNames,
            VariableSelection selection = VariableSelection.NotApplicable,
            bool isMulti = false)
        {
            return VariableDefinition(variableName, variableName.ToLowerInvariant(), levelNames, selection, isMulti);
        }

        public QuotaFrameBuilder Structure(
            Action<QuotaFrameStructureBuilder> buildAction)
        {
            var builder = new QuotaFrameStructureBuilder();

            _structureBuilders.Add(builder);

            buildAction(builder);

            return this;
        }
    }
}