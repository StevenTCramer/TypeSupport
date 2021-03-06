﻿#pragma warning disable 0649,0169
namespace TypeSupport.Tests.TestObjects
{
    public class BasicObject
    {
        private readonly int _test;
        public int Id { get; set; }

        public int Test
        {
            get
            {
                return _test;
            }
        }

        public BasicObject(int id)
        {
            Id = id;
            _test = 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
