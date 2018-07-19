﻿using SmartSql.Abstractions;
using SmartSql.Abstractions.Cache;
using SmartSql.Configuration.Maps;
using SmartSql.Configuration.Tags;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SmartSql.Configuration.Statements
{
    public enum StatementType
    {
        Insert = 1 << 0,
        Update = 1 << 1,
        Delete = 1 << 2,
        Select = 1 << 3
    }

    public class Statement
    {
        public virtual StatementType Type { get; internal set; }
        [XmlIgnore]
        public SmartSqlMap SmartSqlMap { get; internal set; }
        [XmlAttribute]
        public String Id { get; set; }
        public String FullSqlId => $"{SmartSqlMap.Scope}.{Id}";
        public IList<ITag> SqlTags { get; set; }
        public Cache Cache { get; set; }
        public ResultMap ResultMap { get; set; }
        public ParameterMap ParameterMap { get; set; }

        public void BuildSql(RequestContext context)
        {
            foreach (ITag tag in SqlTags)
            {
                tag.BuildSql(context);
            }
        }
    }
}
